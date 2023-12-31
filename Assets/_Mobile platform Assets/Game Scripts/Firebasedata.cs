using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class Firebasedata : MonoBehaviour
{
    // Start is called before the first frame update

    #region Generate Random Member Code
    const string glyphs = "0123456789"; //add the characters you want
    string myString;
    public string GenerateCode()
    {
        myString = "";
        for (int i = 0; i < 4; i++)
        {
            myString += glyphs[Random.Range(0, glyphs.Length)];
        }

        return myString;
        Debug.Log(" === "+myString);
    }
    #endregion
    public string firebaseUrl = "https://mvp-project-f9929-default-rtdb.firebaseio.com/";

    public List<User> CurrentUser;
    FirebaseDatabase db;
    private void Start()
    {
        //updateUsers();
        InvokeRepeating("updateUsers", 1,10);
    }
    public void AddUser()
    {
        User user = new User();
        user.Membercode = LoginManager.ins.MemberCodeField.text;
        user.PasswordField =LoginManager.ins.NewPasswordField.text;
        user.CPasswordField = LoginManager.ins.ConfirmPasswordField.text;
        user.DobField = CustomDatePicker.CalendarController.calendarInstance.selectedYear + "-" +
                        CustomDatePicker.CalendarController.calendarInstance.selectedMonth + "-" +
                        CustomDatePicker.CalendarController.calendarInstance.selectedDay;
        user.userId = apiManager.ins.userid;
   
        string json = JsonUtility.ToJson(user);
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(async task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                db = FirebaseDatabase.GetInstance(firebaseUrl);

                db.GetReference("UserData").Child(user.Membercode).SetRawJsonValueAsync(json);
                LoginManager.ins.clr = true;
                LoginManager.ins.showToast("Succesfully signed up!", 3);
                UImanager.ins.SignUpScreen.SetActive(false);
                UImanager.ins.MainScreen.SetActive(true);
                updateUsers();
                LoginManager.ins.OnBackKeyClicked();

            }
        });
    }
    void updateUsers()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(async task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                db = FirebaseDatabase.GetInstance(firebaseUrl);
               
                CurrentUser = new List<User>();
                var snapshot = await db.GetReference("UserData").GetValueAsync();
                string data = snapshot.GetRawJsonValue();

                JSONNode CurrentUser_ = SimpleJSON.JSONNode.Parse(data);

                for (int i = 0; i < CurrentUser_.Count; i++)
                {
                    User us = new User();
                    us.PasswordField = CurrentUser_[i][0];
                    us.DobField = CurrentUser_[i][1];
                    us.Membercode = CurrentUser_[i][2];
                    us.CPasswordField = CurrentUser_[i][3];
                    us.userId = CurrentUser_[i][4];
                    CurrentUser.Add(us);
                }
            }
        });
               
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class UserList
{
    public List<User> currentUser = new List<User>();
}

[System.Serializable]
public class User
{
    public string Membercode;
    public string PasswordField;
    public string CPasswordField;
    public string DobField;
    public string userId;
    public static User CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<User>(jsonString);
    }
}