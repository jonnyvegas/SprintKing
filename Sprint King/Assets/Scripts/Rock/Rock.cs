using Unity.Cinemachine;
using UnityEngine;

public class Rock : MonoBehaviour
{
    CinemachineImpulseSource cinemachineImpulseSource;
    [SerializeField] float shakeModifier = 10f;
    [SerializeField] ParticleSystem rockParticles;
    [SerializeField] AudioClip rockClip;
    AudioSource rockSource;
    float deltaSinceFire = 0f;
    float cooldown = 1f;
    private void Awake()
    {
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
        rockSource = GetComponent<AudioSource>();
        rockSource.clip = rockClip;
    }

    private void Update()
    {
        deltaSinceFire += Time.deltaTime;
    }

    private void FireImpulse()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        float shakeIntensity = (1f / distance) * shakeModifier;
        shakeIntensity = Mathf.Min(shakeIntensity, 1);
        cinemachineImpulseSource.GenerateImpulse(shakeIntensity);
    }

    private void CollisionFX(Collision collision)
    {
        if(rockParticles)
        {
            rockParticles.transform.position = collision.GetContact(0).point;
            rockParticles.Play();
        }
        if(rockSource && rockClip)
        {
            rockSource.Play();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (deltaSinceFire > cooldown)
        {
            //FireImpulse();
            CollisionFX(collision);
            deltaSinceFire = 0f;
        }
    }
}
