using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getLight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject lightObj;
    GameObject CloneObj;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log(" Player Enter in " + other.name);
            CloneObj = Instantiate(lightObj);
            CloneObj.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log(" Player exit in " + other.name);
           // Destroy(CloneObj, 3);
        }
    }
}
