using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WMEnd : MonoBehaviour
{
    public GameObject play;
    public Text FailCount;
    public Text Time;
    public Button nextButton;
    private int fail, touch, totalCard;
    private float time;
    // Start is called before the first frame update

    private string saveUrl, date, question;
    void Start()
    {
        totalCard = play.GetComponent<WMplay>().totalCard;
        saveUrl = "faulty337.cafe24.com/datasave.php";
        date = System.DateTime.Now.ToString("MM/dd/yyyy");
        question = extract(play.GetComponent<WMplay>().question);
        time = play.GetComponent<WMplay>().time;
        if(GameManager.Level <= 3)
        {
            nextButton.interactable = false;
        }
        StartCoroutine(DataSave());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator DataSave()
    {
        WWWForm form = new WWWForm();
        form.AddField("game", GameManager.Game);
        form.AddField("date", date);
        form.AddField("id", GameManager.ID);
        form.AddField("Lan_1", GameManager.Lan_1);
        form.AddField("Lan_2", GameManager.Lan_2);
        form.AddField("subject", GameManager.Subject);
        form.AddField("level", GameManager.Level);
        form.AddField("question", question);
        form.AddField("time", time.ToString());
        WWW webRequest = new WWW(saveUrl, form);
        yield return webRequest;
        print(webRequest.text);
    }

    public void Results(int fail, int touch, float time)
    {
        this.fail = fail;
        this.touch = touch;
        this.time = Mathf.Round(time*10)*0.1f;

        Time.text = "소요시간  " +this.time.ToString() + "초";
        FailCount.text = "정  확  도  " + this.fail.ToString();
    }
    public string extract(string[] data)
    {
        string result = "";
        for(int i = 0; totalCard > i; i++)
        {
            result += "," + data[i];
        }

        return result;

    }
}
