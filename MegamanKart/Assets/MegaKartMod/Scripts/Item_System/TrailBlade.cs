using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailBlade : MonoBehaviour
{
    [SerializeField] Transform blade;
    Vector3 deltaPosition;
    private void Awake()
    {
        deltaPosition = transform.position - blade.position;
    }
    private void FixedUpdate()
    {
        transform.position = blade.position + deltaPosition;
    }
}
