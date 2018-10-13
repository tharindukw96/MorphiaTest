using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class menuManager : MonoBehaviour {
    public Text scoreText;
    private int score;
    private void Start()
    {
        score = PlayerPrefs.GetInt("PlayerScore");
        scoreText.text = "High Score: " + score.ToString();
    }
    public void toGame() {
        SceneManager.LoadScene("Level1");

    }
}
