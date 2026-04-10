using UnityEngine;

public interface ICollisionHandler
{
    // empty.
    public virtual void OnCollisionEnter(Collision collision) { }
    public virtual void OnTriggerEnter(Collider other) { }
}

public class CollisionHandler : MonoBehaviour, ICollisionHandler
{
    public virtual void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(this.gameObject.name + " collided with" + collision.gameObject.name);
    }
    public virtual void OnTriggerEnter(Collider other)
    {
        //Debug.Log(this.gameObject.name + " is a trigger and has triggered " + other.gameObject.name);
    }
}
