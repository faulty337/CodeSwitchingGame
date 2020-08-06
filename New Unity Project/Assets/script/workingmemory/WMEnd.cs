using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WMEnd : MonoBehaviour
{
    public Text FailCount;
    public Text Time;
    public Button nextButton;
    private int fail, touch;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.Level <= 3)
        {
            nextButton.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Results(int fail, int touch, float time)
    {
        this.fail = fail;
        this.touch = touch;
        this.time = Mathf.Round(time*10)*0.1f;

        Time.text = "소요시간  " +this.time.ToString() + "초";
        FailCount.text = "정  확  도  " + this.fail.ToString();
    }
}
