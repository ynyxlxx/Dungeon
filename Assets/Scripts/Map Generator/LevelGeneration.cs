using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour {
    //TODO: refactoring...

    public Transform[] startingPositions;
    public RoomInfo[] rooms;

    private int nextMoveDirection;
    public float moveIncrement;
    private float roomGenTimer;
    public float roomGenTimeInterval;

    public LayerMask roomMask;

    public float maxX;
    public float minX;
    public float minY;

    [HideInInspector]
    public bool stopGen = false;

    private float downCounter = 0;

    private Vector2 startRoomPos;
    private Vector2 endRoomPos;
    private bool startEndRoomGenerated = false;

    private void Start () {

        int randStartingPos = Random.Range (0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;
        Instantiate (rooms[0].prefabs, transform.position, Quaternion.identity);
        startRoomPos = transform.position;

        nextMoveDirection = GetRandomNumberBtw (1, 5);
    }

    private void Update () {
        if (roomGenTimer >= roomGenTimeInterval && stopGen == false) {
            MoveGenerationPoint ();
            roomGenTimer = 0f;
        } else {
            roomGenTimer += Time.deltaTime;
        }

        if (startEndRoomGenerated == false && stopGen == true) {
            startEndRoomGenerated = true;
            GenerateStartEndRoom ();
        }

    }

    // 0 -> LR, 1 -> LRB, 2 -> LRT, 3 -> LRTB
    private void MoveGenerationPoint () {
        //move right
        if (nextMoveDirection == 1 || nextMoveDirection == 2) {
            if (transform.position.x < maxX) {
                downCounter = 0;
                Vector2 nowPos = new Vector2 (transform.position.x + moveIncrement, transform.position.y);
                transform.position = nowPos;

                int rand = GetRandomNumberBtw (0, 3);
                Instantiate (rooms[rand].prefabs, transform.position, Quaternion.identity);

                //避免重叠， 往一个方向移动后不能返回
                nextMoveDirection = GetRandomNumberBtw (1, 5);
                if (nextMoveDirection == 3) {
                    nextMoveDirection = 1;
                } else if (nextMoveDirection == 4) {
                    nextMoveDirection = 5;
                }

            } else {
                nextMoveDirection = 5;
            }
            //move left
        } else if (nextMoveDirection == 3 || nextMoveDirection == 4) {
            if (transform.position.x > minX) {
                downCounter = 0;
                Vector2 nowPos = new Vector2 (transform.position.x - moveIncrement, transform.position.y);
                transform.position = nowPos;

                int rand = GetRandomNumberBtw (0, 3);
                Instantiate (rooms[rand].prefabs, transform.position, Quaternion.identity);

                nextMoveDirection = GetRandomNumberBtw (3, 5);

            } else {
                nextMoveDirection = 5;
            }
            // move down
        } else if (nextMoveDirection == 5) {
            if (transform.position.y > minY) {
                downCounter++;
                Collider2D roomDetection = Physics2D.OverlapCircle (transform.position, 1, roomMask);

                RoomType roomType = roomDetection.GetComponent<RoomType> ();
                if (roomType.type != 1 && roomType.type != 3) {
                    //连续两次向下移动时，开口可能被阻断，此时必需生成四向开口的room来保证连通
                    if (downCounter >= 2) {
                        roomDetection.GetComponent<RoomType> ().RoomDestruction ();
                        Instantiate (rooms[3].prefabs, transform.position, Quaternion.identity);
                    } else {
                        //此时的transform.position还是上一个room的位置，检测上一层的room是否有底部的开口，如果没有，摧毁之后替换成底部开口的, 1, 3 是底部开口的     
                        roomType.RoomDestruction ();
                        int randBottomRoom = GetRandomNumberBtw (1, 3);
                        if (randBottomRoom == 2) {
                            randBottomRoom = 1;
                        }
                        Instantiate (rooms[randBottomRoom].prefabs, transform.position, Quaternion.identity);
                    }
                }

                Vector2 nowPos = new Vector2 (transform.position.x, transform.position.y - moveIncrement);
                transform.position = nowPos;

                //保证生成的room有顶部的开口
                int rand = GetRandomNumberBtw (2, 3);
                Instantiate (rooms[rand].prefabs, transform.position, Quaternion.identity);

                nextMoveDirection = GetRandomNumberBtw (1, 5);
            } else {
                endRoomPos = transform.position;
                stopGen = true;
            }
        }
    }

    private void GenerateStartEndRoom () {
        Collider2D roomDetection = Physics2D.OverlapCircle (startRoomPos, 1, roomMask);
        roomDetection.GetComponent<RoomType> ().RoomDestruction ();

        roomDetection = Physics2D.OverlapCircle (endRoomPos, 1, roomMask);
        roomDetection.GetComponent<RoomType> ().RoomDestruction ();

        Instantiate (rooms[4].prefabs, startRoomPos, Quaternion.identity);
        Instantiate (rooms[5].prefabs, endRoomPos, Quaternion.identity);
    }

    private int GetRandomNumberBtw (int a, int b) {
        return Random.Range (a, b + 1);
    }

    [System.Serializable]
    public struct RoomInfo {
        public int type;
        public Transform prefabs;

        public RoomInfo (int type, Transform prefabs) {
            this.type = type;
            this.prefabs = prefabs;
        }
    }

}