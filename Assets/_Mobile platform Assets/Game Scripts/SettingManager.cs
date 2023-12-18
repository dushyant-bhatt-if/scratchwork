  using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingManager : MonoBehaviour
{

    public List<Settting> DataObj = new List<Settting>();
    
    public Text TitleTxt,DescTxt;

    public GameObject SettingScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MySetting_SaveBtn(int i)
    {
        TitleTxt.text = DataObj[i].titleTxt;
        DescTxt.text = DataObj[i].DescrptionTxt;
        SettingScreen.GetComponent<PanelAnimator>().StartAnimIn();
    }
}

[Serializable]
public class Settting
{
    public String titleTxt;
    public String DescrptionTxt;
}