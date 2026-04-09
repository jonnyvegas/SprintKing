using UnityEngine;

public class PickupCollisionHandler : CollisionHandler
{
    public override void OnTriggerEnter(Collider other)
    {
        const string playerString = "Player";
        base.OnTriggerEnter(other);
        
        //Debug.Log("pickup has entered as a trigger.");
        if(other.CompareTag(playerString))
        {
            Debug.Log(other.gameObject.name);
        }
    }
}
