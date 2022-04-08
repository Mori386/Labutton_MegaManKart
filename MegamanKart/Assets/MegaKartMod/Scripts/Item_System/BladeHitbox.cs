using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeHitbox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Acertou jogador");
            Debuff.Instance.AplicarDebuff(other.gameObject);
            Transform trail = transform.parent.parent.Find("Trail");
            GameObject parent = trail.parent.gameObject;
            trail.parent = null;
            trail.GetComponent<TrailFollow>().enabled = false;
            Destroy(parent);
            trail.GetComponent<TrailRenderer>().autodestruct = true;
            Destroy(transform.parent);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Track"))
        {
            Transform trail = transform.parent.parent.Find("Trail");
            GameObject parent = trail.parent.gameObject;
            trail.parent = null;
            trail.GetComponent<TrailFollow>().enabled = false;
            Destroy(parent);
            trail.GetComponent<TrailRenderer>().autodestruct = true;
            Destroy(transform.parent);
        }
    }
}
