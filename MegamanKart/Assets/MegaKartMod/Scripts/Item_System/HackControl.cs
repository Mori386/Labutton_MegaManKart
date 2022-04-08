using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HackControl : MonoBehaviour
{
    public static HackControl Instance;
    Image hackScreen;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        hackScreen = GetComponent<Image>();
    }
    public void TurnOnHackScreen()
    {
        hackScreen.enabled=true;
        hackScreen.GetComponent<Animator>().SetTrigger("Hacked");
    }
    public void OnAnimationEnd()
    {
        foreach (KartInfos kartInfos in ManagerPowerUps.Instance.kartList)
        {
            Debuff.Instance.AplicarDebuff(kartInfos.kartObject);
        }
        hackScreen.enabled = false;
    }
}
