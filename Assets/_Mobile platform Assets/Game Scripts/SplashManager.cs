using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SplashManager : MonoBehaviour
{


    [SerializeField] private GameObject CurrentScreen;
    [SerializeField] private GameObject NextScreen;
    // Start is called before the first frame update
    public void LoadStart()
    {
        StartCoroutine(loadScene());   
    }
    IEnumerator loadScene()
    {
            yield return new  WaitForSeconds(0.2f);
            CurrentScreen.SetActive(false);
            NextScreen.SetActive(true);
    }
}
