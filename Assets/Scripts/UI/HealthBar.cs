using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    private int health;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private Player player;

    private void Update () {
        if (GameManager.instance.findThePlayer) {
            player = GameManager.instance.playerInstance;
            health = (int) player.health;

            if (health > player.maxHealth) health = (int) player.maxHealth;

            numOfHearts = (int) player.maxHealth;

            for (int i = 0; i < hearts.Length; i++) {
                if (i < health) {
                    hearts[i].sprite = fullHeart;
                } else {
                    hearts[i].sprite = emptyHeart;
                }

                if (i < numOfHearts) {
                    hearts[i].enabled = true;
                } else {
                    hearts[i].enabled = false;
                }
            }
        }

    }
}