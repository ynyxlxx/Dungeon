using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRooms : MonoBehaviour {
    public LayerMask roomMask;
    public LevelGeneration levelGeneration;

    private void Update () {
        Collider2D roomDetection = Physics2D.OverlapCircle (transform.position, 1, roomMask);
        if (roomDetection == null && levelGeneration.stopGen) {
            //int rand = Random.Range (0, levelGeneration.rooms.Length);
            int rand = RandomManager.instance.GetRandomNumberBtw (0, levelGeneration.rooms.Length - 3);
            Instantiate (levelGeneration.rooms[rand].prefabs, transform.position, Quaternion.identity);
            Destroy (gameObject);
        }
    }
}