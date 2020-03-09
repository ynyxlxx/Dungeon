using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplayManager : MonoBehaviour {
    public static Text displayText;

    private static TextDisplayManager _instance;
    public static TextDisplayManager Instance {
        get {
            return _instance;
        }
    }

    private void Awake () {
        _instance = this;
        displayText = GetComponent<Text> ();
    }

    private void Start () {
        HideTheText ();
    }

    public void DisplayTheText (string toDisplay) {
        displayText.text = toDisplay;
        displayText.gameObject.SetActive (true);
    }

    public void HideTheText () {
        displayText.text = "";
        displayText.gameObject.SetActive (false);
    }
}