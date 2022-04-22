using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HackControl : MonoBehaviour
{
    public static HackControl Instance;
    Image[] hackScreen;
    private void Awake()
    {
        hackScreen = new Image[SelectScreenConfigs.Instance.playerAmmount];
        for(int i = 0; i < SelectScreenConfigs.Instance.playerAmmount; i++)
        {
            hackScreen[i] = transform.GetChild(i).GetComponent<Image>();
        }
        switch(SelectScreenConfigs.Instance.playerAmmount)
        {
            case 2:
                RectTransformExtensions.SetAllSides(hackScreen[0].rectTransform, -430, -430, -220, 50);
                RectTransformExtensions.SetAllSides(hackScreen[1].rectTransform, -430, -430, 50, -220);
                break;
            case 3:
                RectTransformExtensions.SetAllSides(hackScreen[0].rectTransform, -430, -430, -220, 50);
                RectTransformExtensions.SetAllSides(hackScreen[1].rectTransform, -430, 50, 50, -220);
                RectTransformExtensions.SetAllSides(hackScreen[2].rectTransform, 50, -430, 50, -220);
                break;
            case 4:
                RectTransformExtensions.SetAllSides(hackScreen[0].rectTransform, -430, 50, -220, 50);
                RectTransformExtensions.SetAllSides(hackScreen[1].rectTransform, 50, -430, -220, 50);
                RectTransformExtensions.SetAllSides(hackScreen[2].rectTransform, -430, 50, 50, -220);
                RectTransformExtensions.SetAllSides(hackScreen[3].rectTransform, 50, -430, 50, -220);
                break;
        }
        Instance = this;
    }
    private void Start()
    {

    }
    private List<KartController> casters = new List<KartController>();
    public void TurnOnHackScreen(KartController kartCaster)
    {
        for (int i = 0; i < SelectScreenConfigs.Instance.playerAmmount; i++)
        {
            if (i != kartCaster.playerID)
            {
                hackScreen[i].enabled = true;
                hackScreen[i].GetComponent<Animator>().SetTrigger("Hacked");
            }
        }
        casters.Add(kartCaster);
    }
    public void OnAnimationEnd()
    {
        foreach (KartInfos kartInfos in ManagerPowerUps.Instance.kartList)
        {
            if(kartInfos.kartObject != null)
            {
                if(casters.Count > 0 && casters[0].gameObject != null )
                {
                    if(kartInfos.kartObject != casters[0].gameObject)
                    {
                        Debuff.Instance.AplicarDebuff(kartInfos.kartObject);
                    }
                }
                else
                {
                    Debuff.Instance.AplicarDebuff(kartInfos.kartObject);
                }
            }
        }
        for (int i = 0; i < SelectScreenConfigs.Instance.playerAmmount; i++)
        {
            if (i != casters[0].playerID)
            {
                hackScreen[i].enabled = false;
            }
        }
        AllAnimationEnded();
    }
    int animationsEnded=0;
    private void AllAnimationEnded()
    {
        animationsEnded += 1;
        if(SelectScreenConfigs.Instance.playerAmmount == animationsEnded+1)
        {
            casters.Remove(casters[0]); 
            animationsEnded = 0;
        }
    }
}
