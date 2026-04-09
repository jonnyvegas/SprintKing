using UnityEngine;

public class PlayerCollisionHandler : CollisionHandler
{
    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        Debug.Log("Player collision handler. collided with " + collision.gameObject.name);
    }
    public override void OnTriggerEnter(Collider other)
    {
        
    }
}
