using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;
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
        StartCoroutine(checkMembercode(field));
    }
    public bool isValid;
    public IEnumerator checkMembercode(string field)
    {
        if (field != "")
        {
            usercode u1 = new usercode();
            u1.memberCode = field;

            var body = JsonUtility.ToJson(u1);

            Debug.Log(body);


            using (UnityWebRequest request =
             new UnityWebRequest(new Uri(memberCode_Url), UnityWebRequest.kHttpVerbPOST))
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
                    JSONNode jsonNode = JSON.Parse(request.downloadHandler.text);

                    isValid =
                     (Convert.ToBoolean(jsonNode["isExist"].Value));

                    print(jsonNode["status"].Value);
                    print(jsonNode["isExist"].Value);
                    print("Message : " + jsonNode["message"].Value);
                    if (!isValid)
                    {
                        LoginManager.ins.showToast("Member code doesn't exist!", 5);
                        Handheld.Vibrate();
                        LoginManager.ins.shakeDuration = 2;
                        LoginManager.ins.SignupBtn.interactable = false;
                    }
                    else if (isValid)
                    {
                        LoginManager.ins.SignupBtn.interactable = true;

                    }

                }
            }
        }
    }


    #endregion
}
[System.Serializable]
public class usercode
{
    public string memberCode;
}

