using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rankscript : MonoBehaviour
{
    public Text NO, Score, Time;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void RankSetting(int No, string Score, string Time){
        this.NO.text = No.ToString();
        this.Score.text = Score;
        this.Time.text = Time;
    }
}
