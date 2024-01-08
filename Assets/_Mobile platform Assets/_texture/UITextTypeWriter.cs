using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextTypeWriter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public Text txt;
	public string story;

	void Awake()
	{
		//txt = GetComponent<Text>();
		story = "Welcome to the MyVillage Project Knowledge Experience!  Go search for some knowledge.....";// txt.text;
		txt.text = "";

		
	}

	public void StartWriting()
    {
		StartCoroutine("PlayText");
	}
	IEnumerator PlayText()
	{
		foreach (char c in story)
		{
			txt.text += c;
			yield return new WaitForSeconds(0.2f);		
		}

		yield return new WaitForSeconds(2.0f);
		txt.text = "";
		StartCoroutine("PlayText");
	}
}
