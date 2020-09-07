using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubjectCount : MonoBehaviour
{
    public Text School, Sports, Kitchen, Jobs;
    public GameObject Schoolobj, Sportsobj, Kitchenobj, Jobsobj;

    public List<Text> Subjects;
    public List<GameObject> countobj;

    private string getUrl;
    // Start is called before the first frame update
    void Start()
    {
        Subjects = new List<Text>();
        countobj = new List<GameObject>();
        Subjects.Add(School);
        Subjects.Add(Sports);
        Subjects.Add(Kitchen);
        Subjects.Add(Jobs);
        
        countobj.Add(Schoolobj);
        countobj.Add(Sportsobj);
        countobj.Add(Kitchenobj);
        countobj.Add(Jobsobj);
        
    }
    public void SubjectSetting(){
        Schoolobj.SetActive(true);
        Sportsobj.SetActive(true);
        Kitchenobj.SetActive(true);
        Jobsobj.SetActive(true);
        StartCoroutine(DataGet());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator DataGet(){
        getUrl = "faulty337.cafe24.com/SubjectCount.php";
        WWWForm form = new WWWForm();
        form.AddField("id", GameManager.ID);//GameManager.Subject
        form.AddField("GameName", GameManager.Game);
        WWW web = new WWW(getUrl, form);
        do
        {
            yield return null;
        }
        while (!web.isDone);
        if (web.error != null)
        {
            Debug.LogError("web.error=" + web.error);
            yield break;
        }
        string[] data = web.text.Split(',');
        for (int i = 1; i < data.Length; i++)
        {
            Subjects[i-1].text = data[i];
            if(data[i] == "0"){
                countobj[i-1].SetActive(false);
            }
        }


    }
}
