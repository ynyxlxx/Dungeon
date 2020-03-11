using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    public Player playerInstance { get; private set; }
    public bool findThePlayer { get; private set; }

    private bool isEnterNextLevel = false;

    public Animator transitionAnimator;
    public RectTransform gameOverUI;

    private void Start () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy (this.gameObject);
        }

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
            //StartCoroutine (LoadScene ());
            PlayerStats.Instance._firstInitial = true;
            gameOverUI.gameObject.SetActive (true);
        }

        if (findThePlayer == true && playerInstance.gameObject.GetComponent<PlayerController> ().isReadyToEnterNextLevel == true && isEnterNextLevel == false) {
            isEnterNextLevel = true;
            transitionAnimator.SetTrigger ("end");
            Invoke ("EnterNextLevel", 3f);
        }
    }

    public void EnterNextLevel () {
        Debug.Log ("Loading next level....");
        isEnterNextLevel = false;
        SceneManager.LoadScene ("GameScene");
    }

    public void LoadMainMenu () {
        SceneManager.LoadScene ("MainMenu");
    }

    public void TryAgainButton () {
        transitionAnimator.SetTrigger ("end");
        gameOverUI.gameObject.SetActive (false);
        Invoke ("EnterNextLevel", 3f);
    }

    public void MainMenuButton () {
        transitionAnimator.SetTrigger ("end");
        gameOverUI.gameObject.SetActive (false);
        Invoke ("LoadMainMenu", 3f);
    }

    public void QuitButton () {
        Application.Quit ();
    }

}