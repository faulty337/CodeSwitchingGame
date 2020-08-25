using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Simonplay : MonoBehaviour
{
    public GameObject manager;
    public Button Len1Button, Len2Button;
    public float time, Qtime, totalTime;
    public Text Question;
    public bool start = false;
    private List<string[]> Data = new List<string[]>();
    private int stage, level;
    public int TotalStage;
    public string[] input, Answer, Compatible, position, reactionTime, Q, direction;
    
    private TextAnchor[] alignment;
    
    // Start is called before the first frame update
    public void Start()
    {   
        stage = 0;
        TotalStage = 40;
        alignment = new TextAnchor[2] {TextAnchor.MiddleLeft, TextAnchor.MiddleRight};
        Data = manager.GetComponent<SimonManager>().Data.ConvertAll(s => s);
        start = false;
        Qtime = 1.0f;
        input = new string[TotalStage+1];
        Answer = new string[TotalStage+1];
        Compatible = new string[TotalStage+1];
        position = new string[TotalStage+1];
        reactionTime = new string[TotalStage+1];
        direction = new string[2] {"왼쪽","오른쪽"};
        Q = new string[TotalStage+1];
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
        // Len1Button.GetComponent<Text>().text = GameManager.Len_1;
        // Len2Button.GetComponent<Text>().text = GameManager.Len_2;
        startsatting();
        
    }

    // Update is called once per frame
    void Update()
    {
        totalTime += Time.deltaTime;
        time += Time.deltaTime;
        if(start){
            if(time > Qtime){
                NextQuestion();
                time = 0.0f;
            }
        }else{
            if(time > 1.0f){
                start = true;
                startsatting();
            }
        }

        // if(start){
            
        // }else{
        //     timer(time);
        // }
    }

    public void LanButton(int index){
        reactionTime[stage] = time.ToString();
        input[stage] = direction[index];
        NextQuestion();
        
    }



    public void NextQuestion(){
        time = 0.0f;
        stage++;
        input[stage] = "Pass";
        reactionTime[stage] = "1";
        int ran = Random.Range(0,Data.Count);
        int ran2 = Random.Range(0,2);
        int ran3 = Random.Range(0,2);
        if(Random.Range(1, 11) > level){
            Question.alignment = alignment[ran2];
            position[stage] = direction[ran2];
            Compatible[stage] = "compatible";
        }else{
            Compatible[stage] = "incompatible";
            if(ran2 == 0){
                Question.alignment = alignment[1];
                position[stage] = direction[1];
            }else{
                Question.alignment = alignment[0];
                position[stage] = direction[0];
            }
        }
        
        Question.text = Data[ran][ran2];
        Q[stage] = Data[ran][ran2];
        Answer[stage] = direction[ran2];
        if(stage >= TotalStage){
            manager.GetComponent<SimonManager>().gameEnd();
        }
    }
    public void startsatting(){
        time = 0.0f;
        input[stage] = "Pass";
        reactionTime[stage] = "1";
        int ran = Random.Range(0,Data.Count);
        int ran2 = Random.Range(0,2);
        int ran3 = Random.Range(0,2);
        if(Random.Range(1, 11) > level){
            Question.alignment = alignment[ran2];
            position[stage] = direction[ran2];
            Compatible[stage] = "compatible";
        }else{
            Compatible[stage] = "incompatible";
            if(ran2 == 0){
                Question.alignment = alignment[1];
                position[stage] = direction[1];
            }else{
                Question.alignment = alignment[0];
                position[stage] = direction[0];
            }
        }
        
        Question.text = Data[ran][ran2];
        Q[stage] = Data[ran][ran2];
        Answer[stage] = direction[ran2];
        if(stage >= TotalStage){
            manager.GetComponent<SimonManager>().gameEnd();
        }
    }
}
