using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsSelectScreen : MonoBehaviour
{
    GameObject SelectPlayerAmmountScreen;
    GameObject SelectKartScreen;

    GameObject[] selectKartScreen_PlayerIcon;
    private void Start()
    {
        GameObject canvas = GameObject.Find("Canvas");
        SelectKartScreen = canvas.transform.Find("SelectKartScreen").gameObject;
        SelectKartScreen.SetActive(false);
        SelectPlayerAmmountScreen = canvas.transform.Find("SelectPlayerAmmountScreen").gameObject;
        SelectPlayerAmmountScreen.SetActive(true);
        selectKartScreen_PlayerIcon = new GameObject[4];
        Transform players = SelectKartScreen.transform.Find("Players"); 
        for(int i = 0; i < players.childCount; i++)
        {
            selectKartScreen_PlayerIcon[i] = players.GetChild(i).gameObject;
        }
        foreach(GameObject p in selectKartScreen_PlayerIcon)
        {
            Debug.Log(p.name);  
        }
    }
    public void KartSelect(int kartId)
    {
        Debug.Log(kartId);
    }
    public void PlayerAmmountSelect(int ammount)
    {
        SelectScreenConfigs.Instance.playerAmmount = ammount;
        SelectPlayerAmmountScreen.SetActive(false);
        for(int i=3; i>=ammount; i--)
        {
            selectKartScreen_PlayerIcon[i].SetActive(false);
        }
        SelectKartScreen.gameObject.SetActive(true);
    }
}
