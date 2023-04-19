using UnityEngine;

public class cameraController : MonoBehaviour
{
    public Transform car;
    private Rigidbody car_rb;

    public Vector3 Offset;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        car_rb = car.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void LateUpdate()
    {

        Vector3 playerForward = (car_rb.velocity + 
            car.transform.forward).normalized;

        transform.position = Vector3.Lerp(transform.position,
            car.position + car.transform.TransformVector(Offset) + 
            playerForward * (-5f), speed * Time.deltaTime);

        transform.LookAt(car);
    }
}
