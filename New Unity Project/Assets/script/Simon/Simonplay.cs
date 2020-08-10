using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Simonplay : MonoBehaviour
{
    public GameObject manager;
    public Button Len1Button, Len2Button;
    public float time, Qtime;
    public Text Question;
    public bool check, start;
    private List<string[]> Data = new List<string[]>();
    private int totalstage, stage;
    
    private TextAnchor[] alignment;
    
    // Start is called before the first frame update
    void Start()
    {
        totalstage = 40;
        alignment = new TextAnchor[2] {TextAnchor.MiddleLeft, TextAnchor.MiddleRight};
        print("asdfas");
        Data = manager.GetComponent<SimonManager>().Data.ConvertAll(s => s);
        print(Data[0][0]);
        print("Aaa");
        start = false;
        Qtime = 0.5f;
        // Len1Button.GetComponent<Text>().text = GameManager.Len_1;
        // Len2Button.GetComponent<Text>().text = GameManager.Len_2;
         
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > Qtime){
            NextQuestion();
            time = 0.0f;
        }
        // if(start){
            
        // }else{
        //     timer(time);
        // }
    }

    public void timer(float tiem){
        if(time > 3.0f){

        }
    }

    public void Len_1Button(){
        NextQuestion();
        time = 0.0f;
        
    }

    public void Len_2Button(){
        NextQuestion();
        time = 0.0f;
    }

    public void NextQuestion(){
        print(stage);
        int ran = Random.Range(0,Data.Count);
        int ran2 = Random.Range(0,2);
        int ran3 = Random.Range(0,2);
        Question.alignment = alignment[ran3];
        Question.text = Data[ran][ran2];
        stage++;
        if(stage > totalstage){
            manager.GetComponent<SimonManager>().gameEnd();
        }
    }
}
