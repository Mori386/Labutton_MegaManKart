using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatoBraco : MonoBehaviour
{
    [Header("Configurações")]
    [SerializeField] float delayEntreMovimentos;
    [SerializeField] Vector3 anguloInicial;
    [SerializeField] Vector3 anguloFinal;
    [SerializeField] float grausPorTick;
    private void Awake()
    {
        transform.rotation = Quaternion.Euler(anguloInicial);
        grausPorTick = Mathf.Abs(grausPorTick);
    }
    private void Start()
    {
        StartCoroutine(RotacionarObjeto(anguloInicial, anguloFinal));
    }
    private IEnumerator RotacionarObjeto(Vector3 pontoInicial, Vector3 pontoFinal)
    {
        Vector3 anguloDelta = pontoFinal - pontoInicial;
        while (anguloDelta != new Vector3(0, 0, 0))
        {
            Vector3 anguloAplicado = pontoFinal;
            if (anguloDelta.x > 0)
            {
                if (grausPorTick > anguloDelta.x)
                {
                    anguloAplicado.x = transform.rotation.eulerAngles.x + anguloDelta.x;
                    anguloDelta.x = 0;
                }
                else
                {
                    anguloAplicado.x = transform.rotation.eulerAngles.x + grausPorTick;
                    anguloDelta.x -= grausPorTick;
                }
            }
            else if (anguloDelta.x < 0)
            {
                if (-grausPorTick < anguloDelta.x)
                {
                    anguloAplicado.x = transform.rotation.eulerAngles.x + anguloDelta.x;
                    anguloDelta.x = 0;
                }
                else
                {
                    anguloAplicado.x = transform.rotation.eulerAngles.x - grausPorTick;
                    anguloDelta.x += grausPorTick;
                }
            }


            if (anguloDelta.y > 0)
            {
                if (grausPorTick > anguloDelta.y)
                {
                    anguloAplicado.y = transform.rotation.eulerAngles.y + anguloDelta.y;
                    anguloDelta.y = 0;
                }
                else
                {
                    anguloAplicado.y = transform.rotation.eulerAngles.y + grausPorTick;
                    anguloDelta.y -= grausPorTick;
                }
            }
            else if (anguloDelta.y < 0)
            {
                if (-grausPorTick < anguloDelta.y)
                {
                    anguloAplicado.y = transform.rotation.eulerAngles.y + anguloDelta.y;
                    anguloDelta.y = 0;
                }
                else
                {
                    anguloAplicado.y = transform.rotation.eulerAngles.y - grausPorTick;
                    anguloDelta.y += grausPorTick;
                }
            }


            if (anguloDelta.z > 0)
            {
                if (grausPorTick > anguloDelta.z)
                {
                    anguloAplicado.z = transform.rotation.eulerAngles.z + anguloDelta.z;
                    anguloDelta.z = 0;
                }
                else
                {
                    anguloAplicado.z = transform.rotation.eulerAngles.z + grausPorTick;
                    anguloDelta.z -= grausPorTick;
                }
            }
            else if (anguloDelta.z < 0)
            {
                if (-grausPorTick < anguloDelta.z)
                {
                    anguloAplicado.z = transform.rotation.eulerAngles.z + anguloDelta.z;
                    anguloDelta.z = 0;
                }
                else
                {
                    anguloAplicado.z = transform.rotation.eulerAngles.z - grausPorTick;
                    anguloDelta.z += grausPorTick;
                }
            }

            transform.rotation = Quaternion.Euler(anguloAplicado);
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(delayEntreMovimentos);
        StartCoroutine(RotacionarObjeto(pontoFinal, pontoInicial));
    }
}
