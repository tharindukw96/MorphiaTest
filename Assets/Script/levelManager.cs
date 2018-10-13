using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelManager : MonoBehaviour {
    private int hitpoint = 3;
    private int score = 0;

    public Text lifeText;
    public Text scoreText;

    public Transform spawnPosition;
    public Transform playerTransform;

    public static levelManager instance { set; get; }


    private void Awake()
    {
        instance = this;
        lifeText.text = "Lives: " + hitpoint.ToString();
        scoreText.text = "Current Score: " + score.ToString();


    }
    private void Update() {
        scoreText.text = "Current Score: " + score.ToString();
        
        if (playerTransform.position.y < -10) {
            playerTransform.position = spawnPosition.position;
            hitpoint--;
            lifeText.text = "Lives: " + hitpoint.ToString();
            if (hitpoint <= 0) {
                Application.LoadLevel("menu");
            }
        }

    }
    public void win() {
        if (PlayerPrefs.GetInt("PlayerScore")<score)
        {
            PlayerPrefs.SetInt("PlayerScore", score);
        }
        Application.LoadLevel("menu");
    }
    public void collectCoin() {
        score += 5;
        scoreText.text = "Current Score: " + score.ToString();
    }
}
