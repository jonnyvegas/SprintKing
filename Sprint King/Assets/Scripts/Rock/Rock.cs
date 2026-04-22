using Unity.Cinemachine;
using UnityEngine;

public class Rock : MonoBehaviour
{
    CinemachineImpulseSource cinemachineImpulseSource;
    [SerializeField] float shakeModifier = 10f;
    private void Awake()
    {
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        float shakeIntensity = (1f / distance) * shakeModifier;
        shakeIntensity = Mathf.Min(shakeIntensity, 1);
        cinemachineImpulseSource.GenerateImpulse(shakeIntensity);
    }
}
