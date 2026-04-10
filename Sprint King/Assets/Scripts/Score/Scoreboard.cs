using TMPro;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    int score = 0;
    public void UpdateScore(int deltaScore)
    {
        score += deltaScore;
        text.text = "Score: " + score;
    }
}  
