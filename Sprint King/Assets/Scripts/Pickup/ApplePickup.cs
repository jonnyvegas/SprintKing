using UnityEngine;

public class ApplePickup : PickupParent
{
    PowerupPickupBehavior speedUpBehavior;
    float deltaSpeed = 3f;

    public void Init(ILevelGenerator levelGenerator)
    {
        speedUpBehavior = new SpeedUpBehavior();
        // Cast it once so we can set it and forget it.
        SpeedUpBehavior sub = (SpeedUpBehavior)speedUpBehavior;
        sub.lg = levelGenerator;
    }
    private void Start()
    {
        //levelGen = GameObject.FindGameObjectWithTag(levelGeneratorTag);
    }
    public override void OnPickup()
    {
        base.OnPickup();

        speedUpBehavior.Powerup();
    }
}
