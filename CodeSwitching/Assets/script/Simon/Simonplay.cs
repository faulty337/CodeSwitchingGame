using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Simonplay : MonoBehaviour
{
    public GameObject manager, blockPanel;
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
        
        Len1Button.GetComponent<Text>().text = GameManager.Lan_1;
        Len2Button.GetComponent<Text>().text = GameManager.Lan_2;
        
    }

    public void GameStart(){
        timestart = 0.0f;
        empty = 0.2f;
        Question.text = "";
        Score.text = "0";
        ScoreCount = 0;
        QInterval = true;
        first = false;
        stage = 0;
        TotalStage = 40;
        time = 0.0f;
        totalTime = 0.0f;
        start = false;
        startTime = 1.0f;
        StartCoroutine(GameSetting());
        
    }

    IEnumerator GameSetting(){
        Data = manager.GetComponent<SimonManager>().Data.ConvertAll(s => s);
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
        switch(GameManager.Level){
            case 1:
                level = 3;
                break;
            case 2:
                level = 5;
                break;
            case 3:
                level = 7;
                break;
            default:
                level = 3;
                break;
        }
        
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
                }
            }else{
                if(time > Qtime){
                    print("쉬는타임");
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
        NextQuestion();
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
            manager.GetComponent<SimonManager>().gameEnd();
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
