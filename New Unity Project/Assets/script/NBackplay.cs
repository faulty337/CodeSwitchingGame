using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class NBackplay : MonoBehaviour
{
    public GameObject manager;
    public int stage; //현재 스테이지, 시간에 따라 스테이지 변화
    public bool start; //게임 시작 유무
    public int TotalStage; //총 스테이지 길이
    public float sec; //한 문제당 풀이 시간??
    public float time;//시작후 프레임당 시간
    public float delay;//시작 딜레이
    public TextMeshProUGUI count;
    public TextMeshProUGUI question;
    public Button yesbutton, nobutton; //화면 비율에 따라 할건지
    public string[,] Q; //문제
    public int[,] data;  //사용자 입력
    public int N;
    // Start is called before the first frame update
    void Start()
    {
        start = false;
        time = 1.0f;
        sec = 1.0f;
        delay = 4.0f;
        TotalStage = manager.GetComponent<NBackManager>().TotalStage;
        Q = (string[,])manager.GetComponent<NBackManager>().Q.Clone();
        N = manager.GetComponent<NBackManager>().N;   
        data = new int[TotalStage, 4];//첫번째엔 문제, 두번째엔 문제정답, 3번째엔 사용자 입력, 4번째엔 장답여부
        count.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime; //시간정보 누적
        if (stage == 0 && start)
        {
            data[stage, 0] = Random.Range(0, 10);
            data[stage, 1] = 2;
            question.gameObject.SetActive(true);
            question.GetComponent<TextMeshProUGUI>().text = Q[data[stage, 0], Random.Range(0, 2)];
            stage++;
            return;
        }
        if (!start)
        {
            count.GetComponent<TextMeshProUGUI>().text = ((int)time).ToString();
            if (time > delay)
            {
                time = 0.0f;
                start = true;
                count.gameObject.SetActive(false);
            }
        }
        else//게임 시작부분
        {
            if (time > sec) //stage 증가 시점(1초마다 실행)
            {
                if (stage > N)
                {
                    Problem();
                }
                else
                {
                    data[stage, 0] = Random.Range(0, 10);
                    data[stage, 1] = 2;
                }
                question.GetComponent<TextMeshProUGUI>().text = Q[data[stage, 0], Random.Range(0, 2)];
                Debug.Log(stage + "");
                sec++;
                stage++;
                if (stage >= TotalStage)//게임 종료 여부
                    manager.GetComponent<NBackManager>().play = false; //게임종료변수 변경
                
            }
        }
    }

    void Problem()//이함수는 N번째 스테이지 이후부터 사용해야함
    {
        if (0.5 > Random.Range(0.0f, 1.5f))//거의 3/1확률
        {
            Debug.Log("next");
            data[stage, 0] = data[stage- N, 0];
            data[stage, 1] = 1;//1이면 정답
        }
        else
        {
            
            data[stage, 0] = Random.Range(0, 10);
            data[stage, 1] = 2;//2면 오답
        }

    }

    public void YesButton()
    {
        data[stage, 2] = 1;
    }

    public void NoButton()
    {
        data[stage, 2] = 2;
    }
}
