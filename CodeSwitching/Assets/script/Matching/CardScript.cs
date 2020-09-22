using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    public int cardindex, cardnum;
    public GameObject manager, playmanager;
    
    public Text Cardtext;
    public string Cardstr ="카드";
    public Button Card;
    public Image CardImg;
    public Sprite frontImg;
    public Sprite backImg;
    public bool isOpen = false; //false면 뒤집어져 있는 상태, true면 펼쳐져 있는 상태

    private float delaySpeed = 0.05f;
    

    private void Start()
    {
        
    }
    // Start is called before the first frame update
    public void CardClick()
    {
        Card.interactable = false;
        playmanager.GetComponent<WMplay>().cardnum = cardnum;
        playmanager.GetComponent<WMplay>().state = !playmanager.GetComponent<WMplay>().state;
    }
    public void CardOpen()
    {
        StartCoroutine(CardOpenAn());
    }
    IEnumerator CardOpenAn(){
        // while(transform.eulerAngles.y < 90){
        //     transform.Rotate(new Vector3(0, 45.0f, 0));
        //     yield return new WaitForSeconds(delaySpeed);
        // }
        for(int i = 0; i < 3; i++){
            transform.Rotate(new Vector3(0, 30.0f, 0));
            yield return new WaitForSeconds(0.02f);
        }
        
        Cardtext.text = Cardstr;
        Card.interactable = false;
        CardImg.sprite = frontImg;
        transform.Rotate(new Vector3(0, 180, 0));
        // while(transform.eulerAngles.y < 360){
        //     transform.Rotate(new Vector3(0, 45.0f, 0));
        //     yield return new WaitForSeconds(delaySpeed);
        // }
        for(int i = 0; i < 3; i++){
            transform.Rotate(new Vector3(0, 30.0f, 0));
            yield return new WaitForSeconds(0.02f);
        }
        transform.eulerAngles = new Vector3(0, 0, 0);
        yield return null;
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
