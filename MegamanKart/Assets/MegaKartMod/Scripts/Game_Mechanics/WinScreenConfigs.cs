using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreenConfigs : MonoBehaviour
{
    [SerializeField] private GameObject[] HUD;
    private void OnEnable()
    {
        foreach(GameObject go in HUD)
        {
            go.SetActive(false);
        }
    }
}
