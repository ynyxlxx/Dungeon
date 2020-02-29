using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform player;
    private Camera mainCam;

    [SerializeField]
    private float cameraMoveSpeed = 5f;

    private void Start () {
        mainCam = Camera.main;
        StartCoroutine ("PlayerFollow");
    }

    private IEnumerator PlayerFollow () {
        while (true) {
            mainCam.transform.position = Vector3.Lerp (
                mainCam.transform.position,
                new Vector3 (player.position.x, player.position.y, mainCam.transform.position.z),
                Time.deltaTime * cameraMoveSpeed);

            yield return null;
        }
    }
}