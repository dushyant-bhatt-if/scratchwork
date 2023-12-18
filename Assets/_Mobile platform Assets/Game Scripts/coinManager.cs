using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class coinManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]private Text coinCounter;
   public int counter;
   public AudioSource coin_clip;
    public static coinManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        counter = PlayerPrefs.GetInt("coins",0);
        coinCounter.text = counter.ToString();
        SpawnObject();
    }

     public void SetCOunter()
    {
        coinCounter.text = counter.ToString();
        Debug.Log("Coins :"+counter);
        PlayerPrefs.SetInt("coins", counter);
        coin_clip.Play();
        if(counter%10==0)
        {
            SpawnObject();
        }
    }
    public GameObject objectToSpawn;
    public Vector3 spawnRangeMin;
    public Vector3 spawnRangeMax;

    void SpawnObject()
    {
        for (int i = 0; i < 10; i++)
        {
            float randomX = Random.Range(spawnRangeMin.x, spawnRangeMax.x);
            float randomY = Random.Range(spawnRangeMin.y, spawnRangeMax.y);
            float randomZ = Random.Range(spawnRangeMin.z, spawnRangeMax.z);

            Vector3 randomSpawnPosition = new Vector3(randomX, randomY, randomZ);
            GameObject spwnObj=  Instantiate(objectToSpawn, randomSpawnPosition, Quaternion.identity);
            spwnObj.name = "obj" + i;
            spwnObj.transform.localScale = Vector3.one*0.3f;
            spwnObj.transform.localPosition = randomSpawnPosition;
        }
    }
}


