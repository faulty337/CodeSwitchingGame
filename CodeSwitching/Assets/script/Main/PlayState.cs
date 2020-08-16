using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayState : MonoBehaviour
{
    [Header("GameObj")]
    public GameObject NBackObj;
    public GameObject MatchingObj, StroopObj, SimonObj, ComplexObj;
    [Header("Point")]
    public GameObject NBackPoint;
    public GameObject MatchingPoint, StroopPoint, SimonPoint, ComplexPoint;
    private List<GameObject> parent = new List<GameObject>();
    private List<GameObject> pointlist = new List<GameObject>();
    private int[] gamecount;
    public Text idoutput;
    private string getUrl;
    // Start is called before the first frame update
    void Start()
    {
        parent.Add(NBackObj);
        parent.Add(MatchingObj);
        parent.Add(StroopObj);
        parent.Add(SimonObj);
        parent.Add(ComplexObj);
        pointlist.Add(NBackPoint);
        pointlist.Add(MatchingPoint);
        pointlist.Add(StroopPoint);
        pointlist.Add(SimonPoint);
        pointlist.Add(ComplexPoint);
        gamecount = new int[5]{2, 5, 1, 3, 4};
        getUrl = "faulty337.cafe24.com/gameplaycount.php";
        idoutput.text = GameManager.ID + " 님의 플레이 현황";
        StartCoroutine(DataGet());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Countadd(GameObject parent, GameObject Point, int Count){
        if(Count > 20){
            Count = 20;
        }
        for(int i = 0; i < Count; i++){
            var point = Instantiate(Point);
            point.transform.SetParent(parent.transform);
        }
    }
    IEnumerator DataGet(){
        WWWForm form = new WWWForm();
        form.AddField("id", GameManager.ID);//GameManager.Subject
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
            gamecount[i-1] = int.Parse(data[i]);
        }
        for(int i = 0; i < 5; i++){
            Countadd(parent[i], pointlist[i], gamecount[i]);
        }

    }
}
