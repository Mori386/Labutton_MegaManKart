using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsSelectScreen : MonoBehaviour
{
    GameObject SelectPlayerAmmountScreen;
    GameObject SelectKartScreen;

    GameObject[] selectKartScreen_PlayerIcons;
    GameObject[] selectKartScreen_KartIcons;
    private int playerSelecting=0;
    private void Start()
    {
        GameObject canvas = GameObject.Find("Canvas");
        SelectKartScreen = canvas.transform.Find("SelectKartScreen").gameObject;
        SelectKartScreen.SetActive(false);
        SelectPlayerAmmountScreen = canvas.transform.Find("SelectPlayerAmmountScreen").gameObject;
        SelectPlayerAmmountScreen.SetActive(true);
        selectKartScreen_PlayerIcons = new GameObject[4];
        Transform players = SelectKartScreen.transform.Find("Players"); 
        for(int i = 0; i < players.childCount; i++)
        {
            selectKartScreen_PlayerIcons[i] = players.GetChild(i).gameObject;
        }
        foreach(GameObject p in selectKartScreen_PlayerIcons)
        {
            Debug.Log(p.name);  
        }
        selectKartScreen_KartIcons = new GameObject[4];
        for(int i =0;i<4;i++)
        {
            selectKartScreen_KartIcons[i] = SelectKartScreen.transform.Find("KartButtons").GetChild(i).gameObject;
        }
    }
    public void KartSelect(int kartId)
    {
        SelectScreenConfigs.Instance.kartIDPlayer[playerSelecting] = kartId;
        selectKartScreen_KartIcons[kartId - 1].GetComponent<Image>().color = Color.black;
        selectKartScreen_KartIcons[kartId - 1].GetComponent<Button>().enabled = false;
        KartSelectInfos kartSelectInfos = selectKartScreen_KartIcons[kartId - 1].GetComponent<KartSelectInfos>();
        RenderControl.Instance.kartRenders.Remove(kartSelectInfos.kartRendered);
        kartSelectInfos.kartRendered.transform.localPosition = new Vector3(-700, -380, 0);
        Destroy(kartSelectInfos);
        playerSelecting++;
    }
    public void PlayerAmmountSelect(int ammount)
    {
        SelectScreenConfigs.Instance.definePlayerAmmount(ammount);
        SelectPlayerAmmountScreen.SetActive(false);
        for(int i=3; i>=ammount; i--)
        {
            selectKartScreen_PlayerIcons[i].SetActive(false);
        }
        SelectKartScreen.gameObject.SetActive(true);
    }
}
