using UnityEngine;

public class PlayerCollisionHandler : CollisionHandler
{
    [SerializeField] Animator animator;
    const string hitString = "Hit";
    [SerializeField] float hitCooldown = .5f;
    float currentTime = -1.5f;
    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        
        if (Time.time - currentTime > hitCooldown)
        {
            currentTime = Time.time;
            animator.SetTrigger(hitString);
        }

    }
    public override void OnTriggerEnter(Collider other)
    {
        
    }
}
