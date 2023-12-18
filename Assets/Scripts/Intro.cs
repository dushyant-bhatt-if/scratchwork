using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{

  public float waitTime = 12f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(introStart());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator introStart()
    {
      yield return new WaitForSeconds(waitTime);

      SceneManager.LoadScene("Main Menu");

    }
}
