using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {
    public Transform camTransform;

    private static float shakeDuration = 0f;
    private static float shakAmount = 0f;

    private Vector3 originalPos;

    private Vector3 newPosVelocity = Vector3.zero;
    private float shakeAmountVelocity = 0f;

    private void Awake () {
        if (camTransform == null) {
            camTransform = this.transform;
        }

        originalPos = camTransform.localPosition;
    }

    public static void ShakeOnce (float length, float strength) {
        shakeDuration = length;
        shakAmount = strength;
    }

    private void Update () {
        originalPos = camTransform.localPosition;
        if (shakAmount > 0) {
            Vector3 newPos = originalPos + Random.insideUnitSphere * shakAmount;
            camTransform.localPosition = Vector3.SmoothDamp (camTransform.localPosition, newPos, ref newPosVelocity, 0.05f);
            shakeDuration -= Time.deltaTime;
            shakAmount = Mathf.SmoothDamp (shakAmount, 0, ref shakeAmountVelocity, 0.7f);
        } else {
            camTransform.localPosition = originalPos;
        }
        // } else {
        //     camTransform.localPosition = originalPos;
        // }
    }
}