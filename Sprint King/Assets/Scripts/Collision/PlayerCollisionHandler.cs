using UnityEngine;

public class PlayerCollisionHandler : CollisionHandler
{
    [SerializeField] Animator animator;
    const string hitString = "Hit";
    [SerializeField] float hitCooldown = .5f;
    float currentTime = -1.5f;
    [SerializeField] float deltaMoveSpeed = -2f;

    [SerializeField] LevelGenerator LevelGenerator;
    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        if (Time.time - currentTime > hitCooldown)
        {
            currentTime = Time.time;
            animator.SetTrigger(hitString);
            LevelGenerator.SetSpeed(LevelGenerator.GetSpeed() + deltaMoveSpeed);
        }

    }
    public override void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Checkpoint checkpoint))
        {
            LevelGenerator.TimeManager.addDeltaRemainingSeconds(checkpoint.TimeToAdd);
        }
    }
}
