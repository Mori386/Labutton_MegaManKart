using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerPowerUps : MonoBehaviour
{
    public GameObject prefabMissel;
    public GameObject prefabShield;
    public GameObject prefabBlade;
    public GameObject prefabLandMine;

    public Sprite Img_Empty;
    public Sprite Img_Missel;
    public Sprite Img_Shield;
    public Sprite Img_Blade;
    public Sprite Img_LandMine;
    public Sprite Img_Hack;

    public static ManagerPowerUps Instance;
    public List<KartInfos> kartList;
    private void Awake()
    {
        Instance = this;
        kartList = new List<KartInfos>();
    }
}
