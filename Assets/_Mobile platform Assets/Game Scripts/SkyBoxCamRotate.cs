
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class SkyBoxCamRotate : MonoBehaviour
{
    public float rotateFactor=0.005f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotateFactor, 0);
    }
}