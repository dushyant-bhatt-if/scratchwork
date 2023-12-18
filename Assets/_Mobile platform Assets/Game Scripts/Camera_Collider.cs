using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Collider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log(" Player Enter in "+ other.name);
            FindObjectOfType<CameraController>().SetCamToFirstView();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log(" Player exit in "+ other.name);
            FindObjectOfType<CameraController>().SetCamToThirdView();
        }
    }
}
