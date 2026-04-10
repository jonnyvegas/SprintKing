using UnityEngine;

public abstract class PickupParent : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 100f;
    protected CollisionHandler pickupCollisionHandler;

    private void Start()
    {
        pickupCollisionHandler = GetComponent<PickupCollisionHandler>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    public virtual void OnPickup()
    {
        Destroy(this.gameObject);
    }
}
