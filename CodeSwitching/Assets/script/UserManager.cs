using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserManager : MonoBehaviour
{
    [Header("LoginPanel")]
    public InputField IDInputField;
    public InputField PassInputField;
    [Header("CreateAccountPanel")]
    public InputField New_IDIputField;
    public InputField New_PassInputField;
    public InputField New_AgeInputField;
    public Dropdown newGender;
    public Dropdown newGrade;
    public Dropdown newLanguage;
    public GameObject SignUpPanel;
    public GameObject LogInPanel;

    public string LoginUrl;
    public string SignUpUrl;
    public string ID;
    public int SetWidth, SetHeight;
    // Start is called before the first frame update
    void Start()
    {
        SetWidth = 16;
        SetHeight = 9;
        LoginUrl = "faulty337.cafe24.com/Login.php";
        SignUpUrl = "faulty337.cafe24.com/SignUp.php";
        LogInPanel.SetActive(false);
        SignUpPanel.SetActive(false);
    }

    public void LoginBtn()
    {
        StartCoroutine(LoginCo());
    }

    IEnumerator LoginCo()
    {
        Debug.Log(IDInputField.text);
        Debug.Log(PassInputField.text);

        WWWForm form = new WWWForm();
        form.AddField("Input_user", IDInputField.text);
        form.AddField("Input_pass", PassInputField.text);

        WWW webRequest = new WWW(LoginUrl, form);
        yield return webRequest;
        if(webRequest.text == "true")
        {
            GameManager.ID = IDInputField.text;
            SceneManager.LoadScene("Main");
        }
        else
        {

        }
    }
    public void OpenLoginBtn(){
        LogInPanel.SetActive(true);
    }

    public void OpenSignUpBtn()
    {
        SignUpPanel.SetActive(true);
        //StartCoroutine(LoginCo());
    }

    public void SignupBtn()
    {
        StartCoroutine(SignUpCo());
    }   

    IEnumerator SignUpCo()
    {
       
        WWWForm signupform = new WWWForm();
       
        signupform.AddField("Input_id", New_IDIputField.text);
        signupform.AddField("Input_password", New_PassInputField.text);
        signupform.AddField("Input_age", New_AgeInputField.text);
        signupform.AddField("Input_gender", newGender.options[newGender.value].text);
        signupform.AddField("Input_grade", newGrade.options[newGrade.value].text);
        signupform.AddField("Input_language", newLanguage.options[newLanguage.value].text);

        WWW webRequest_signup = new WWW(SignUpUrl, signupform);
        
        yield return webRequest_signup;
        if (webRequest_signup.text == "success")
        {
            SignUpPanel.SetActive(false);
            LogInPanel.SetActive(true);
        }
    }
}
//material
