using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffPlayer : MonoBehaviour
{
    public static DebuffPlayer Instance;
    [System.NonSerialized] public List<GameObject> objetosAtravesaveis;
    [Header("Configurações")]
    [SerializeField] private Vector3 escalaReduzida;
    [SerializeField] private float duracao;
    [SerializeField] private float porcentagemDeLentidao;
    [SerializeField] private bool lentidaoDecadente;
    bool debuffApplicado;
    private void Awake()
    {
        Instance = this;
        objetosAtravesaveis = new List<GameObject>();
    }
    public void AplicarDebuff()
    {
        if (!debuffApplicado)
        {
            debuffApplicado = true;
            StartCoroutine(DebuffTimer());
        }
    }
    private IEnumerator DebuffTimer()
    {
        List<int> layerIds = new List<int>();
        foreach (GameObject go in objetosAtravesaveis)
        {
            layerIds.Add(go.layer);
            go.layer = LayerMask.NameToLayer("SemColisaoPlayer");
        }
        Transform kartVisual = transform.Find("KartVisual");
        Vector3 escalaOriginal = kartVisual.transform.localScale;
        kartVisual.transform.localScale = escalaReduzida;
        KartGame.KartSystems.ArcadeKart arcadeKart = GetComponent<KartGame.KartSystems.ArcadeKart>();
        float topSpeed = arcadeKart.baseStats.TopSpeed;
        float acceleration = arcadeKart.baseStats.Acceleration;
        float reverseSpeed = arcadeKart.baseStats.ReverseSpeed;
        float reverseAcceleration = arcadeKart.baseStats.ReverseAcceleration;
        if (!lentidaoDecadente)
        {
            arcadeKart.baseStats.TopSpeed = topSpeed - topSpeed * (porcentagemDeLentidao / 100);
            arcadeKart.baseStats.Acceleration = acceleration - acceleration * (porcentagemDeLentidao / 100);
            arcadeKart.baseStats.ReverseSpeed = reverseSpeed - reverseSpeed * (porcentagemDeLentidao / 100);
            arcadeKart.baseStats.ReverseAcceleration = reverseAcceleration - reverseAcceleration * (porcentagemDeLentidao / 100);
            //Diminuir velocidade do alvo, tanto aceleração quanto velocidade para ambos os lados
            yield return new WaitForSeconds(duracao);
        }
        else
        {
            for (float i = duracao / 0.1f; i >0; i--)
            {
                kartVisual.transform.localScale += (escalaOriginal - escalaReduzida)/(duracao / 0.1f);
                arcadeKart.baseStats.TopSpeed = topSpeed * (porcentagemDeLentidao* (((duracao / 0.1f) - i) / (duracao / 0.1f))) / 100;
                arcadeKart.baseStats.Acceleration = acceleration * (porcentagemDeLentidao * (((duracao / 0.1f) - i) / (duracao / 0.1f))) / 100;
                arcadeKart.baseStats.ReverseSpeed = reverseSpeed * (porcentagemDeLentidao * (((duracao / 0.1f) - i) / (duracao / 0.1f))) / 100;
                arcadeKart.baseStats.ReverseAcceleration = reverseAcceleration * (porcentagemDeLentidao * (((duracao / 0.1f) - i) / (duracao / 0.1f))) / 100;
                yield return new WaitForSeconds(0.1f);
            }
        }
        //aplicar a velocidade original 
        arcadeKart.baseStats.TopSpeed = topSpeed;
        arcadeKart.baseStats.Acceleration = acceleration;
        arcadeKart.baseStats.ReverseSpeed = reverseSpeed;
        arcadeKart.baseStats.ReverseAcceleration = reverseAcceleration;
        kartVisual.transform.localScale = escalaOriginal;
        for (int i = 0; i < layerIds.Count; i++)
        {
            objetosAtravesaveis[i].layer = layerIds[i];
        }
        debuffApplicado = false;
    }
}
