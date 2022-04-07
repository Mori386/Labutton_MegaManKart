using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineScriptV1 : MonoBehaviour
{
    [SerializeField] private bool explodeCheck = false;
    private MeshRenderer r;
    private BoxCollider boxCollider;
    ParticleSystem particleSys;
    private void Start()
    {
        Invoke("turnOn", 1);
    }
    void Awake()
    {
        r = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        particleSys = GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (explodeCheck == true && other.tag=="Player")
        {
            r.enabled = false;
            boxCollider.enabled = false;
            particleSys.Play();
            //TODO adicionar debuff
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
