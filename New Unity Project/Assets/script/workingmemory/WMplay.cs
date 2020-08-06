using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WMplay : MonoBehaviour
{
    public GameObject manager, playPanel, blockpanel;
    public GameObject pfcard;
    private int totalCard, thCount, x, y, width, height, cardW, cardH, DistanceW, DistanceH, cardMargin;
    public Text count;
    private List<string[]> Data= new List<string[]>();
    private List<List<string>> Q = new List<List<string>>();
    private List<int> index, ranIndex = new List<int>();
    private List<GameObject> Cards = new List<GameObject>();
    public int cardnum, lastcardnum, touchCount, failCount, passCount;
    private float time;
    public bool state; //true면 카드 open안된상태, false면 다른 카드가 open된 상태

    // Start is called before the first frame update
    void Start()
    {
        cardW = (int)pfcard.GetComponent<RectTransform>().rect.width;
        cardH = (int)pfcard.GetComponent<RectTransform>().rect.height;
        cardMargin = 10;
       
        switch (1)//GameManager.Level
        {
            case 1 :
                x = 2;
                y = 3;
                break;
            case 2:
                x = 4;
                y = 3;
                break;
            case 3:
                x = 4;
                y = 4;
                break;
        }
        //화면 비율 다시 맞추기(카드 각 공간은 고정으로)
        DistanceW = cardW + cardMargin;
        DistanceH = cardH + cardMargin;
        width = (-Screen.width/2) + ((Screen.width - ((cardW * x) + (cardMargin * (x - 1))))/2)+cardW/2;
        height = (Screen.height / 2) - ((Screen.height - ((cardH * y) + (cardMargin * (y - 1))))/2)-cardH/2;
        totalCard = ((x * y) / 2)*2;
        Data = manager.GetComponent<WMManager>().Data.ConvertAll(s => s);
       
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
            card.GetComponent<RectTransform>().anchoredPosition = new Vector2(w, height);
            card.SetActive(true);
            w = w + DistanceW;
            if ((i+1)%x == 0)
            {
                height = height - DistanceH;
                w = width;

            }
            Cards.Add(card);
        }
    }


    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
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
            Cards[cardnum].GetComponent<CardScript>().CardOpen();
            if (Cards[lastcardnum].GetComponent<CardScript>().cardindex == Cards[cardnum].GetComponent<CardScript>().cardindex)
            {
                Cards[cardnum].GetComponent<CardScript>().Finsh();
                Cards[lastcardnum].GetComponent<CardScript>().Finsh();
                passCount++;
                count.text = passCount.ToString();
                if(passCount == totalCard / 2)
                {
                    manager.GetComponent<WMManager>().GameEnd(failCount, touchCount, time);
                }
            }
            else
            {
                blockpanel.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                Cards[cardnum].GetComponent<CardScript>().CardClose();
                Cards[lastcardnum].GetComponent<CardScript>().CardClose();
                blockpanel.SetActive(false);
                failCount++;
            }

        }
        
    }
}
