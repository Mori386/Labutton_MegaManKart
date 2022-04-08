using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSelfDestruct : MonoBehaviour
{
    ParticleSystem particleSys;
    private void Awake()
    {
        particleSys = GetComponent<ParticleSystem>();
    }
    private void Start()
    {
        particleSys.Play();
    }
    private void FixedUpdate()
    {
        if(particleSys && !particleSys.IsAlive())
        {
            Destroy(gameObject);
        }
    }
}
