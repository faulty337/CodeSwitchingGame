using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerCard : MonoBehaviour
{
    public int cardIndex, cardnum;
    public GameObject playmanager;
    
    public Text Cardtext;
    public string cardStr ="";
    public Button Card;
    public bool isOpen = false; //false면 뒤집어져 있는 상태, true면 펼쳐져 있는 상태

    void Start(){
        
        Cardtext.text = cardStr;
    }

    public void CardClick(){
        playmanager.GetComponent<ComplexPlay>().CardTouch(cardIndex, cardStr);
        
        gameObject.SetActive(false);
    }
}
