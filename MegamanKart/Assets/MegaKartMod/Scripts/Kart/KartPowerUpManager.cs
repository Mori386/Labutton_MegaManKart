using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KartPowerUpManager : MonoBehaviour
{
    [SerializeField] int maximoDeItensCarregados;
    [SerializeField] Transform castFrontPositionTransform;
    [SerializeField] Transform castBackPositionTransform;
    public Image powerUpAtualImagem;

    private KeyCode powerUpKey;
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
    private void Start()
    {
        powerUpKey = GetComponent<KartController>().powerUpKey;
    }
    private void Update()
    {
        if(powerUpsAtuais[0]!=0&&Input.GetKeyDown(powerUpKey))
        {
            PowerUpUse();
        }
    }
    public void UpdatePowerupUse()
    {
        switch ((int)powerUpsAtuais[0])
        {
            case 0:
                PowerUpUse = null;
                powerUpAtualImagem.sprite = ManagerPowerUps.Instance.Img_Empty;
                break;
            case 1:
                PowerUpUse = MisselUse;
                powerUpAtualImagem.sprite = ManagerPowerUps.Instance.Img_Missel;
                break;
            case 2:
                PowerUpUse = BladeUse;
                powerUpAtualImagem.sprite = ManagerPowerUps.Instance.Img_Blade;
                break;
            case 3:
                PowerUpUse = ShieldUse;
                powerUpAtualImagem.sprite = ManagerPowerUps.Instance.Img_Shield;
                break;
            case 4:
                PowerUpUse = LandMineUse;
                powerUpAtualImagem.sprite = ManagerPowerUps.Instance.Img_LandMine;
                break;
            case 5:
                PowerUpUse = HackUse;
                powerUpAtualImagem.sprite = ManagerPowerUps.Instance.Img_Hack;
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
        UpdatePowerupUse();
    }
    public void MisselUse()
    {
        Transform target = null;
        foreach(KartInfos kartInfos in ManagerPowerUps.Instance.kartList)
        {
            if (kartInfos.kartObject != gameObject && kartInfos.kartObject != null)
            {
                if (kartInfos.controller.placeInRace == GetComponent<KartController>().placeInRace - 1)
                {
                    target = kartInfos.kartObject.transform;
                    GameObject missel = Instantiate(ManagerPowerUps.Instance.prefabMissel, castFrontPositionTransform.position, Quaternion.Euler(0, transform.eulerAngles.y, 0));
                    missel.transform.Rotate(0, transform.rotation.y, 0);
                    missel.transform.Find("MisselPowerControl").GetComponent<MisselScript>().target = target;
                    powerUpsAtuais[0] = 0;
                    powerUpAtualImagem.sprite = ManagerPowerUps.Instance.Img_Empty; ;
                }
            }
        }
        if(target == null)
        {
            Debug.Log("No target");
        }
    }
    public void BladeUse()
    {
        GameObject blade =
        Instantiate(ManagerPowerUps.Instance.prefabBlade, castFrontPositionTransform.position, Quaternion.Euler(0,transform.eulerAngles.y,0));
        powerUpsAtuais[0] = 0;
        powerUpAtualImagem.sprite = ManagerPowerUps.Instance.Img_Empty; ;
    }
    public void ShieldUse()
    {
        ShieldScript shieldScript = Instantiate(ManagerPowerUps.Instance.prefabShield).GetComponent<ShieldScript>();
        shieldScript.followTransform = transform;
        KartController kartController = GetComponent<KartController>();
        shieldScript.controller = kartController;
        kartController.isShielded = true;
        powerUpsAtuais[0] = 0;
        powerUpAtualImagem.sprite = ManagerPowerUps.Instance.Img_Empty; ;
    }
    public void LandMineUse()
    {
        Instantiate(ManagerPowerUps.Instance.prefabLandMine, castBackPositionTransform.position, Quaternion.identity);
        powerUpsAtuais[0] = 0;
        powerUpAtualImagem.sprite = ManagerPowerUps.Instance.Img_Empty; ;
    }
    public void HackUse()
    {
        HackControl.Instance.TurnOnHackScreen(GetComponent<KartController>());
        powerUpsAtuais[0] = 0;
        powerUpAtualImagem.sprite = ManagerPowerUps.Instance.Img_Empty; ;
    }
}
