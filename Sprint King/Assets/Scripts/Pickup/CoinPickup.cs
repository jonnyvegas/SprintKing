using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Events;

public class CoinPickup : PickupParent
{
    //public Scoreboard scoreboard;
    public UnityEvent onScoreIncreasePickup;
    [SerializeField] public int scoreAmt = 100;
    ScorePickupBehavior scorePickup;
    public void Init(Scoreboard scoreboard)
    {
        //this.scoreboard = scoreboard;
        scorePickup = new AddScorePickupBehavior();
        AddScorePickupBehavior adpb = (AddScorePickupBehavior)scorePickup;
        adpb.scoreboard = scoreboard;
    }
    public override void OnPickup()
    {
        base.OnPickup();
        //Debug.Log("Money money money dolla dolla");
        //onScoreIncreasePickup.Invoke();
        scorePickup.ChangeScore(scoreAmt);
    }
}
