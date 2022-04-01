using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartPowerUpManager : MonoBehaviour
{
    [SerializeField] int maximoDeItensCarregados;
    public enum listPowerUps  
    {
        Empty,Blaster, Blade, Shield
    }
    [System.NonSerialized] public listPowerUps[] powerUpsAtuais;
    private void Awake()
    {
        powerUpsAtuais = new listPowerUps[maximoDeItensCarregados];
        for (int i = 0; i < maximoDeItensCarregados; i++)
        {
            powerUpsAtuais[i] = listPowerUps.Empty;
        }
    }
    public void PickUpPowerUp()
    {
        for(int i = 0; i<maximoDeItensCarregados;i++)
        {
            
        }
    }
}
