using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    public string hint;
    public ParticleSystem FloatingEffect;
    public float moveDistance = 0.1f;
    public float floatingSpeed = 0.8f;

    private float posY;

    private void Start () {
        posY = transform.position.y;
        ParticleSystem particle = Instantiate (FloatingEffect, transform.position, Quaternion.identity);
        particle.transform.parent = transform;
    }

    private void Update () {
        transform.position = new Vector2 (transform.position.x, posY + moveDistance * Mathf.Sin (floatingSpeed * Time.time));
    }

}