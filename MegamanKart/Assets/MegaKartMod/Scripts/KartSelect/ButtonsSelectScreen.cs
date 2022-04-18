using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        selectKartScreen_KartIcons = new GameObject[4];
        for(int i =0;i<4;i++)
        {
            selectKartScreen_KartIcons[i] = SelectKartScreen.transform.Find("KartButtons").GetChild(i).gameObject;
        }
    }
    public void KartSelect(int kartId)
    {
        SelectScreenConfigs.Instance.kartIDPlayer[playerSelecting] = kartId;
        selectKartScreen_KartIcons[kartId].GetComponent<Image>().color = Color.black;
        selectKartScreen_KartIcons[kartId].GetComponent<Button>().enabled = false;
        KartSelectInfos kartSelectInfos = selectKartScreen_KartIcons[kartId].GetComponent<KartSelectInfos>();
        RenderControl.Instance.kartRenders.Remove(kartSelectInfos.kartRendered);
        Vector3 position = Vector3.zero;
        switch(playerSelecting)
        {
            case 0:position = new Vector3(-700, -380, 0);break;
            case 1:position = new Vector3(-250, -380, 0);break;
            case 2:position = new Vector3(250, -380, 0);break;
            case 3:position = new Vector3(700, -380, 0); break;
        }
        kartSelectInfos.kartRendered.transform.localPosition = position;
        kartSelectInfos.kartRendered.transform.localScale = kartSelectInfos.kartRendered.transform.localScale*0.8f;
        Destroy(kartSelectInfos);
        playerSelecting++;
        if(playerSelecting > SelectScreenConfigs.Instance.playerAmmount-1)
        {
            SceneManager.LoadScene("MainScene 1");
        }
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
