using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    private Transform playerPos;
    public Camera mainCam;

    [SerializeField]
    private float cameraMoveSpeed = 5f;

    // private void Start () {
    //     mainCam = Camera.main;
    //     // StartCoroutine ("PlayerFollow");
    // }

    private void Update () {
        if (GameManager.instance.findThePlayer) {
            playerPos = GameManager.instance.playerInstance.gameObject.transform;
            mainCam.transform.position = Vector3.Lerp (
                mainCam.transform.position,
                new Vector3 (playerPos.position.x, playerPos.position.y, mainCam.transform.position.z),
                cameraMoveSpeed * Time.deltaTime);
        }
    }

    // private IEnumerator PlayerFollow () {
    //     while (true) {
    //         mainCam.transform.position = Vector3.Lerp (
    //             mainCam.transform.position,
    //             new Vector3 (playerPos.position.x, playerPos.position.y, mainCam.transform.position.z),
    //             Time.deltaTime * cameraMoveSpeed);

    //         yield return null;
    //     }
    // }
}