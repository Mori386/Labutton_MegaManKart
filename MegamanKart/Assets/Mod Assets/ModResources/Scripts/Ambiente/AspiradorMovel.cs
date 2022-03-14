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
        while(Vector3.Distance(transform.position,localFinal)>0.01)
        {
            transform.position += direcao * velocidade * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        StartCoroutine(MoverObjeto(localInicial));
        yield break;
    }
}
