using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomManager : MonoBehaviour {
    public static RandomManager instance;

    private int lastGenerateNum;
    private int newGenerateNum;

    private void Start () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy (gameObject);
        }

        DontDestroyOnLoad (this);
    }

    public int GetRandomNumberBtw (int a, int b) {
        newGenerateNum = Random.Range (a, b + 1);
        if (newGenerateNum == lastGenerateNum) {
            newGenerateNum = Random.Range (a, b + 1);
            if (newGenerateNum == lastGenerateNum) {
                newGenerateNum = Random.Range (a, b + 1);
            }
        }
        lastGenerateNum = newGenerateNum;
        return newGenerateNum;
    }
}