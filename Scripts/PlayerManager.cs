using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public static bool isGameStarted;
    public static bool isGamePaused;
    public GameObject gameOverPanel;
    public GameObject startingText;
    public static int numberOfCoins = 0;

    public Text coinsText;
    // Start is called before the first frame update
    void Start()
    {
        gameOver = isGameStarted = isGamePaused = false;
        gameOverPanel.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //Game Over
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);

        }
        coinsText.text = "Coins : " + numberOfCoins;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            isGameStarted = true;
            Destroy(startingText);
        }
    }
}
