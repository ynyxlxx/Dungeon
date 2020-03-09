using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldUI : MonoBehaviour {
    public Text goldText;

    private float currentGold;

    private void Update () {
        currentGold = PlayerStats.Instance.GetMoney ();
        goldText.text = currentGold.ToString ();
    }
}