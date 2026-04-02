using UnityEngine;

public class MoveObjectBackward : MonoBehaviour
{
    Vector3 updatedPosition;
    [SerializeField] float amtToMove = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        updatedPosition = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.transform.Translate(this.transform.forward * -1 * (amtToMove * Time.deltaTime));
        //updatedPosition = this.gameObject.transform.position;
        //updatedPosition.z -= Time.deltaTime * amtToMove;
        //this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, updatedPosition, 1);

    }
}
