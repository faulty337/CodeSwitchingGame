using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StroopPlay : MonoBehaviour
{
    public Text Question, Score;

    public Button redBtn, greenBtn, blueBtn, yellowBtn;
    public GameObject manager, blockPanel;
    public string[] color = {"빨강", "노랑", "초록", "파랑"};
    public string[] colorstr = {"빨간", "노란", "초록", "파란"};
    public string[] colorNum;
    public int[,] QuestionIndex;
    public int stage, TotalStage, ScoreCount;
    private int level;
    public List<string[]> Data = new List<string[]>();
    public string[] reactionTime, Q, input, Answer;
    public float time, QTime, totalTime,timeStart, startTime, empty;
    public bool start, QInterval, first;
    // Start is called before the first frame update
    public void Start()
    {
        QTime = 2.0f;
        startTime = 1.0f;
        
    }

    public void StartSetting(){
        blockPanel.SetActive(true);
        
        StartCoroutine(GameSetting());
        
    }
    IEnumerator GameSetting(){
        colorNum[0] = "<color=#bf2836>";//빨강
        colorNum[1] = "<color=#f3c500>";//노랑
        colorNum[2] = "<color=#0c7b3f>";//초록
        colorNum[3] = "<color=#004e9e>";//파랑
        TotalStage = 40;
        timeStart = 0.0f;
        QuestionIndex = new int[TotalStage+1, 2];
        Question.text = "";
        Score.text = "";
        ScoreCount = 0;
        totalTime = 0.0f;
        time = 0.0f;
        start = false;
        first = false;
        QInterval = true;
        empty = 0.2f;
        stage = 0;
        level = GameManager.Level;
        //{"<color=#bf2836>", "<color=#f3c500>", "<color=#0c7b3f>", "<color=#004e9e>"};
        Data = manager.GetComponent<StroopManager>().Data.ConvertAll(s => s);
        
        reactionTime = new string[TotalStage+1];
        Q = new string[TotalStage+1];
        input = new string[TotalStage+1];
        Answer = new string[TotalStage+1];
        
        for(int i = 0; i<TotalStage; i++){
            QuestionMaking(i);
        }
        timeStart = 1.0f;
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        totalTime += Time.deltaTime * timeStart;
        time += Time.deltaTime * timeStart;
        if(first){
            if(QInterval){
                if(time > empty){
                    NextQuestion();
                    QInterval = false;
                    redBtn.interactable = true;
                    blueBtn.interactable = true;
                    greenBtn.interactable = true;
                    yellowBtn.interactable = true;
                    time = 0.0f;
                }
            }else{
                if(time > QTime){
                    Question.text = " ";
                    time = 0.0f;
                    QInterval = true;
                    
                }
            }
        }else{
            if(start){
                if(time > QTime){
                    first = true;
                    time = 0.0f;
                    Question.text = "";
                }
            }else{
                if(time > startTime){
                    blockPanel.SetActive(false);
                    FirstQuestion();
                    start = true;
                    time = 0.0f;
                }
            }
            
        }

    }

    public void ButtonIndex(int index){
        input[stage] = color[index];
        reactionTime[stage] = time.ToString();
        if(color[index] == Answer[stage]){
            ScoreCount++;
            Score.text = ScoreCount.ToString();
        }
        
        Question.text = "";
        QInterval = true;
        time = 0.0f;
        first = true;
        redBtn.interactable = false;
        blueBtn.interactable = false;
        greenBtn.interactable = false;
        yellowBtn.interactable = false;
        // NextQuestion();
    }

    public void NextQuestion(){
        stage++;
        Question.text = colorNum[QuestionIndex[stage, 0]] +colorstr[QuestionIndex[stage, 1]]+" "+Q[stage]+"</color>";
        if(stage >= TotalStage){
            manager.GetComponent<StroopManager>().gameEnd();
        }
    }
    public void QuestionMaking(int st){
        int ran = Random.Range(0,Data.Count);
        int ran2 = Random.Range(0,2);
        int fake = Random.Range(0,4);
        int ans = Random.Range(0,4);
        QuestionIndex[st, 1] = fake;
        QuestionIndex[st, 0] = ans;
       
        input[st] = "Pass";
        Q[st] = Data[ran][ran2];
        switch (level){
            case 1 :
                Answer[st] = color[ans];
                break;
            case 2 :
                if(ran2 == 1)
                    Answer[st] = color[ans];
                else
                    Answer[st] = "Pass";
                break;
            case 3 :
                if(ran2 == 0)
                    Answer[st] = color[ans];
                else
                    Answer[st] = "Pass";
                break;
        }
    }
    public void FirstQuestion(){
        Question.text = colorNum[QuestionIndex[stage, 0]]+colorstr[QuestionIndex[stage, 1]]+" "+Q[stage]+"</color>";//QuestionIndex[stage, 0]
        if(stage >= TotalStage){
            manager.GetComponent<StroopManager>().gameEnd();
        }
    }

}
