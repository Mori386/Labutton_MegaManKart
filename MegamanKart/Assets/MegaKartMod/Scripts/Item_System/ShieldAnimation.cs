using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAnimation : MonoBehaviour
{
    [SerializeField]float spinPerTick;
    private void FixedUpdate()
    {
        transform.Rotate(0,spinPerTick*Time.fixedDeltaTime, spinPerTick*Time.fixedDeltaTime);
    }
}
