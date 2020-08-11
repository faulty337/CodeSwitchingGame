using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StroopPlay : MonoBehaviour
{
    public Text Question;
    public GameObject manager;
    public string[] color = {"빨간", "노란", "초록", "파란"};
    public string[] colorNum = {"<color=#bf2836>", "<color=#f3c500>", "<color=#0c7b3f>", "<color=#004e9e>"};
    public int stage, totalstage;
    public List<string[]> Data = new List<string[]>();
    public float time, Qtime;
    // Start is called before the first frame update
    void Start()
    {
        Data = manager.GetComponent<StroopManager>().Data.ConvertAll(s => s);
        totalstage = 40;
        Qtime = 1.0f;
        //colorNum[1] + "aaa" + "</color>"
        NextQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > Qtime){
            NextQuestion();
            time = 0.0f;
        }
    }

    public void ButtonIndex(int index){
        time = 0.0f;
        NextQuestion();
    }

    public void NextQuestion(){
        int ran = Random.Range(0,Data.Count);
        int ran2 = Random.Range(0,2);
        int ran3 = Random.Range(0,4);
        print(colorNum[2]);
        Question.color = UnityEngine.Color.blue;
        Question.text = color[ran3]+Data[ran][ran2];
        //colorNum[ran3]++"</color>"
        stage++;
        // if(stage > totalstage){
        //     manager.GetComponent<StroopManager>().gameEnd();
        // }
    }
}
