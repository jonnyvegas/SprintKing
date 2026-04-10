using UnityEngine;

public class ApplePickup : PickupParent
{
    private string levelGeneratorTag = "LevelGenerator";
    GameObject levelGen;
    float deltaSpeed = 3f;
    private void Start()
    {
        levelGen = GameObject.FindGameObjectWithTag(levelGeneratorTag);
    }
    public override void OnPickup()
    {
        base.OnPickup();
        if(levelGen.TryGetComponent(out LevelGenerator levelGenerator))
        {
            levelGenerator.SetSpeed(levelGenerator.GetSpeed() + deltaSpeed);
        }
    }
}
