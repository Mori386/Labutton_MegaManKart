using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeScript : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float spinPerTick;
    Transform visual;
    [SerializeField, Range(0, 50)] private float speed;
    private void Awake()
    {
        visual = transform.Find("Visual");
        Invoke("autoDestroy", duration);
    }
    private void FixedUpdate()
    {
        visual.Rotate(0,0,spinPerTick*Time.fixedDeltaTime);
        transform.position += transform.forward * speed * Time.fixedDeltaTime;
    }
    private void autoDestroy()
    {
        Transform trail = transform.parent.Find("Trail");
        GameObject parent = trail.parent.gameObject;
        trail.parent = null;
        trail.GetComponent<TrailFollow>().enabled = false;
        Destroy(parent);
        trail.GetComponent<TrailRenderer>().autodestruct = true;
        Destroy(gameObject);
    }
}
