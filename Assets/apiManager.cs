using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;

public class apiManager : MonoBehaviour
{
    public static apiManager ins;

    private void Awake()
    {
        if (ins == null)
            ins  = this;
    }

    #region check memberCode
    string memberCode_Url = "https://bm432lnyc6.execute-api.us-east-1.amazonaws.com/dev/checkMemberCodeExist";

    public void isCodeAvailable()
    {
        // StartCoroutine(checkMembercode());
    }
    public bool isValid;
    public IEnumerator checkMembercode()
    {
        if (LoginManager.ins._PinField.text != "")
        {
            WWWForm form = new WWWForm();

            memberCode_Url += "?memberCode=" + LoginManager.ins._PinField.text;
            Debug.Log(memberCode_Url);
            using (var w = UnityWebRequest.Post(memberCode_Url, form))
            {
                yield return w.SendWebRequest();
                if (w.result != UnityWebRequest.Result.Success)
                {
                    print(w.error);
                    //yield return false;
                }
                else
                {
                    print(w.downloadHandler.text);
                    JSONNode jsonNode = JSON.Parse(w.downloadHandler.text);
                    isValid =
                    (Convert.ToBoolean(jsonNode["isExist"].Value));

                    print(jsonNode["status"].Value);
                    print(jsonNode["isExist"].Value);
                    print("Message : " + jsonNode["message"].Value);
                    // yield return true;
                }
            }
        }
    }


    #endregion
}
