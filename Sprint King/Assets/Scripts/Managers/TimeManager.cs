using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] TMP_Text timeText;
    [SerializeField] GameObject gameOverText;
    bool gameOver = true;
    [SerializeField] float startTime = 5f;
    float remainingSecs = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOver = false;
        remainingSecs = startTime;
        gameOverText.SetActive(false);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            return;
        }
        remainingSecs -= Time.deltaTime;
        // mins : seconds. No hours needed.
        timeText.text = "Time Left: " + remainingSecs.ToString("F1"); 
        if(remainingSecs < 0f)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        gameOver = true;
        gameOverText.SetActive(true);
        Time.timeScale = .1f;
        playerMovement.enabled = false;
        
    }
    public void addDeltaRemainingSeconds(float deltaSecs)
    {
        remainingSecs += deltaSecs;
    }
}
