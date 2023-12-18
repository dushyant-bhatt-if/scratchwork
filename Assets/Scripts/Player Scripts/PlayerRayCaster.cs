using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerRayCaster : MonoBehaviour
{
    public Camera playerCam;
    public LayerMask Teleportermask;
    private void Start() {
        playerCam = Camera.main;
    }
  

  
  GameObject RayCastInFront(){
    RaycastHit raycastHit;

     Physics.Raycast(playerCam.transform.position,playerCam.transform.forward,out raycastHit,20,Teleportermask);
    if(raycastHit.collider != null){
         return  raycastHit.collider.gameObject;
            
    }else{
        return null;
    }
  }

void OnInteract(InputValue ctx){
 GameObject currentTeleporter =  RayCastInFront();
    if(currentTeleporter != null){
            currentTeleporter.GetComponent<Teleporter>().SceneChangeFromPlayer();
    }
}


}
