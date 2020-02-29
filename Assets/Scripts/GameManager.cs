using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    private void Start () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy (this.gameObject);
        }
    }

    public void EnterNextLevel () {
        Debug.Log ("Loading next level....");
        SceneManager.LoadScene ("GameScene");
    }
}