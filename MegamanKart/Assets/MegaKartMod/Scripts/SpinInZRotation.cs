using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinInZRotation : MonoBehaviour
{
    [SerializeField] float spinPerTick;
    private void FixedUpdate()
    {
        transform.Rotate(0, 0, spinPerTick*Time.fixedDeltaTime);
    }
}
