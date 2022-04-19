using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
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
            print("a");
        }
    }
}
