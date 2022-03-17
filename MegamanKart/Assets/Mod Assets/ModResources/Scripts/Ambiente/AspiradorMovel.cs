using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspiradorMovel : MonoBehaviour
{
    [SerializeField] Vector3 posicaoFinal;
    [SerializeField] float velocidade;
    private void Start()
    {
        StartCoroutine(MoverObjeto(posicaoFinal));
    }
    private IEnumerator MoverObjeto(Vector3 localFinal)
    {
        Vector3 localInicial = transform.position;
        Vector3 direcao;
        direcao = Vector3.Normalize(localFinal - transform.position);
        Vector3 posicaoDelta = localFinal - localInicial;
        float distanciaAPercorrer = Mathf.Abs(posicaoDelta.x) + Mathf.Abs(posicaoDelta.y) + Mathf.Abs(posicaoDelta.z);
        Vector3 distanciaVetorialPorSegundo= direcao * velocidade * Time.fixedDeltaTime;
        float distanciaTotalPorTick = Mathf.Abs(distanciaVetorialPorSegundo.x) + Mathf.Abs(distanciaVetorialPorSegundo.y) + Mathf.Abs(distanciaVetorialPorSegundo.z);
        while (distanciaAPercorrer > distanciaTotalPorTick)
        {
            distanciaAPercorrer -= distanciaTotalPorTick;
            transform.position += direcao * velocidade * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        if(distanciaAPercorrer > 0)
        {
            transform.position = localFinal;
        }
        StartCoroutine(MoverObjeto(localInicial));
        yield break;
    }
}
