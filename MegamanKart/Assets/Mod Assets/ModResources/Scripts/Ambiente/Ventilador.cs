using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ventilador : MonoBehaviour
{
    [Header("Configurações")]
    [SerializeField] Vector3 direcao;

    [SerializeField] float forca;
    private void Awake()
    {
        direcao = Vector3.Normalize(direcao);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (empurrarJogador == null) empurrarJogador = StartCoroutine(EmpurrarJogador(other.transform.parent.gameObject));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (empurrarJogador != null)
            {
                StopCoroutine(empurrarJogador);
                empurrarJogador = null;
            }
        }
    }
    Coroutine empurrarJogador;
    IEnumerator EmpurrarJogador(GameObject player)
    {
        Rigidbody rb;
        rb = player.GetComponent<Rigidbody>();
        while(true)
        {
            rb.velocity += direcao*forca;
            yield return new WaitForFixedUpdate();
        }
    }
}
