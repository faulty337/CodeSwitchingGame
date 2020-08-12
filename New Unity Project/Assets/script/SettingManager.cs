using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingManager : MonoBehaviour
{
    public GameObject Statictics;
    public GameObject GameChoice;
    public GameObject Setting_1;
    public GameObject SubjectChoice;
    public GameObject Setting_Level;
    public GameObject BackBt;
    public GameObject past;
    public GameObject present;
    private List<GameObject> Panels;
    public Dropdown Len_1;
    public Dropdown Len_2;
    public Dropdown level;

    void Start()
    {
        Statictics.SetActive(true);
        GameChoice.SetActive(false);
        Setting_1.SetActive(false);
        SubjectChoice.SetActive(false);
        Setting_Level.SetActive(false);
        Panels = new List<GameObject>();
        Panels.Add(Statictics);
        Panels.Add(GameChoice);
        Panels.Add(Setting_1);
        Panels.Add(SubjectChoice);
        Panels.Add(Setting_Level);
        GameManager.state = 1;
    }

    public void gotoPanel(string index)
    {
        string[] index_p = index.Split(',');

        past = Panels[int.Parse(index_p[0])];
        present = Panels[int.Parse(index_p[1])];
        past.SetActive(false);
        present.SetActive(true);
        GameManager.state = int.Parse(index_p[2]);
    }

    public void GameChoiceButtonEvent(string game)
    {
        GameManager.Game = game;
        BackBt.SetActive(true);
        Setting_1.SetActive(true);
        GameManager.state = 3;
    }

    public void LenguageSetting()
    {
        GameManager.Len_1 = Len_1.options[Len_1.value].text;
        GameManager.Len_2 = Len_2.options[Len_2.value].text;
        gotoPanel("2, 3, 4");
    }

    public void SubjectSetting(string subject)
    {
        GameManager.Subject = subject;
        gotoPanel("3, 4, 5");
    }

    public void LevelSetting(int level)
    {
        GameManager.Level = level;
        GameManager.state = 6; //게임 상태
        SceneManager.LoadScene(GameManager.Game);
    }
    public void BackButton()
    {
        //GameManager.state;
    }
}
