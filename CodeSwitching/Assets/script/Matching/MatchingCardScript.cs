using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchingCardScript : MonoBehaviour
{
    public Text NO, Time;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void RankSetting(int No, string Time){
        this.NO.text = No.ToString();
        this.Time.text = Time;
    }
}
