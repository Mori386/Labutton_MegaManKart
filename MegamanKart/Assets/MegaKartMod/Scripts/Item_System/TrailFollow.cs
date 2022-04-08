using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailFollow : MonoBehaviour
{
    [SerializeField] Transform followObject;
    Vector3 deltaPosition;
    private void Awake()
    {
        deltaPosition = transform.position - followObject.position;
    }
    private void FixedUpdate()
    {
        transform.position = followObject.position + deltaPosition;
    }
}
