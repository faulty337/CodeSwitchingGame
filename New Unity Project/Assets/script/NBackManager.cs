using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class NBackManager : MonoBehaviour
{
    public int TotalStage; //총 스테이지 길이
    public TextMeshProUGUI count;
    public TextMeshProUGUI question;
    public GameObject panel;
    public string[,] Q; //문제
    public int[] data;  //사용자 입력
    public int N;

    public GameObject playpanel;
    public GameObject endpanel;
    public bool play;
    // Start is called before the first frame update
    void Start()
    {
        N = 2;
        Q = new string[10, 2] {
            { "1", "one" }, { "2", "two" }, { "3", "three" },
            { "4", "four" }, { "5", "five" }, { "6", "six" },
            { "7", "seven" }, { "8", "eight" }, { "9", "nine" },
            { "10", "ten" }
        };//이후에 받아올 내용
        play = true;
        TotalStage = 10;
        data = new int[TotalStage];
        

    }

}
