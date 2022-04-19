using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [System.NonSerialized] public int checkPointOrder;
    private void Start()
    {
        GameObject visual = transform.GetChild(0).gameObject;
        for (int i = 1; i <= SelectScreenConfigs.Instance.playerAmmount; i++)
        {
            GameObject visualPerPlayer =
            Instantiate(visual, transform);
            visualPerPlayer.name = visual.name + "P" + i.ToString();
            visualPerPlayer.layer = LayerMask.NameToLayer("Cam" + i.ToString());
        }
        Destroy(visual);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            KartController kart = other.GetComponent<KartController>();
            if (kart.checkPointStage == checkPointOrder)
            {
                CoopManager.Instance.TurnOnNextCP(kart);
            }
        }
    }
    public void DisableChild(int childIdNum)
    {
        transform.GetChild(childIdNum).gameObject.SetActive(false);
    }
    public void EnableChild(int childIdNum)
    {
        transform.GetChild(childIdNum).gameObject.SetActive(true);
    }
}
