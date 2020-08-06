using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    public int rotatespeed = 2;
    public int cardindex, cardnum;
    public GameObject manager;
    public GameObject playmanager;
    public Text Cardtext;
    public string Cardstr ="카드";
    public Button Card;
    public Image CardImg;
    public Sprite frontImg;
    public Sprite backImg;
    public bool isOpen = false; //false면 뒤집어져 있는 상태, true면 펼쳐져 있는 상태

    private void Start()
    {
        
    }
    // Start is called before the first frame update
    public void CardClick()
    {

        playmanager.GetComponent<WMplay>().cardnum = cardnum;
        playmanager.GetComponent<WMplay>().state = !playmanager.GetComponent<WMplay>().state;
    }
    public void CardOpen()
    {
        
        CardImg.sprite = frontImg;
        Cardtext.text = Cardstr;
        Card.interactable = false;
    }
    

    public void CardClose()
    {
        CardImg.sprite = backImg;
        Cardtext.text = "";
        Card.interactable = true;
    }

    public void Finsh()
    {
        CardImg.sprite = frontImg;
        Card.interactable = false;
    }
}
