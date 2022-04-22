using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour
{
    [SerializeField] private int place;
    void Start()
    {
        KartInfos thisKartInfo = new KartInfos
        {
            kartObject = gameObject,

        };
        ManagerPowerUps.Instance.kartList.Add(thisKartInfo);
    }
}
