using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff : MonoBehaviour
{
    public static Debuff Instance;
    [System.NonSerialized] public List<GameObject> objetosAtravesaveis;
    [Header("Configurações")]
    [SerializeField] private float duracao;
    [SerializeField] private float spinPerTick;
    private void Awake()
    {
        Instance = this;
        objetosAtravesaveis = new List<GameObject>();
    }
    public void AplicarDebuff(GameObject player)
    {
        KartController controller = player.GetComponent<KartController>();
        if (controller && !controller.isShielded)
        {
            if (!controller.debuffApplicado)
            {
                controller.debuffApplicado = true;
                StartCoroutine(DebuffTimer(player));
            }
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
        Rigidbody rb = player.GetComponent<Rigidbody>();
        Transform visual = player.transform.Find("Visual");
        Vector3 originalRotation = visual.rotation.eulerAngles;
        float timer = 0;
        while (timer < duracao/2)
        {
            //visual.Rotate(0, spinPerTick * Time.deltaTime, 0);
            rb.velocity = rb.velocity/1.05f;

            timer += Time.deltaTime;
            yield return null;
        }
        timer = 0;
        Vector3 deltaRotation = originalRotation - visual.eulerAngles;
        while (timer < 100)
        {
            //visual.Rotate(0,deltaRotation.y/100,0);
            rb.velocity = rb.velocity / 1.05f;
            timer += 1;
            yield return null;
        }
        //visual.rotation = Quaternion.Euler(0,0,0);
        for (int i = 0; i < layerIds.Count; i++)
        {
            objetosAtravesaveis[i].layer = layerIds[i];
        }
        player.GetComponent<KartController>().debuffApplicado = false;
    }
}
