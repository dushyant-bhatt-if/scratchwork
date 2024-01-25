using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class apiManager : MonoBehaviour
{
    public static apiManager ins;

    private void Awake()
    {
        if (ins == null)
            ins = this;
    }

    #region check memberCode
    string memberCode_Url = "https://bm432lnyc6.execute-api.us-east-1.amazonaws.com/dev/checkMemberCodeExist";

    public void isCodeAvailable()
    {
        string field = LoginManager.ins.MemberCodeField.text;
        string dobField = 
        CustomDatePicker.CalendarController.calendarInstance.selectedYear + "-" +
        CustomDatePicker.CalendarController.calendarInstance.selectedMonth+ "-" +
        CustomDatePicker.CalendarController.calendarInstance.selectedDay;

        StartCoroutine(checkMembercode(field,dobField));
    }
    public bool isValid;
    public string userid;
   public LoginRoot LoginData;
    public string warningMsg;
    public IEnumerator checkMembercode(string field,string dobField)
    {
        if (field != "" && dobField !="")
        {
            usercode u1 = new usercode();
            u1.memberCode = field;
            u1.birthday = dobField;
            var body = JsonUtility.ToJson(u1);

            Debug.Log(body);


            using (UnityWebRequest request =
             new UnityWebRequest(new Uri(memberCode_Url), UnityWebRequest.kHttpVerbPOST))
            {
                request.SetRequestHeader("Content-Type", "application/json");
                var jsonBytes = System.Text.Encoding.UTF8.GetBytes(body);
                request.uploadHandler = new UploadHandlerRaw(jsonBytes);
                request.downloadHandler = new DownloadHandlerBuffer();

                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.ConnectionError
                    || request.result == UnityWebRequest.Result.ProtocolError
                    || request.result == UnityWebRequest.Result.DataProcessingError)
                {
                    Debug.LogWarning($" Failed! due to : <color=red> {request.error} </color>");

                }
                else
                {
                    JSONNode jsonNode = JSON.Parse(request.downloadHandler.text);

                    isValid =
                     (Convert.ToBoolean(jsonNode["isExist"].Value));

                    print(jsonNode["status"].Value);
                    print(jsonNode["isExist"].Value);
                    print("Message : " + jsonNode["message"].Value);
                     warningMsg = jsonNode["message"].Value;
                    if (!isValid)
                    {
                        LoginManager.ins.showToast(warningMsg, 5);

                        Handheld.Vibrate();
                        LoginManager.ins.shakeDuration = 2;
                    }
                    else if (isValid)
                    {
                        userid = jsonNode["userId"].Value;
                    }

                }
            }
        }
    }
    #endregion

    #region LoginData

    public IEnumerator loginApi(string code, string userId)
    {
        User u1 = new User();
        u1.Membercode = code;
        u1.userId = userId;
        var body = JsonUtility.ToJson(u1);

        Debug.Log(body);


        using (UnityWebRequest request =
         new UnityWebRequest(new Uri("https://bm432lnyc6.execute-api.us-east-1.amazonaws.com/dev/getUserDetail"), UnityWebRequest.kHttpVerbPOST))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            // request.SetRequestHeader("Authorization", UserData.userAuthToken);

            var jsonBytes = System.Text.Encoding.UTF8.GetBytes(body);
            request.uploadHandler = new UploadHandlerRaw(jsonBytes);
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();


            if (request.result == UnityWebRequest.Result.ConnectionError
                || request.result == UnityWebRequest.Result.ProtocolError
                || request.result == UnityWebRequest.Result.DataProcessingError)
            {
                Debug.LogWarning($" Failed! due to : <color=red> {request.error} </color>");
            }
            else
            {
                LoginData = JsonUtility.FromJson<LoginRoot>(request.downloadHandler.text);              
                print(LoginData.status);
                string name = LoginData.data.name;
                PlayerPrefs.SetInt("coins", LoginData.data.totalCoin);

                if (PlayerPrefs.GetInt("inPlay")==1)
                   SceneManager.LoadSceneAsync(1);
                else
                    UImanager.ins.GetOnloginSuccess(name);
            }
        }
    }
    #endregion
}
[System.Serializable]
public class usercode
{
    public string memberCode;
  
    public string birthday;
    
}
[System.Serializable]

public class LoginData
{
    public string id ;
    public string memberCode ;
    public string name ;
    public string birthday ;
    public int totalCoin ;
    public string familyName;
    public string givenName;

}
[System.Serializable]

public class LoginRoot
{
    public int status ;
    public LoginData data ;
}
