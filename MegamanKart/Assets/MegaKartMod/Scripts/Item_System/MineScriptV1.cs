using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineScriptV1 : MonoBehaviour
{
    private bool explodeCheck = false;
    private MeshRenderer r;
    private BoxCollider boxCollider;
    ParticleSystem particleSys;
    void Awake()
    {
        r = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        particleSys = GetComponent<ParticleSystem>();
    }
    private void Start()
    {
        Invoke("turnOn", 1);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (explodeCheck == true && other.tag=="Player")
        {
            r.enabled = false;
            boxCollider.enabled = false;
            particleSys.Play();
            Debuff.Instance.AplicarDebuff(other.gameObject);
            other.GetComponent<ParticleSystem>().Play();
            Invoke("selfDestroy", 1);
        }
    }

    private void turnOn()
    {
        explodeCheck = true;
    }

    private void selfDestroy()
    {
        Destroy(gameObject);
    }

}
