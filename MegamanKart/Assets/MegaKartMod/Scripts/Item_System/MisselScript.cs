using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisselScript : MonoBehaviour
{
    [Header("Configurações Subindo")]
    [SerializeField] private float goingUpTime;
    [SerializeField] private float goingUpSpeed;

    [Header("Configurações Seguindo")]
    [SerializeField] private float trackingSpeed;
    public Transform target;

    [SerializeField] private float spinPerTick;
    private void Awake()
    {
        transform.Rotate(-45f, 0, 0);
    }
    private void Start()
    {
        StartCoroutine(GoingUp());
    }
    private IEnumerator GoingUp()
    {
        float timer = 0;
        while (timer < goingUpTime)
        {
            transform.position += transform.forward * goingUpSpeed * Time.fixedDeltaTime;
            timer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        StartCoroutine(waitForTarget());
    }
    private IEnumerator waitForTarget()
    {
        while (target == null) yield return new WaitForSeconds(0.05f);
        StartCoroutine(GoingToTarget());
    }
    private IEnumerator GoingToTarget()
    {
        while (true)
        {
            var rotation = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.fixedDeltaTime * 4);
            transform.position += transform.forward * trackingSpeed * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debuff.Instance.AplicarDebuff(other.gameObject);
            Transform pstr = transform.Find("ParticleExplosion");
            pstr.gameObject.AddComponent<ParticleSelfDestruct>();
            pstr.parent = null;
            Transform parent = transform.parent;
            transform.parent = null;
            Destroy(parent.gameObject);



            Transform trail = transform.Find("Trail");
            trail.GetComponent<TrailRenderer>().autodestruct = true;
            trail.parent = null;
            Destroy(gameObject);

        }
    }
}
