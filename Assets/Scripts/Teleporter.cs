using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class Teleporter : MonoBehaviour
{
  public GameManager gameManager;

  
   public string SceneToLoad;

private void Start() {
    gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
}

    public void SceneChangeFromPlayer(){
        gameManager.LoadNewScene(SceneToLoad);

    }
    
}
