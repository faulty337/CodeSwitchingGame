using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComplexPlay : MonoBehaviour
{
    public GameObject manager, playPanel, Blockpanel;
    public GameObject Lan1, Lan2, pfcard;
    public Button Lan1Btn, Lan2Btn;
    public Text Lan1text, Lan2text;
    public string[] Q, Input;
    public Text question;
    public int totalStage, cardnum;
    public string cardstr;
    private int stageIndex, state, level, cardMargin, cardW, cardH, DistanceW, DistanceH, width, height, cardTouchCount;
    public float startTime, QTime, totalTime, timeStart;
    private float time, empty;
    private bool first, start, QInterval;
    private List<string[]> Data, cardlist;
    
    private List<GameObject> Cards;
    private List<string> Cardstr, cardStrList, cardStrListCopy;
    // Start is called before the first frame update
    void Start()
    {
        cardMargin = 10;
        cardW = (int)pfcard.GetComponent<RectTransform>().rect.width;
        cardH = (int)pfcard.GetComponent<RectTransform>().rect.height;
        DistanceW = cardW + cardMargin;
        DistanceH = cardH + cardMargin;
        width = -(((cardW * 4) + (cardMargin * (4 - 1))) / 2) + (cardW / 2);
        height = (((cardH * 4) + (cardMargin * (4 - 1))) / 2) - (cardH / 2);

    }

    public void StartSetting(int stageSize, int level, List<string[]> Data)
    {
        Blockpanel.SetActive(true);
        Lan1.gameObject.SetActive(false);
        Lan2.gameObject.SetActive(false);
        totalStage = stageSize;
        this.level = level;
        time = 0.0f;
        stageIndex = 0;
        cardTouchCount = 0;
        startTime = 1.0f;
        totalTime = 0.0f;
        timeStart = 0.0f;
        empty = 0.2f;
        QTime = 2.0f;
        first = false;
        state = 0;
        QInterval = true;
        start = false;
        Input = new string[totalStage + 1];
        Q = new string[totalStage];
        this.Data = new List<string[]>();
        this.Data = Data.ConvertAll(s => s);
        StartCoroutine(GameSetting());
    }

    IEnumerator GameSetting()
    {
        cardStrList = new List<string>();
        cardlist = Data.ConvertAll(s => s);
        print(cardlist.Count);
        foreach(string[] str in cardlist){
            cardStrList.Add(str[0]);
        }
        print(cardStrList.Count);
        for (int i = 0; i < Q.Length; i++)
        {
            int ran = Random.Range(0, cardlist.Count);
            Q[i] = cardlist[ran][0];
            cardlist.RemoveAt(ran);
        }
        
        timeStart = 1.0f;
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        totalTime += Time.deltaTime * timeStart;
        time += Time.deltaTime * timeStart;
        if (first)
        {
            if (QInterval)
            {
                if (time > empty)
                {
                    Lan1.gameObject.SetActive(false);
                    Lan2.gameObject.SetActive(false);
                    switch (state)
                    {
                        case 0:
                            NextQuestion_Memory();
                            break;
                        case 1:
                            NextQuestion_AC();
                            break;
                        case 2:
                            StartCoroutine(CardSetting());
                            break;

                    }
                    QInterval = false;
                }
            }
            else
            {
                if (time > QTime)
                {
                    question.text = " ";
                    time = 0.0f;
                    QInterval = true;
                }
            }
        }
        else
        {
            if (start)
            {
                if (time > QTime)
                {
                    first = true;
                    time = 0.0f;
                    question.text = "";
                }
            }
            else
            {
                if (time > startTime)
                {
                    start = true;
                    Blockpanel.SetActive(false);
                    FirstQuestion();
                    time = 0.0f;
                }
            }

        }
    }
    public void ButtonTouch()
    {
        // state = 1;
        if (stageIndex != 0 && (stageIndex+1) % level == 0)
        {
            state = 2;
        }
        else
        {
            state = 0;
        }
        time = 0.0f;
        Lan1Btn.interactable = false;
        Lan2Btn.interactable = false;
        Lan1.gameObject.SetActive(false);
        Lan2.gameObject.SetActive(false);
        question.text = "";
        QInterval = true;

    }

    public void NextQuestion_Memory()
    {
        stageIndex++;
        question.text = Q[stageIndex];
        
        state = 1;
        time = 0.0f;
    }
    public void FirstQuestion()
    {
        question.text = Q[stageIndex];

        state = 1;
        time = 0.0f;
    }
    public void NextQuestion_AC()
    {
        int ran = Random.Range(0, Data.Count);
        question.text = Data[ran][1];
        // if (Random.Range(0, 2) > 0)
        // {
        //     Lan1text.text = Data[ran][2];
        //     Lan2text.text = Data[ran][3];
        // }
        // else
        // {
        //     Lan2text.text = Data[ran][2];
        //     Lan1text.text = Data[ran][3];
        // }
        Lan1text.text = Data[ran][2];
        Lan2text.text = Data[ran][3];

        Lan1.gameObject.SetActive(true);
        Lan2.gameObject.SetActive(true);
        Lan1Btn.interactable = true;
        Lan2Btn.interactable = true;
        if (stageIndex != 0 && (stageIndex+1) % level == 0)
        {
            
            state = 2;
        }
        else
        {
            state = 0;
        }
        time = 0.0f;
    }

    IEnumerator CardSetting()
    {
        timeStart = 0.0f;
        Blockpanel.SetActive(true);
        Cardstr = new List<string>();
        cardStrListCopy = new List<string>(cardStrList);
        // cardStrListCopy = cardStrList.ConvertAll(s => s);
        print(cardStrList.Count);
        // print(stageIndex + "  " + level);
        for (int i = stageIndex - (level-1); i <= stageIndex; i++)
        {
            Cardstr.Add(Q[i]);
            cardStrListCopy.Remove(Q[i]);
        }
        for (int i = level; i < 16; i++)
        {
            int ran = Random.Range(0, cardStrListCopy.Count);
            
            Cardstr.Add(cardStrListCopy[ran]);
            cardStrListCopy.RemoveAt(ran); 
        }
        // print("Cardstr size : " + Cardstr.Count);
        Cards = new List<GameObject>();
        int w = width;
        int h = height;
        for(int i = 0; i < Cardstr.Count; i++){
            print("CardStr : " + Cardstr[i]);
        }
        // for(int i = 0; i < Q.Length; i++){
        //     print("Q : " + Q[i]);
        // }
        for (int i = 0; i < 16; i++)
        {
            var card = Instantiate(pfcard, transform);
            // card.transform.SetParent(CardParent.transform);
            card.GetComponent<AnswerCard>().cardnum = i;
            int ran = Random.Range(0, Cardstr.Count);
            card.GetComponent<AnswerCard>().cardStr = Cardstr[ran];
            print("card"+i+"번 : " + Cardstr[ran]);
            // question[i] = Q[ranIndex[i]][Random.Range(0, Q[ranIndex[i]].Count)];
            Cardstr.RemoveAt(ran);

            card.GetComponent<RectTransform>().anchoredPosition = new Vector2(w, h);
            card.SetActive(true);

            // print(card.GetComponent<RectTransform>().rect.height);
            w = w + DistanceW;
            if ((i + 1) % 4 == 0)
            {
                h = h - DistanceH;
                w = width;
            }
            Cards.Add(card);
        }
        
        Blockpanel.SetActive(false);
        yield return null;
    }

    public void CardTouch(int cardIndex, string cardStr)
    {
        Input[cardTouchCount] = cardStr;
        print(cardTouchCount + "  " + cardStr);
        cardTouchCount++;
        if (cardTouchCount % level == 0)
        {
            if(cardTouchCount >= totalStage){
                CardClear();
                if(GameManager.state == 10){
                    manager.GetComponent<ComplexManager>().GameEnd();
                }else{
                    manager.GetComponent<ComplexManager>().PracticeGameEnd();
                }
                
            }else{
                CardClear();
                state = 0;
                time = 0.0f;
                timeStart = 1.0f;
            }
        }
        
    }

    public void CardClear()
    {
        for (int i = 0; i < Cards.Count; i++)
        {
            Cards[i].SetActive(false);
        }
        Cards.Clear();
    }


}
