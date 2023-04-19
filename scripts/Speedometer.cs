using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour {

    private const float max_angle = -20;
    private const float zero_angle = 230;

    private Transform needleTransform;
    private Transform speedLabelTemplateTransform;

    private float speedMax;
    public int current_speed;

    //public int speed;
    public float needleSpeed = 400f;

    public car_controller car_controller;

    private void Start()
    {
        GameObject carObject = GameObject.Find("Chev666");

        car_controller = carObject.GetComponent<car_controller>();
        
    }

    void Awake()
    {
        needleTransform = transform.Find("needle");

        speedLabelTemplateTransform = 
            transform.Find("speedLabelTemplate");

        speedLabelTemplateTransform.
            gameObject.SetActive(false);

        speedMax = 140f;
        CreateSpeedLabels();
    }
    void CreateSpeedLabels()
    {
        int labelAmount = 7;
        float totalAngleSize = zero_angle - max_angle;

        for (int i = 0; i <= labelAmount; i++)
        {
            Transform speedLabelTransform = Instantiate(speedLabelTemplateTransform, transform);

            float labelSpeedNormalized = (float)i / labelAmount;
            float speedLabelAngle = zero_angle - labelSpeedNormalized * totalAngleSize;

            speedLabelTransform.eulerAngles = new Vector3(0, 0, speedLabelAngle);

            speedLabelTransform.Find("speedText").GetComponent<Text>().text = Mathf.RoundToInt(labelSpeedNormalized * speedMax).ToString();

            speedLabelTransform.Find("speedText").eulerAngles = Vector3.zero;

            speedLabelTransform.gameObject.SetActive(true);
        }
        needleTransform.SetAsLastSibling();
    }

    private void Update()
    {
        current_speed = (int) car_controller.kmh;

        // position the needle based on the speed
        float needleRotation = GetSpeedRotation();
        needleTransform.eulerAngles = new Vector3(0, 0, needleRotation);

        // animate the needle rotation
        float prevRotation = needleTransform.localEulerAngles.z;
        float rotationDiff = Mathf.DeltaAngle(prevRotation, needleRotation);
        float rotationStep = Mathf.Sign(rotationDiff) * Mathf.Min(Mathf.Abs(rotationDiff), needleSpeed * Time.deltaTime);
        needleTransform.localEulerAngles += new Vector3(0, 0, rotationStep);
    }

    private float GetSpeedRotation() 
    {
        float totalAngleSize = zero_angle - max_angle;

        float speedNormalized = current_speed / speedMax;

        return zero_angle - speedNormalized * totalAngleSize;
    }
}
