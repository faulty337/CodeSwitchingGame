using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Simonplay : MonoBehaviour
{
    public GameObject manager, blockPanel;
    public Button RightBtn, LeftBtn;
    public Text Len1Button, Len2Button;
    public float time, Qtime, totalTime, timestart, empty, startTime;
    public Text Question, Score;
    public bool start, QInterval, first;
    private List<string[]> Data = new List<string[]>();
    private int stage, level, ScoreCount;
    public int TotalStage;
    public string[] input, Answer, Compatible, position, reactionTime, Q, direction;
    
    private TextAnchor[] alignment;
    
    // Start is called before the first frame update
    public void Start()
    {  
        
    }

    public void GameStart(int level, int totalStage, List<string[]> Data){
        timestart = 0.0f;
        empty = 0.2f;
        Question.text = "";
        Score.text = "0";
        ScoreCount = 0;
        QInterval = true;
        first = false;
        stage = 0;
        TotalStage = totalStage;
        time = 0.0f;
        totalTime = 0.0f;
        start = false;
        startTime = 1.0f;
        this.Data = Data.ConvertAll(s => s);
        switch(level){
            case 1:
                this.level = 3;
                break;
            case 2:
                this.level = 5;
                break;
            case 3:
                this.level = 7;
                break;
            default:
                this.level = 3;
                break;
        }
        StartCoroutine(GameSetting());
        
    }

    IEnumerator GameSetting(){
        
        blockPanel.SetActive(true);
        Qtime = 2.0f;
        input = new string[TotalStage+1];
        Answer = new string[TotalStage+1];
        Compatible = new string[TotalStage+1];
        position = new string[TotalStage+1];
        reactionTime = new string[TotalStage+1];
        alignment = new TextAnchor[2] {TextAnchor.MiddleLeft, TextAnchor.MiddleRight};
        Q = new string[TotalStage+1];
        direction = new string[2] {"왼쪽","오른쪽"};
        
        
        for(int i = 0; i < TotalStage; i++){
            
            QuestionMaking(i);
        }
        print(Q.Length);
        timestart = 1.0f;
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        totalTime += Time.deltaTime * timestart;
        time += Time.deltaTime * timestart;
        if(first){
            if(QInterval){
                if(time > empty){
                    NextQuestion();
                    time = 0.0f;
                    QInterval = false;
                    RightBtn.interactable = true;
                    LeftBtn.interactable = true;
                }
            }else{
                if(time > Qtime){
                    Question.text = " ";
                    time = 0.0f;
                    QInterval = true;
                }
            }
            
        }else{
            if(start){
                if(time > Qtime){
                    first = true;
                    time = 0.0f;
                    Question.text = "";
                }
            }else{
                if(time > startTime){
                    blockPanel.SetActive(false);
                    startsatting();
                    start = true;
                    time = 0.0f;
                }
            }
        }
    }

    public void LanButton(int index){
        reactionTime[stage] = time.ToString();
        input[stage] = direction[index];
        if(direction[index] == Answer[stage]){
            ScoreCount++;
            Score.text = ScoreCount.ToString();
        }
        Question.text = "";
        QInterval = true;
        time = 0.0f;
        first = true;
        RightBtn.interactable = false;
        LeftBtn.interactable = false;
        // NextQuestion();
    }



    public void NextQuestion(){
        stage++;
        time = 0.0f;
        Question.text = Q[stage];
        if(position[stage] == "왼쪽"){
            Question.alignment = alignment[0];
        }else{
            Question.alignment = alignment[1];
        }
        
        if(stage >= TotalStage){
            if(GameManager.state == 10){
                manager.GetComponent<SimonManager>().gameEnd();
            }else{
                manager.GetComponent<SimonManager>().PracticeGameEnd();
            }
            
        }
    }
    public void QuestionMaking(int st){
        input[st] = "Pass";
        reactionTime[st] = "1";
        int ran = Random.Range(0,Data.Count);
        int ran2 = Random.Range(0,2);
        int ran3 = Random.Range(0,2);
        if(Random.Range(1, 11) > level){
            // Question.alignment = alignment[ran2];
            // print("direction :" + direction[ran2] + "  " + ran2);
            position[st] = direction[ran2];
            Compatible[st] = "compatible";
        }else{
            Compatible[st] = "incompatible";
            if(ran2 == 0){
                // Question.alignment = alignment[1];
                position[st] = direction[1];
            }else{
                // Question.alignment = alignment[0];
                position[st] = direction[0];
            }
        }
        Q[st] = Data[ran][ran2];
        Answer[st] = direction[ran2];
    }
    public void startsatting(){
        time = 0.0f;
        if(position[stage] == "왼쪽"){
            Question.alignment = alignment[0];
        }else{
            Question.alignment = alignment[1];
        }
        Question.text = Q[stage];
    }
}
