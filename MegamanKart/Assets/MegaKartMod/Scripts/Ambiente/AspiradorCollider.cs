using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspiradorCollider : MonoBehaviour
{
    private void Start()
    {
        DebuffPlayer.Instance.objetosAtravesaveis.Add(gameObject);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            DebuffPlayer.Instance.AplicarDebuff();
        }

    }
}
