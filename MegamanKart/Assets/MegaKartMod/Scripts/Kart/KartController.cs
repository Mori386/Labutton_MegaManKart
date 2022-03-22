using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class KartController : MonoBehaviour
{
    Rigidbody rb;
    [Header("Pneus")]
    [SerializeField] WheelCollider FRWheel;
    [SerializeField] WheelCollider FLWheel;
    [SerializeField] WheelCollider BRWheel;
    [SerializeField] WheelCollider BLWheel;
    WheelCollider[] FWheels;
    WheelCollider[] BWheels;

    [Header("Configurações")]
    //Força maxima do carro
    [SerializeField] float motorTorque;
    [SerializeField] float maxSpeed;

    //Angulação do carro 
    [SerializeField] float steerAngle;

    [SerializeField] float brakeForce;
    float currentBrakeForce;
    bool isBreaking;

    private void Awake()
    {
        FWheels = new WheelCollider[2];
        FWheels[0] = FRWheel;
        FWheels[1] = FLWheel;

        BWheels = new WheelCollider[2];
        BWheels[0] = BRWheel;
        BWheels[1] = BLWheel;
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        BWheelsMotorTorqueInputAxis(Input.GetAxisRaw("Vertical"));
        SpinWheel(Input.GetAxisRaw("Vertical"));
        FWheelsSteerAngleInputAxis(Input.GetAxisRaw("Horizontal"));
        BreakUpdate();
    }
    private void BWheelsMotorTorqueInputAxis(float inputAxis)
    {
        float speed = Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y) + Mathf.Abs(rb.velocity.z);
        if (speed<maxSpeed)
        {
            foreach (WheelCollider wheelCollider in BWheels)
            {
                wheelCollider.motorTorque = inputAxis * motorTorque;
            }
        }
        else
        {
            foreach (WheelCollider wheelCollider in BWheels)
            {
                wheelCollider.motorTorque = 0;
            }
        }
    }
    private void FWheelsSteerAngleInputAxis(float inputAxis)
    {
        foreach (WheelCollider wheelCollider in FWheels)
        {
            wheelCollider.steerAngle = inputAxis * steerAngle;
            Transform visual = wheelCollider.transform.parent.Find("Visual");
            visual.localRotation = Quaternion.Euler(new Vector3(visual.rotation.x, 270 + steerAngle * inputAxis, visual.rotation.z));
        }
    }
    private void BreakUpdate()
    {
        isBreaking = Input.GetKey(KeyCode.Space);
        currentBrakeForce = isBreaking ? brakeForce : 0f;
        foreach (WheelCollider wheelCollider in FWheels)
        {
            wheelCollider.brakeTorque = currentBrakeForce;
        }
        foreach (WheelCollider wheelCollider in BWheels)
        {
            wheelCollider.brakeTorque = currentBrakeForce;
        }
    }
    private void SpinWheel(float VerticalInputAxis)
    {
        foreach (WheelCollider wheelCollider in BWheels)
        {
            Transform visual = wheelCollider.transform.parent.Find("Visual");
            visual.Rotate(new Vector3(0, 0, -VerticalInputAxis));
        }
        foreach (WheelCollider wheelCollider in FWheels)
        {
            Transform visual = wheelCollider.transform.parent.Find("Visual");
            visual.Rotate(new Vector3(0, 0, -VerticalInputAxis));
        }
    }
}
