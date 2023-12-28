using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meshcomponent : MonoBehaviour
{

    public MeshRenderer[] rr;

    // Start is called before the first frame update
    void Start()
    {
        rr = transform.GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer r in rr)
        {
            if (r.GetComponent<MeshCollider>() == null)
                r.GetComponent<GameObject>().AddComponent<MeshCollider>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
