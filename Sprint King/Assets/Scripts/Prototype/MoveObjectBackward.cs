using UnityEngine;

public class MoveObjectBackward : MonoBehaviour
{
    Vector3 updatedPosition;
    [SerializeField] float speed = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        updatedPosition = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.transform.Translate(this.transform.forward * -1 * (speed * Time.deltaTime));
        //updatedPosition = this.gameObject.transform.position;
        //updatedPosition.z -= Time.deltaTime * amtToMove;
        //this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, updatedPosition, 1);

    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public float GetSpeed()
    {
        return this.speed;
    }
}
