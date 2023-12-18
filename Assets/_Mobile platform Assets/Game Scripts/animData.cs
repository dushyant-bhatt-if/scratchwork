using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class animData : MonoBehaviour
{

    public Animator AvtarObj;
    [SerializeField] GameObject ParentObj;

    [SerializeField] GameObject MaleAvtarObj;
    [SerializeField] GameObject FemaleAvtarObj;  
    public Avatar male_Avtar,Female_Avtar;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void SelectMale()
    {
        AvtarObj.avatar = male_Avtar;
        MaleAvtarObj.SetActive(true);
        ParentObj.SetActive(false);

        Cursor.visible = false;
    }
    public void SelectFemale()
    {
        AvtarObj.avatar = Female_Avtar;
        FemaleAvtarObj.SetActive(true);
        ParentObj.SetActive(false);
    }

    public void Restart()
    {
        
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
