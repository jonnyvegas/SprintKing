using UnityEngine;



public class ScorePickupBehavior
{
    public virtual void ChangeScore(int deltaScore) { }
}

public class PowerupPickupBehavior
{
    public virtual void Powerup() { }
}

public class AddScorePickupBehavior : ScorePickupBehavior
{
    public IScoreboard scoreboard;
    public override void ChangeScore(int deltaScore)
    {
        base.ChangeScore(deltaScore);
        scoreboard.UpdateScore(deltaScore);
    }
}

public class SpeedUpBehavior : PowerupPickupBehavior
{
    public ILevelGenerator lg;
    public float deltaSpeed = 3f;
    public override void Powerup()
    {
        base.Powerup();
        lg.SetSpeed(lg.GetSpeed() + deltaSpeed);
    }
}