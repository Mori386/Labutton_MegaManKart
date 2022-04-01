using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartBoostKeySpin : MonoBehaviour
{
    [SerializeField] float spinPerTick;
    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, spinPerTick));
    }
}
