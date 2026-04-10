using UnityEngine;
using UnityEngine.Events;

public class CoinPickup : PickupParent
{
    public Scoreboard scoreboard;
    public UnityEvent onScoreIncreasePickup;

    public void Start()
    {
        scoreboard = GameObject.FindFirstObjectByType<Scoreboard>();
    }
    public override void OnPickup()
    {
        base.OnPickup();
        Debug.Log("Money money money dolla dolla");
        onScoreIncreasePickup.Invoke();
        scoreboard.UpdateScore(100);
    }
}
