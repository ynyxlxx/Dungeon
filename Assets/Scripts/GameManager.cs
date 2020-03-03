using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    public Player playerInstance { get; private set; }
    public bool findThePlayer { get; private set; }

    private void Start () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy (this.gameObject);
        }

        DontDestroyOnLoad (this);
    }

    private void Update () {
        if (findThePlayer == false && playerInstance == null) {
            try {
                playerInstance = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
                if (playerInstance != null) {
                    findThePlayer = true;
                }
            } catch {

            }
        }

        if (findThePlayer == true && playerInstance == null) {
            Debug.Log ("Game Over!!!");
            findThePlayer = false;
            SceneManager.LoadScene ("GameScene");
        }
    }

    public void EnterNextLevel () {
        Debug.Log ("Loading next level....");
        SceneManager.LoadScene ("GameScene");
    }

}