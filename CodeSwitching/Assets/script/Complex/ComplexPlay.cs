using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComplexPlay : MonoBehaviour
{
    public GameObject manager, playPanel, Blockpanel;
    public GameObject Lan1, Lan2, pfcard;
    public Text Lan1Btn, Lan2Btn;
    public string[] Q;
    public Text question;
    public int totalStage;
    private int stageSize, stageIndex, state, level, cardMargin, cardW, cardH, DistanceW, DistanceH, width, height;
    public float startTime, QTime, totalTime, timeStart;
    private float time, empty;
    private bool first, start, QInterval;
    private List<string[]> Data;
    // Start is called before the first frame update
    void Start()
    {
        cardMargin = 20;
        cardW = (int)pfcard.GetComponent<RectTransform>().rect.width;
        cardH = (int)pfcard.GetComponent<RectTransform>().rect.height;
        DistanceW = cardW + cardMargin;
        DistanceH = cardH + cardMargin;
        width = -(((cardW * 4) + (cardMargin * (4 - 1)))/2)+(cardW/2);
        height = (((cardH * 4) + (cardMargin * (4 - 1)))/2)-(cardH/2);

    }

    public void StartSetting(int stageSize, int level){
        Blockpanel.SetActive(true);
        Lan1.gameObject.SetActive(false);
        Lan2.gameObject.SetActive(false);
        this.stageSize = stageSize;
        this.level = level;
        time = 0.0f;
        stageIndex = 0;
        startTime = 1.0f;
        totalTime = 0.0f;
        timeStart = 0.0f;
        empty = 0.2f;
        QTime = 2.0f;
        first = false;
        state = 0;
        QInterval = true;
        start = false;
        Q = new string[stageSize];
        Data = new List<string[]>();
        StartCoroutine(GameSetting());
    }

    IEnumerator GameSetting(){
        Data = manager.GetComponent<ComplexManager>().Data.ConvertAll(s => s);
        for(int i = 0; i<Data.Count; i++){
            print(Data[i][0]);
            print(Data[i][1]);
            print(Data[i][2]);
        }
        for(int i = 0; i < Q.Length; i++){
            Q[i] = Data[Random.Range(0, Data.Count)][0];
        }
        timeStart = 1.0f;
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        totalTime += Time.deltaTime;
        time += Time.deltaTime;
        if(first){
            if(QInterval){
                if(time > empty){
                    switch(state){
                        case 0:
                            NextQuestion_Memory();
                            break;
                        case 1:
                            NextQuestion_AC();
                            break;
                        case 2:
                            break;

                    }
                    QInterval = false;
                }
            }else{
                if(time > QTime){
                    question.text = " ";
                    time = 0.0f;
                    QInterval = true;
                }
            }
        }else{
            if(start){
                if(time > QTime){
                    first = true;
                    time = 0.0f;
                    question.text = "";
                }
            }else{
                if(time > startTime){
                    start = true;
                    Blockpanel.SetActive(false);
                    NextQuestion_Memory();
                    time = 0.0f;
                }
            }
            
        }
    }
    public void ButtonTouch(){
        // state = 1;
        time = 0.0f;

    }

    public void NextQuestion_Memory(){
        stageIndex++;
        question.text = Q[stageIndex];
        Lan1.gameObject.SetActive(false);
        Lan2.gameObject.SetActive(false);
        state = 1;
        time = 0.0f;
    }
    public void FirstQuestion(){
        question.text = Q[stageIndex];
        Lan1.gameObject.SetActive(false);
        Lan2.gameObject.SetActive(false);
        state = 1;
        time = 0.0f;
    }
    public void NextQuestion_AC(){
        int ran = Random.Range(0, Data.Count);
        question.text = Data[ran][1];
        if(Random.Range(0,2) > 0){
            Lan1Btn.text = Data[Random.Range(0, Data.Count)][2];
            Lan2Btn.text = Data[ran][2];
        }else{
            Lan2Btn.text = Data[Random.Range(0, Data.Count)][2];
            Lan1Btn.text = Data[ran][2];
        }
        
        Lan1.gameObject.SetActive(true);
        Lan2.gameObject.SetActive(true);
        if(stageIndex % level == 0){
            state = 2;
        }else{
            state = 0;
        }
        time = 0.0f;
    }

    void CardSetting(){
        for (int i = 0; i < 16; i++)
        {
            var card = Instantiate(pfcard, transform);
            // card.transform.SetParent(playPanel.transform);
            card.GetComponent<CardScript>().cardnum = i;
            // card.GetComponent<CardScript>().cardindex = ranIndex[i];
            // int ran = Random.Range(0, Q[ranIndex[i]].Count);
            // card.GetComponent<CardScript>().Cardstr = Q[ranIndex[i]][ran];
            // question[i] = Q[ranIndex[i]][Random.Range(0, Q[ranIndex[i]].Count)];
            // Q[ranIndex[i]].RemoveAt(ran);
            
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
    }


}
