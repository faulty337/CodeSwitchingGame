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
    public InputField New_PassInputField, New_PassInputCheck;
    public InputField New_AgeInputField;
    public Dropdown newGender;
    public Dropdown newGrade;
    public Dropdown newLanguage;
    public GameObject SignUpPanel, LogInPanel, Panel;
    public Image PopupB;
    public Text PopupText;

    private bool PopupStatus;
    public string LoginUrl, SignUpUrl, ID;
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
        if(IDInputField.text == "" || PassInputField.text == "" ){
            Popup("빈칸이 존재합니다. 빈칸을 채워주세요.");
        }else{
            StartCoroutine(LoginCo());
        }
        
    }


    IEnumerator LoginCo()
    {

        WWWForm form = new WWWForm();
        form.AddField("Input_user", IDInputField.text);
        form.AddField("Input_pass", PassInputField.text);

        WWW webRequest = new WWW(LoginUrl, form);
        yield return webRequest;
        if(webRequest.text == "Success")
        {
            GameManager.ID = IDInputField.text;
            SceneManager.LoadScene("Main");
        }
        else if(webRequest.text == "ID")
        {
            Popup("존재하지 않는 ID입니다.");
        }
        else if(webRequest.text == "PassWord")
        {
            Popup("틀린 비밀번호입니다.");
        }
    }
    public void OpenLoginBtn(){
        LogInPanel.SetActive(true);
        GameManager.state = 1;
    }

    public void OpenSignUpBtn()
    {
        SignUpPanel.SetActive(true);
        GameManager.state = 1;
        //StartCoroutine(LoginCo());
    }

    public void SignupBtn()
    {
        print(New_IDIputField.text);
        print(New_PassInputField.text);
        print(New_AgeInputField.text);
        print(newGender.options[newGender.value].text);
        print(newGrade.options[newGrade.value].text);
        print(newLanguage.options[newLanguage.value].text);
        if(New_IDIputField.text == "" || New_PassInputField.text == "" 
        || New_AgeInputField.text == ""  || newGender.options[newGender.value].text == "성별*" 
        || newGrade.options[newGrade.value].text == "학력*" || newLanguage.options[newLanguage.value].text == "모국어*"){
            Popup("비어있는 칸이 존재합니다. 빈칸을 채워주세요.");

        }else{
            if(New_PassInputField.text != New_PassInputCheck.text){
                Popup("비밀번호가 다릅니다. 확인해주세요");
            }else{
                StartCoroutine(SignUpCo());
            }
            
        }
        
    }
    public void Popup(string text)
    {
        PopupB.gameObject.SetActive(true);
        PopupText.text = text;
        if(PopupStatus == true) //중복재생방지
        {
            return;
        }
        StartCoroutine(fadeoutplay(2.0f, 1.0f, 0.0f));//코루틴 실행
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
            Panel.SetActive(true);
        }else if(webRequest_signup.text == "ID"){
            Popup("중복되는 ID가 존재합니다. 다른 아이디를 사용해주세요.");

        }else if(webRequest_signup.text == "error"){
            Popup("네트워크를 확인하고 다시 시도해주세요.");
        }
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
    public void Cancelbtn(){
        GameManager.state = 0;
        LogInPanel.SetActive(false);
        SignUpPanel.SetActive(false);
        Panel.SetActive(true);
    }
}