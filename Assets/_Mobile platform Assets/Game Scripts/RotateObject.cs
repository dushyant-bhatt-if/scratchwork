using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 50f;

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            coinManager.instance.counter+=1;
            coinManager.instance.SetCOunter();
            Destroy(this.gameObject); 
        }
    }
}

