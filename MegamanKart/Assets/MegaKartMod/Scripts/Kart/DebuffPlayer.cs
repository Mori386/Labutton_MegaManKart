using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffPlayer : MonoBehaviour
{
    public static DebuffPlayer Instance;
    [System.NonSerialized] public List<GameObject> objetosAtravesaveis;
    [Header("Configurações")]
    [SerializeField] private float duracao;
    bool debuffApplicado;
    private void Awake()
    {
        Instance = this;
        objetosAtravesaveis = new List<GameObject>();
    }
    public void AplicarDebuff(GameObject player)
    {
        if (!debuffApplicado)
        {
            debuffApplicado = true;
            StartCoroutine(DebuffTimer(player));
        }
    }
    private IEnumerator DebuffTimer(GameObject player)
    {
        List<int> layerIds = new List<int>();
        foreach (GameObject go in objetosAtravesaveis)
        {
            layerIds.Add(go.layer);
            go.layer = LayerMask.NameToLayer("SemColisaoPlayer");
        }
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        yield return new WaitForSeconds(duracao);
        for (int i = 0; i < layerIds.Count; i++)
        {
            objetosAtravesaveis[i].layer = layerIds[i];
        }
        debuffApplicado = false;
    }
}
