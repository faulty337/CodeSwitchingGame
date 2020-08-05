using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameplay : MonoBehaviour
{
    public GameObject manager, playPanel, blockpanel;
    public GameObject pfcard;
    private int totalCard, thCount, x, y, width, height, cardW, cardH, intervalW, intervalH;
    private bool check = true;
    private List<string[]> Data= new List<string[]>();
    private List<List<string>> Q = new List<List<string>>();
    private List<int> index, ranIndex = new List<int>();
    private List<GameObject> Cards = new List<GameObject>();
    public int cardnum, lastcardnum;
    public bool state; //true면 카드 open안된상태, false면 다른 카드가 open된 상태

    // Start is called before the first frame update
    void Start()
    {
        cardW = (int)pfcard.GetComponent<RectTransform>().rect.width;
        cardH = (int)pfcard.GetComponent<RectTransform>().rect.height;
       
        switch (2)//gmamanager.Level
        {
            case 1 :
                x = 3;
                y = 3;
                break;
            case 2:
                x = 4;
                y = 4;
                break;
            case 3:
                x = 5;
                y = 5;
                break;
        }
        //화면 비율 다시 맞추기(카드 각 공간은 고정으로)
        intervalW = ((1080 - (cardW * x)) / (x + 1));
        intervalH = (600 - (cardH * y)) / (y+1);
        width = -540 + (intervalW + (cardW / 2));
        height = 300 - (intervalH + (cardH / 2));
        totalCard = ((x * y) / 2)*2;
        Data = manager.GetComponent<CardManager>().Data.ConvertAll(s => s);
       
        index = new List<int>();
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
        for (int i = 0; i< totalCard; i++)//단어 랜덤 추출(중복없이)
        {
            
            int ran = Random.Range(0, Data.Count);
            List<string> ca = new List<string>();
            ca.Add(Data[ran][0]);
            ca.Add(Data[ran][1]);
            Q.Add(ca);
            Data.RemoveAt(ran);
        }
        int w = width;
        for (int i = 0; i < totalCard; i++)
        {

            var card = Instantiate(pfcard);
            card.transform.SetParent(playPanel.transform);
            card.GetComponent<CardScript>().cardnum = i;
            card.GetComponent<CardScript>().cardindex = ranIndex[i];
            card.GetComponent<CardScript>().Cardstr = Q[ranIndex[i]][Random.Range(0, Q[ranIndex[i]].Count)].ToString();
            Q[ranIndex[i]].Remove(card.GetComponent<CardScript>().Cardstr);
            card.GetComponent<RectTransform>().anchoredPosition = new Vector3(w, height, 0);
            card.SetActive(true);
            w = w + (cardW + intervalW);
            if ((i+1)%x == 0)
            {
                height = height - (cardH + intervalH);
                w = width;

            }
            Cards.Add(card);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
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

        }
        else
        {
            Cards[cardnum].GetComponent<CardScript>().CardOpen();
            if (Cards[lastcardnum].GetComponent<CardScript>().cardindex == Cards[cardnum].GetComponent<CardScript>().cardindex)
            {
                Cards[cardnum].GetComponent<CardScript>().Finsh();
                Cards[lastcardnum].GetComponent<CardScript>().Finsh();
            }
            else
            {
                blockpanel.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                Cards[cardnum].GetComponent<CardScript>().CardClose();
                Cards[lastcardnum].GetComponent<CardScript>().CardClose();
                blockpanel.SetActive(false);
            }

        }
        
    }
}
