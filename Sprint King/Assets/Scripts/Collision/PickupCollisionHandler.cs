using UnityEngine;

public class PickupCollisionHandler : CollisionHandler
{
    public override void OnTriggerEnter(Collider other)
    {
        const string playerString = "Player";
        base.OnTriggerEnter(other);
        //Debug.Log(this.gameObject.name);
        //Debug.Log("pickup has entered as a trigger.");
        if(other.CompareTag(playerString))
        {
            if(TryGetComponent(out PickupParent pickupParentRef))
            {
                pickupParentRef.OnPickup();
            }
        }
    }
}
