using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private float timeToAdd = 5f;
    public float TimeToAdd => timeToAdd;
}
