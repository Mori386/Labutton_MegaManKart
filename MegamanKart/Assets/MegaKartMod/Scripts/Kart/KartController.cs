using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartController : MonoBehaviour
{
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

    //Angulação do carro 
    [SerializeField] float steerForce;
    private void Awake()
    {
        FWheels = new WheelCollider[2];
        FWheels[0] = FRWheel;
        FWheels[1] = FLWheel;

        BWheels = new WheelCollider[2];
        BWheels[0] = BRWheel;
        BWheels[1] = BLWheel;
    }
    private void Update()
    {
        BWheelsMotorTorqueInputAxis(Input.GetAxisRaw("Vertical"));
        FWheelsSteerAngleInputAxis(Input.GetAxisRaw("Horizontal"));
    }
    private void BWheelsMotorTorqueInputAxis(float inputAxis)
    {
        foreach (WheelCollider wheelCollider in BWheels)
        {
            wheelCollider.motorTorque = inputAxis*motorTorque;
        }
    }
    private void FWheelsSteerAngleInputAxis(float inputAxis)
    {
        foreach (WheelCollider wheelCollider in FWheels)
        {
            wheelCollider.steerAngle = inputAxis * steerForce;
        }
    }
}
