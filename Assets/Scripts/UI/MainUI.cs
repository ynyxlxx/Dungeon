using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUI : MonoBehaviour {
    public Animator transitionAnimator;

    public void StartGame () {
        transitionAnimator.SetTrigger ("end");
        StartCoroutine (LoadNextLevel ());
    }

    public void QuitGame () {
        Application.Quit ();
    }

    private IEnumerator LoadNextLevel () {
        yield return new WaitForSeconds (3f);
        SceneManager.LoadScene ("GameScene");
    }

}