using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatoBracoCollider : MonoBehaviour
{
    private void Start()
    {
        DebuffPlayer.Instance.objetosAtravesaveis.Add(gameObject);
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            if (collision.contacts.Length > 0)
            {
                ContactPoint contact = collision.contacts[0];
                if (Vector3.Dot(contact.normal, Vector3.up) > 0.5)
                {
                    DebuffPlayer.Instance.AplicarDebuff();
                }
            }
        }

    }
}
