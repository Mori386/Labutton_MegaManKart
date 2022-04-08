using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    [SerializeField]float spinPerTick;
    public Transform followTransform;
    [SerializeField] float duration;
    public KartController controller;
    private void Start()
    {
        Destroy(gameObject,duration);
    }
    private void Update()
    {
        transform.position = followTransform.position;
    }
    private void FixedUpdate()
    {
        transform.Rotate(0,spinPerTick*Time.fixedDeltaTime, spinPerTick*Time.fixedDeltaTime);
    }
    private void OnDestroy()
    {
        controller.isShielded = false;
    }
}
