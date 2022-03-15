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
    bool debuffApplicado;
    private void Awake()
    {
        Instance = this;
        objetosAtravesaveis = new List<GameObject>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            AplicarDebuff();
        }
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
        foreach(GameObject go in objetosAtravesaveis)
        {
            layerIds.Add(go.layer);
            go.layer = LayerMask.NameToLayer("SemColisaoPlayer");
        }
        Transform kartVisual = transform.Find("KartVisual");
        Vector3 escalaOriginal = kartVisual.transform.localScale;
        kartVisual.transform.localScale = escalaReduzida;
        KartGame.KartSystems.ArcadeKart arcadeKart = GetComponent<KartGame.KartSystems.ArcadeKart>();
        //arcadeKart.baseStats.TopSpeed = 1;
        //salvar a velocidade atual(original)
        //Diminuir velocidade do alvo, tanto aceleração quanto velocidade para ambos os lados
        yield return new WaitForSeconds(duracao);
        //aplicar a velocidade original 
        kartVisual.transform.localScale = escalaOriginal;
        for (int i =0;i<layerIds.Count;i++)
        {
            objetosAtravesaveis[i].layer = layerIds[i];
        }
        debuffApplicado =false;
    }
}
