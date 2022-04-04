using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartPowerUpManager : MonoBehaviour
{
    [SerializeField] int maximoDeItensCarregados;
    public enum listPowerUps
    {
        Empty = 0,
        Missel = 1,
        Blade = 2,
        Shield = 3,
        LandMine = 4,
        Hack = 5
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
    public void UpdatePowerupUse()
    {
        switch ((int)powerUpsAtuais[0])
        {
            case 0:
                PowerUpUse = null;
                break;
            case 1:
                PowerUpUse = MisselUse;
                break;
            case 2:
                PowerUpUse = BladeUse;
                break;
            case 3:
                PowerUpUse = BladeUse;
                break;
            case 4:
                PowerUpUse = LandMineUse;
                break;
            case 5:
                PowerUpUse = HackUse;
                break;
        }
    }
    private void MovetoNextItemSlot()
    {
        for (int i = 0; i < maximoDeItensCarregados; i++)
        {
            if(i+1<maximoDeItensCarregados)powerUpsAtuais[i] = powerUpsAtuais[i+1];
        }
    }
    public delegate void KartPowerUpAvailiabes();
    public KartPowerUpAvailiabes PowerUpUse;
    public void PickUpPowerUp(listPowerUps powerUp)
    {
        for (int i = 0; i < maximoDeItensCarregados; i++)
        {
            if (powerUpsAtuais[i] == 0)
            {
                powerUpsAtuais[i] = powerUp;
                break;
            }
        }
    }
    public void MisselUse()
    {

    }
    public void BladeUse()
    {

    }
    public void ShieldUse()
    {

    }
    public void LandMineUse()
    {

    }
    public void HackUse()
    {

    }
}
