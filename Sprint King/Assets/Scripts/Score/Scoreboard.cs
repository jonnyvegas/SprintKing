using TMPro;
using UnityEngine;

public interface IScoreboard
{
    public void UpdateScore(int deltaScore);
}

public class Scoreboard : MonoBehaviour, IScoreboard
{
    [SerializeField] TimeManager timeManager;
    [SerializeField] TMP_Text text;
    int score = 0;
    public void UpdateScore(int deltaScore)
    {
        if (timeManager.GameOver) return;
        score += deltaScore;
        text.text = "Score: " + score;
    }
}  
