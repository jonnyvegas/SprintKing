using TMPro;
using UnityEngine;

public interface IScoreboard
{
    public void UpdateScore(int deltaScore);
}

public class Scoreboard : MonoBehaviour, IScoreboard
{
    [SerializeField] TMP_Text text;
    int score = 0;
    public void UpdateScore(int deltaScore)
    {
        score += deltaScore;
        text.text = "Score: " + score;
    }
}  
