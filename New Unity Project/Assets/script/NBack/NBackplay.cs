using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class NBackplay : MonoBehaviour
{
    public GameObject manager, endPannel;
    public int stage; //현재 스테이지, 시간에 따라 스테이지 변화
    public bool start = false; //게임 시작 유무
    public int TotalStage; //총 스테이지 길이
    public float sec; //한 문제당 풀이 시간??
    public float time;//시작후 프레임당 시간
    public float delay;//시작 딜레이
    public TextMeshProUGUI count;
    public Text question;
    public Button yesbutton, nobutton; //화면 비율에 따라 할건지
    public List<string[]> data, datacopy = new List<string[]>(); //문제
    public string[,] Q;  //사용자 입력
    public string[] correct, input, RTime, Answer;
    public List<int> index;
    public int N, RanQ, RanI, Ran;
    // Start is called before the first frame update
    void Start()
    {
        TotalStage = manager.GetComponent<NBackManager>().TotalStage;
        N = manager.GetComponent<NBackManager>().N;
        time = 1.0f;
        sec = 1.0f;
        input = new string[TotalStage+1];
        RTime = new string[TotalStage+1];
        Answer = new string[TotalStage + N];
        data = manager.GetComponent<NBackManager>().Q.ConvertAll(s => s) ;
        datacopy = data.ConvertAll(s => s);
        index = new List<int>();
        for(int i = 0; i < data.Count; i++){
            index.Add(i);
        }
           
        Q = new string[TotalStage+N, 4];//문제 인덱스, 출력할 문제, 문제 한영 여부
        
        for(int i = 0; i < N; i++){
            RanQ = index[Random.Range(0, data.Count)];
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
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime; //시간정보 누적
        if(start){
            if(time > sec){
                NextQuestion();
            }
        }else{
            if(time > 1.0f){
                start = true;
                time = 0.0f;
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
        question.text = Q[stage, 1];
        stage++;
        time=0.0f;
        if(stage > 39){
            manager.GetComponent<NBackManager>().GameEnd();
        }
        input[stage]="pass";
        RTime[stage] = "1";
    }

    public void NBackButton(string BtName)
    {
       input[stage] = BtName;
       RTime[stage] = time.ToString();
       NextQuestion();
    }
}
