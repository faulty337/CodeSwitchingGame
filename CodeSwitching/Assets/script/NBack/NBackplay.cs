using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class NBackplay : MonoBehaviour
{
    public GameObject manager, endPannel, BlockPanel;
    public Button NoBtn, YesBtn;
    public int stage; //현재 스테이지, 시간에 따라 스테이지 변화
    public bool start, QInterval, first;//게임 시작 유무
    public int TotalStage; //총 스테이지 길이
     //한 문제당 풀이 시간??
    public float time, totalTime, QTime, timestart, empty, startTime;//시작후 프레임당 시간
    public float delay;//시작 딜레이
    public Text question, Score;
    public List<string[]> data, datacopy; //문제
    public string[,] Q;  //사용자 입력
    public string[] correct, input, RTime, Answer;
    public List<int> index;
    public int N, RanQ, RanI, Ran, successCount;
    // Start is called before the first frame update
    public void Start()
    {
        // GameStart();
        //Start에서 초기화 하게 되면 게임 다시 시작시 초기화가 안됨;;
        
    }

    public void GameStart(int level, int totalStage, List<string[]> data){
        BlockPanel.SetActive(true);
        Score.text = "0";
        timestart = 0.0f;
        successCount = 0;
        first = false;
        stage = 0;
        empty = 0.2f;
        startTime = 1.0f;
        QInterval = true;
        start = false;
        question.text = "";
        TotalStage = totalStage;
        N = level;
        totalTime = 0.0f;
        time = 0.0f;
        QTime = 2.0f;
        this.data = new List<string[]>();
        this.data = data;
        input = new string[TotalStage+1];
        RTime = new string[TotalStage+1];
        Answer = new string[TotalStage + N];
        StartCoroutine(GameSetting());
    }
    
    IEnumerator GameSetting(){
        
        datacopy = new List<string[]>();
        datacopy = data.ConvertAll(s => s);
        
        index = new List<int>();
        for(int i = 0; i < data.Count; i++){
            index.Add(i);
            print(index[i]);
        }
        Q = new string[TotalStage+N, 4];//문제 인덱스, 출력할 문제, 문제 한영 여부
        for(int i = 0; i < N; i++){ //N까지의 문제
            int ran = Random.Range(0, datacopy.Count);
            print(ran);
            RanQ = index[ran];
            
            RanI = Random.Range(0, 2);
            Q[i,0] = RanQ.ToString();
            Q[i,1] = datacopy[RanQ][RanI];
            Q[i,2] = RanI.ToString();
            index.Remove(RanQ);
            datacopy.Remove(datacopy[RanQ]);
            Answer[i] = "No";
        }
        for(int i = 0; i< TotalStage; i++){
            Questionmaking(i);
        }
        input[stage]="pass";
        RTime[stage] = "1";
        timestart = 1.0f;
        
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        totalTime+=Time.deltaTime * timestart;
        time += Time.deltaTime * timestart; //시간정보 누적
        // print(stage + "  "+ Q[stage,0] + "  "+ Q[stage,1] + "  "+ Q[stage,2] + "  ");
        if(first){
            if(QInterval){
                if(time > empty){
                    NextQuestion();
                    QInterval = false;
                    NoBtn.interactable = true;
                    YesBtn.interactable = true;
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
                    BlockPanel.SetActive(false);
                    time = 0.0f;
                    FirstQuestion();
                }
            }
            
        }

    }

    public void Questionmaking(int stage){
        Ran = Random.Range(0, 2);
        string question;
        int KE;
        int index;
        if(Ran == 0){
            index = int.Parse(Q[stage, 0]);
            if(Q[stage, 2] == "0"){
                KE = 1;
            }else{
                KE = 0;
            }
            question = data[int.Parse(Q[stage, 0])][KE];
            Answer[stage+N] = "Yes";
            
        }else{
            index = Random.Range(0, data.Count);
            KE = Random.Range(0, 1);
            question = data[index][KE];
            Answer[stage+N] = "No";
        }
        Q[stage + N,0] = index.ToString();
        Q[stage+N, 1] = question;
        Q[stage+N, 2] = KE.ToString();
    }

    public void NextQuestion(){
        stage++;
        if(stage >= TotalStage){
            manager.GetComponent<NBackManager>().GameEnd();
            // if(GameManager.state == 10){
                
            // }
        }
        question.text = Q[stage, 1];
        print(stage + "  " + Q[stage, 1]);
        time=0.0f;
        
        input[stage]="pass";
        RTime[stage] = "1";
        
        
    }

    public void NBackButton(string BtName)
    {
        input[stage] = BtName;
        RTime[stage] = time.ToString();
        if(BtName == Answer[stage]){
                successCount++;
                Score.text = successCount.ToString();
        }
        question.text = "";
        QInterval = true;
        time = 0.0f;
        first = true;
        NoBtn.interactable = false;
        YesBtn.interactable = false;
    //    NextQuestion();
    }

    public void FirstQuestion(){ //맨처음 stage가 0일때
        question.text = Q[stage, 1];
        print(0 + "" + Q[stage, 1]);
        time=0.0f;
        input[stage]="pass";
        RTime[stage] = "1";
    }
}
