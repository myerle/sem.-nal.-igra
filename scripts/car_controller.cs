//using JetBrains.Annotations;
//using Unity.VisualScripting;
using UnityEngine;
//using UnityEngine.Rendering;
//using UnityEngine.UIElements;

public class car_controller : MonoBehaviour
{
    //igralni objekt
    private Rigidbody car;

    //wheel colliders:
    public WheelCollider frontRight;
    public WheelCollider backRight;
    public WheelCollider frontLeft;
    public WheelCollider backLeft;

    //wheel meshes
    public MeshRenderer f_right;
    public MeshRenderer b_right;
    public MeshRenderer f_left;
    public MeshRenderer b_left;

    //vnos
    public float gas_input;
    public float steering_input;
    public float brake_input;

    //nastavljive spremenljivke
 
    public float brakeForce;

    //spremenljivke za hitrost
    public float speed;
    public float kmh;
    public float motorForce;

    //spremenljivke za brzine
    public int currentGear;

    //funkcije
    public AnimationCurve steeringCurve;
    public AnimationCurve motorCurve;

    //apremenljivka za urejanje trenja
    WheelFrictionCurve ex_slip;

    //spremenljivke za sistem točk
    public float slip_angle;
    public int current_points;
    public int all_points;
    public int temp_points;


    void Start()
    {
        car = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        speed = car.velocity.magnitude;
        kmh = speed * 3.6f;

        WheelRotation();

        check_input();

        acceleration();

        braking();

        eBrake(frontRight, backRight);
        eBrake(frontLeft, backLeft);

        steering(); steering();

        gearing();

        pointCounter();


        counting_all_points();
    }

    void WheelRotation()
    {
        UpdateWheel(frontRight, f_right);
        UpdateWheel(frontLeft, f_left);
        UpdateWheel(backRight, b_right);
        UpdateWheel(backLeft, b_left);
    }

    void UpdateWheel(WheelCollider coll, MeshRenderer wheelMesh)
    {
        Quaternion quat;
        Vector3 position;

        coll.GetWorldPose(out position, out quat);
        wheelMesh.transform.position = position;
        wheelMesh.transform.rotation = quat;
    }


    void check_input()
    {
        gas_input = Input.GetAxis("Vertical");
        steering_input = Input.GetAxis("Horizontal");
    }

    void braking()
    {
        if (motorForce > 0)
        {
            if (gas_input < 0)
            {
                brake_input = Mathf.Abs(gas_input);
                gas_input = 0;
            }
            else brake_input = 0;
        }

        else brake_input = 0;

        frontRight.brakeTorque = brake_input * brakeForce * 0.35f;
        frontLeft.brakeTorque = brake_input * brakeForce * 0.35f;

        backRight.brakeTorque = brake_input * brakeForce * 0.15f;
        backLeft.brakeTorque = brake_input * brakeForce * 0.15f; 
    }

    void acceleration()
    {
        motorForce = gas_input * motorCurve.Evaluate(speed);
        
        backLeft.motorTorque = motorForce;
        backRight.motorTorque = motorForce;
    }

    void eBrake(WheelCollider front, WheelCollider back)
    {
        if (Input.GetKey(KeyCode.Space) == true)
        {
            front.brakeTorque = brakeForce * 0.35f;

            back.brakeTorque = brakeForce * 0.15f;

            ex_slip = back.sidewaysFriction;
            ex_slip.extremumSlip = 0.75f;
            back.sidewaysFriction = ex_slip;

        }

        else if (Input.GetKey(KeyCode.Space) == false)
        {
            front.brakeTorque = 0f;

            back.brakeTorque = 0f;

            counterDrift(back);
        }
    }

    void counterDrift(WheelCollider back)
    {
        if (ex_slip.extremumSlip <= 0.75f &&
            ex_slip.extremumSlip >= 0.3f)
        {
            ex_slip = back.sidewaysFriction;

            ex_slip.extremumSlip -= 0.005f;

            back.sidewaysFriction = ex_slip;
        }
    }


    void steering()
    {
        float steeringAngle = steering_input * 
            steeringCurve.Evaluate(speed);

        if (gas_input > 0) 
        {
            //protikrmiljenje
            steeringAngle += Vector3.SignedAngle(transform.forward, 
                car.velocity + transform.forward, Vector3.up);  
            steeringAngle = Mathf.Clamp(steeringAngle, -90f, 90f);
        }

        else if (gas_input < 0)
            steeringAngle = steering_input * 
                steeringCurve.Evaluate(speed);

        frontLeft.steerAngle = steeringAngle;
        frontRight.steerAngle = steeringAngle;
    }

    
    void gearing()
    {
        if (kmh < 30f) currentGear = 1;
        
        else if (kmh < 60f) currentGear = 2;
        
        else if (kmh < 80f) currentGear = 3;
        
        else if (kmh < 95f) currentGear = 4;

        else currentGear = 5;   
    }

    void pointCounter()
    {
        slip_angle = Vector3.Angle(transform.forward, 
            car.velocity - transform.forward);

        if (slip_angle > 10 && gas_input > 0f 
            && kmh > 10 || steering_input != 0 && kmh > 20)
        {
            current_points++;
        }

        else current_points = 0 ;

        if (current_points > 200)

            current_points ++;

        if (current_points > 500)

            current_points += 2;
        
        if (current_points > 1000) 

            current_points += 3;
    }

    void counting_all_points ()
    {
        if (current_points > 0)
            
            temp_points = current_points;

        if (current_points == 0)
        {
            all_points += temp_points;
            temp_points = 0;
        }
    }
}

