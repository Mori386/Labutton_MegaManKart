using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectScreenConfigs : MonoBehaviour
{
    public static SelectScreenConfigs Instance;
    public int playerAmmount;
    public int[] kartIDPlayer;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    public void definePlayerAmmount(int pAmmount)
    {
        playerAmmount = pAmmount;
        kartIDPlayer = new int[playerAmmount];
    }
}
