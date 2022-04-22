using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishedScreen : MonoBehaviour
{
    public static FinishedScreen Instance;
    [System.NonSerialized] public GameObject[] finishedScreen;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        finishedScreen = new GameObject[SelectScreenConfigs.Instance.playerAmmount];
        for (int i = 0; i < SelectScreenConfigs.Instance.playerAmmount; i++)
        {
            finishedScreen[i] = transform.GetChild(i).gameObject;
        }
        switch (SelectScreenConfigs.Instance.playerAmmount)
        {
            case 2:
                RectTransformExtensions.SetAllSides(finishedScreen[0].GetComponent<RectTransform>(), -430, -430, -220, 50);
                RectTransformExtensions.SetAllSides(finishedScreen[1].GetComponent<RectTransform>(), -430, -430, 50, -220);
                break;
            case 3:
                RectTransformExtensions.SetAllSides(finishedScreen[0].GetComponent<RectTransform>(), -430, -430, -220, 50);
                RectTransformExtensions.SetAllSides(finishedScreen[1].GetComponent<RectTransform>(), -430, 50, 50, -220);
                RectTransformExtensions.SetAllSides(finishedScreen[2].GetComponent<RectTransform>(), 50, -430, 50, -220);
                break;
            case 4:
                RectTransformExtensions.SetAllSides(finishedScreen[0].GetComponent<RectTransform>(), -430, 50, -220, 50);
                RectTransformExtensions.SetAllSides(finishedScreen[1].GetComponent<RectTransform>(), 50, -430, -220, 50);
                RectTransformExtensions.SetAllSides(finishedScreen[2].GetComponent<RectTransform>(), -430, 50, 50, -220);
                RectTransformExtensions.SetAllSides(finishedScreen[3].GetComponent<RectTransform>(), 50, -430, 50, -220);
                break;
        }
    }
}
