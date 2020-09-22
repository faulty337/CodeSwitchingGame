﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WMplay : MonoBehaviour
{
    public GameObject manager, playPanel, blockpanel;
    public GameObject pfcard;
    private int thCount, x, y, width, height, cardW, cardH, DistanceW, DistanceH, cardMargin;
    public Text count;
    private List<string[]> Data;
    private List<List<string>> Q ;
    private List<int> index, ranIndex;
    private List<GameObject> Cards;
    public int cardnum, totalCard, lastcardnum, touchCount, failCount, passCount;
    public string[] question;
    public float totaltime, startTime;
    public bool state; //true면 카드 open안된상태, false면 다른 카드가 open된 상태

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartSetting(int level){
        blockpanel.SetActive(true);
        passCount = 0;
        count.text = "0";
        switch (level)
        {
            case 1 :
                x = 3;
                y = 2;
                break;
            case 2:
                x = 4;
                y = 3;
                break;
            case 3:
                x = 4;
                y = 4;
                cardMargin = 10;
                break;
        }
        StartCoroutine(GameSetting());
    }

    IEnumerator GameSetting(){
        totaltime = 0.0f;
        startTime = 0.0f;
        
        touchCount = 0;
        cardMargin = 20;
        cardW = (int)pfcard.GetComponent<RectTransform>().rect.width;
        cardH = (int)pfcard.GetComponent<RectTransform>().rect.height;
        
        //화면 비율 다시 맞추기(카드 각 공간은 고정으로)
        DistanceW = cardW + cardMargin;
        DistanceH = cardH + cardMargin;
        // width = (-Screen.width/2) + ((Screen.width - ((cardW * x) + (cardMargin * (x - 1))))/2)+cardW/2;
        width = -(((cardW * x) + (cardMargin * (x - 1)))/2)+(cardW/2);
        // height = (Screen.height / 2) - ((Screen.height - ((cardH * y) + (cardMargin * (y - 1))))/2)-cardH/2;
        height = (((cardH * y) + (cardMargin * (y - 1)))/2)-(cardH/2) - 30;
        totalCard = ((x * y) / 2)*2;
        question = new string[totalCard];
        Data = new List<string[]>();
        Data = manager.GetComponent<WMManager>().Data.ConvertAll(s => s);
        ranIndex = new List<int>();
        index = new List<int>();
        Q = new List<List<string>>();
        for(int i = 0; i<totalCard/2; i++) //초기화
        {
            index.Add(i);
            index.Add(i);

        }
        
        for (int i = 0; i < totalCard; i++) //문제 index
        {
            int ran = Random.Range(0, index.Count);
            ranIndex.Add(index[ran]);
            index.RemoveAt(ran);
        }
        for (int i = 0; i< totalCard/2; i++)//단어 랜덤 추출(중복없이)
        {
            
            int ran = Random.Range(0, Data.Count);
            List<string> ca = new List<string>();
            ca.Add(Data[ran][0]);
            ca.Add(Data[ran][1]);
            Q.Add(ca);
            Data.RemoveAt(ran);
        }
        int w = width;
        Cards = new List<GameObject>();
        for (int i = 0; i < totalCard; i++)
        {
            var card = Instantiate(pfcard, transform);
            // card.transform.SetParent(playPanel.transform);
            card.GetComponent<CardScript>().cardnum = i;
            card.GetComponent<CardScript>().cardindex = ranIndex[i];
            int ran = Random.Range(0, Q[ranIndex[i]].Count);
            card.GetComponent<CardScript>().Cardstr = Q[ranIndex[i]][ran];
            question[i] = Q[ranIndex[i]][Random.Range(0, Q[ranIndex[i]].Count)];
            Q[ranIndex[i]].RemoveAt(ran);
            
            card.GetComponent<RectTransform>().anchoredPosition = new Vector2(w, height);
            card.SetActive(true);
            
            print(card.GetComponent<RectTransform>().rect.height);
            w = w + DistanceW;
            if ((i+1)%x == 0)
            {
                height = height - DistanceH;
                w = width;
            }
            Cards.Add(card);
        }
        startTime = 1.0f;
        blockpanel.SetActive(false);
        yield return null;
    }


    // Update is called once per frame
    void Update()
    {
        totaltime += Time.deltaTime * startTime;
    }

    public void Turn()
    {
        StartCoroutine(WaitForIt());
    }
    IEnumerator WaitForIt()
    {
        
        if (state)
        {
            lastcardnum = cardnum;
            Cards[cardnum].GetComponent<CardScript>().CardOpen();
            touchCount++;

        }
        else
        {
            blockpanel.SetActive(true);
            Cards[cardnum].GetComponent<CardScript>().CardOpen();
            // yield return new WaitForSeconds(0.5f);
            if (Cards[lastcardnum].GetComponent<CardScript>().cardindex == Cards[cardnum].GetComponent<CardScript>().cardindex)
            {
                Cards[cardnum].GetComponent<CardScript>().Finsh();
                Cards[lastcardnum].GetComponent<CardScript>().Finsh();
                passCount++;
                count.text = passCount.ToString();
                if(passCount == totalCard / 2)
                {
                    yield return new WaitForSeconds(1.0f);
                    Carddel();
                    startTime = 0.0f;
                    
                    manager.GetComponent<WMManager>().GameEnd();
                }
                blockpanel.SetActive(false);
            }
            else
            {
                
                yield return new WaitForSeconds(0.5f);
                Cards[cardnum].GetComponent<CardScript>().CardClose();
                Cards[lastcardnum].GetComponent<CardScript>().CardClose();
                blockpanel.SetActive(false);
                
                failCount++;
            }

        }
        
    }
    public void Carddel(){
        for(int i = 0; i < Cards.Count; i++){
            Destroy(Cards[i]);
        }
        Cards.Clear();
    }
}
