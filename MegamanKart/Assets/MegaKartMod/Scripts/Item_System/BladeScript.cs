using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeScript : MonoBehaviour
{
    [SerializeField] private float spinPerTick;
    Transform visual;
    [SerializeField, Range(0, 10)] private float speed;
    private void Awake()
    {
        visual = transform.Find("Visual");
        BoxCollider boxCollider = GetComponent<BoxCollider>();
    }
    private void FixedUpdate()
    {
        visual.Rotate(0,0,spinPerTick*Time.fixedDeltaTime);
        transform.position += transform.forward * speed * Time.fixedDeltaTime;
    }
}
