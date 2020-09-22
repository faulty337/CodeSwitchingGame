using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingManager : MonoBehaviour
{
    public GameObject Statictics, GameChoice, Setting_1, SubjectChoice, Setting_Level, BackBt, past, present, ExitPopup;
    public Image PopupB;
    public Text PopupText;
    // public GameObject Statictics;
    // public GameObject GameChoice;
    // public GameObject Setting_1;
    // public GameObject SubjectChoice;
    // public GameObject Setting_Level;
    // public GameObject BackBt;
    // public GameObject past;
    // public GameObject present;
    private List<GameObject> Panels;
    public Dropdown Lan_1, Lan_2, level;
    public Button CancelBtn;

    bool PopupStatus = false;

    void Start()
    {
        Statictics.SetActive(true);
        GameChoice.SetActive(false);
        Setting_1.SetActive(false);
        SubjectChoice.SetActive(false);
        Setting_Level.SetActive(false);
        PopupB.gameObject.SetActive(false);
        ExitPopup.SetActive(false);
        Panels = new List<GameObject>();
        Panels.Add(Statictics);
        Panels.Add(GameChoice);
        Panels.Add(Setting_1);
        Panels.Add(SubjectChoice);
        Panels.Add(Setting_Level);
        Panels.Add(ExitPopup);
        if(GameManager.state == 3){
            gotoPanel("0,1");
            
        }
        GameManager.state = 3;
        CancelBtn.gameObject.SetActive(false);
        
    }

    void Updata(){
        #if UNITY_ANDROID
            if(Input.GetKeyUp(KeyCode.Escape))
            {
                switch(GameManager.state){
                    case 1:
                        ExitPopup.SetActive(true);
                        break;
                    case 2:
                        ExitPopup.SetActive(true);
                        break;
                    default:
                        BackButton();
                        break;
                }
            }
    }
    #endif
    public void gotoPanel(string index)
    {
        string[] index_p = index.Split(',');

        past = Panels[int.Parse(index_p[0])];
        present = Panels[int.Parse(index_p[1])];
        if(int.Parse(index_p[1]) == 2 || int.Parse(index_p[1]) == 3 || int.Parse(index_p[1]) == 4){
            CancelBtn.gameObject.SetActive(true);
        }else{
            CancelBtn.gameObject.SetActive(false);
        }
        past.SetActive(false);
        present.SetActive(true);
    }

    public void GameChoiceButtonEvent(string game)
    {
        GameManager.Game = game;
        BackBt.SetActive(true);
        Setting_1.SetActive(true);
        GameManager.state = 4;
        CancelBtn.gameObject.SetActive(true);
    }

    public void LenguageSetting()
    {
        if(Lan_1.options[Lan_1.value].text == "언어1" || Lan_2.options[Lan_2.value].text=="언어2"){
            Popup("두 언어 모두 선택해주세요.");
        }else if(Lan_1.options[Lan_1.value].text == Lan_2.options[Lan_2.value].text){
            Popup("다른 언어를 선택해주세요.");
        }else{
            GameManager.Lan_1 = Lan_1.options[Lan_1.value].text;
            GameManager.Lan_2 = Lan_2.options[Lan_2.value].text;
            gotoPanel("2, 3");
            Panels[3].GetComponent<SubjectCount>().SubjectSetting();
            GameManager.state = 5;
        }
        
    }

    public void SubjectSetting(string subject)
    {
        GameManager.Subject = subject;
        gotoPanel("3, 4");
        
        GameManager.state = 6;
    }

    public void LevelSetting(int level)
    {
        GameManager.Level = level;
        GameManager.state = 7; //게임 상태
        SceneManager.LoadScene(GameManager.Game);
    }
    public void BackButton()
    {
        gotoPanel((GameManager.state-2).ToString()+", 1");
        GameManager.state = 2;
        BackBt.SetActive(false);
    }
    public void Popup(string text)

    {
        PopupB.gameObject.SetActive(true);
        PopupText.text = text;
        if(PopupStatus == true) //중복재생방지
        {
            return;
        }
        StartCoroutine(fadeoutplay(2.0f, 1.0f, 0.0f));    //코루틴 실행
    }

    IEnumerator fadeoutplay(float FadeTime, float start, float end){
        PopupStatus = true;
        Color PopColor = PopupB.color;
        Color textColor = PopupText.color;
        float time = 0f;
        PopColor.a = Mathf.Lerp(start, end, time);
        textColor.a = Mathf.Lerp(start, end, time);
            while (PopColor .a > 0f)
            {
                time += Time.deltaTime / FadeTime;
                PopColor .a = Mathf.Lerp(start, end, time);
                textColor.a = Mathf.Lerp(start, end, time);
                PopupB.color = PopColor ;
                PopupText.color = textColor;
                yield return null;
            }
            PopupStatus = false;
            PopupB.gameObject.SetActive(false);

    }

}
