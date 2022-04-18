using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainScene 1"))
            {
                playerAmmount = 1;
                kartIDPlayer = new int[1];
                kartIDPlayer[0] = 0;
            }
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
