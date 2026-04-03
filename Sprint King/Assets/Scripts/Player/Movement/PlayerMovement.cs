using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 movement;
    Rigidbody rb;
    Vector3 currentPos;
    Vector3 targetPos;
    [SerializeField] float movementSpeed = 1.0f;
    [SerializeField] float xClamp = 4.5f;
    [SerializeField] float zClamp = 1.0f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        currentPos = rb.position;
    }
    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    public void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        targetPos = currentPos;
        targetPos.x += movement.x * (movementSpeed * Time.deltaTime);
        targetPos.z += movement.y * (movementSpeed * Time.deltaTime);
        currentPos = targetPos;
        currentPos.x = Mathf.Clamp(currentPos.x, -xClamp, xClamp);
        currentPos.z = Mathf.Clamp(currentPos.z, -zClamp, zClamp);
        rb.MovePosition(currentPos);
        /*
        currentPos = rb.position;
        moveDir.x = movement.x;
        moveDir.z = movement.y;
        moveDir.y = 0f;
        targetPos = currentPos + moveDir * movementSpeed * Time.deltaTime;
        rb.MovePosition(targetPos);
        */
    }
}
