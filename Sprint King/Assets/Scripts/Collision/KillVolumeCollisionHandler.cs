using UnityEngine;

public class KillVolumeCollisionHandler : CollisionHandler
{
    public override void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Destroying " + other.gameObject.name);
        Destroy(other.gameObject);
    }
}
