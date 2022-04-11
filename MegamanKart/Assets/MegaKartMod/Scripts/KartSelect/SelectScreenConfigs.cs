using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectScreenConfigs : MonoBehaviour
{
    public static SelectScreenConfigs Instance;
    public int playerAmmount;
    private void Awake()
    {
        Instance = this;    
    }
}
