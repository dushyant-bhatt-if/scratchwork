using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal_Collider : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (transform.tag == "NextScene" && other.tag == "Player")
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                Debug.Log(other.transform.localPosition);
                SceneManager.LoadSceneAsync(2);
            }
            else
            {
                PlayerPrefs.SetInt("isReturn", 1);
                SceneManager.LoadSceneAsync(1);
            }
        }       
    }
}
