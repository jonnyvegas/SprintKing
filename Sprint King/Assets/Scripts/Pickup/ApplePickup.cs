using UnityEngine;

public class ApplePickup : PickupParent
{
    //private string levelGeneratorTag = "LevelGenerator";
    LevelGenerator levelGen;
    float deltaSpeed = 3f;

    public void Init(LevelGenerator levelGenerator)
    {
        this.levelGen = levelGenerator;
    }
    private void Start()
    {
        //levelGen = GameObject.FindGameObjectWithTag(levelGeneratorTag);
    }
    public override void OnPickup()
    {
        base.OnPickup();
        if(levelGen)
        {
            levelGen.SetSpeed(levelGen.GetSpeed() + deltaSpeed);
        }
    }
}
