using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private float startTime = 5f;

    private float remainingSecs = 0f;
    
    private bool gameOver = true;
    // property.
    public bool GameOver => gameOver;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOver = false;
        remainingSecs = startTime;
        gameOverText.SetActive(false);
        Time.timeScale = 1f;
    }

    private void CountDown()
    {
        if (gameOver)
        {
            return;
        }
        remainingSecs -= Time.deltaTime;
        // mins : seconds. No hours needed.
        timeText.text = "Time Left: " + remainingSecs.ToString("F1");
        if (remainingSecs >= 0f)
        {
            return;
        }
        InvokeGameOver();
    }

    // Update is called once per frame
    void Update()
    {
        CountDown();
    }

    void InvokeGameOver()
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
