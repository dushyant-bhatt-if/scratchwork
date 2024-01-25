using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public static UImanager ins;
    public GameObject SavedGameScreen,
        MainScreen,
        LoginScreen,
        SignUpScreen,
        loginmenuScreen,
        DashboardScreen,
        loginScreen1,
        loginScreen2,
        loadingScreen;
    public InputField searchBar;
    public Text lableTxt;
    private void Awake()
    {
        if (ins == null)
            ins = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region UI button functions
    public void ButtonClick(string BtnName)
    {
        Debug.Log(BtnName);
    }

    public void OnPlayClick()
    {
        loadingScreen.SetActive(true);
        SceneManager.LoadSceneAsync(1);
    }

    public void OnSavedGameClicked()
    {
        MainScreen.SetActive(false);
        SavedGameScreen.SetActive(true);
    }
    RectTransform panelS;
    public void OnBackToMainScreen()
    {
        MainScreen.SetActive(true);
        SavedGameScreen.SetActive(false);
        SignUpScreen.SetActive(false);
        LoginScreen.SetActive(false);
        searchBar.text = "";
        transform.GetComponent<LoginManager>().OnBackKeyClicked();
    }

    public void onLoginBtnClick()
    {
        MainScreen.SetActive(false);
        LoginScreen.SetActive(true);
    }

    public void OnSignupBtnClick()
    {
        MainScreen.SetActive(false);
        SignUpScreen.SetActive(true);
    }

    public void GetOnloginSuccess(string _mCode)
    {
        MainScreen.SetActive(true);
        LoginScreen.SetActive(false);
        loginmenuScreen.SetActive(false);
        DashboardScreen.SetActive(true);
        lableTxt.text = "Welcome  " + _mCode + "!";

        loadingScreen.SetActive(false);

    }


    public void OnSwitchUsserClick()
    {
        MainScreen.SetActive(false);
        LoginScreen.SetActive(true);
        LoginManager.ins.OnBackKeyClicked();
    }
    
    #endregion
}
