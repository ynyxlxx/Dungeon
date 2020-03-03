using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {
    public RectTransform healthBar;

    private bool findPlayer = false;

    private void Update () {
        if (findPlayer == false) {
            GameObject.FindGameObjectWithTag ("Player");
            findPlayer = true;
        }
    }

}