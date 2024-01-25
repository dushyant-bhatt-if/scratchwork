using UnityEngine;
using System.Collections;
using Cinemachine.Utility;
using UnityEngine.SceneManagement;


public class CameraMovement : MonoBehaviour
{
	private Quaternion originalRotation; // Variable to store the original rotation
	public Transform targetRotation; // Assign the target rotation in the Unity Editor
	public float rotationSpeed = 2f; // Speed of the rotation
	public float lerpDuration = 2f; // Duration of the lerp operation

	void Start()
	{
	}
	
	public void CLickOnBtn()
	{
		if (MenuScreen.transform.localScale.x == 0)
		{
			originalRotation = transform.rotation;

			transform.GetComponent<Cinemachine.CinemachineBrain>().enabled = false;
			StartCoroutine(RotateCamera());
		}
		else
		{
			StartCoroutine(OnReturnPos());
		}
	}

	
	// Coroutine for smoothly rotating the camera
	private IEnumerator RotateCamera()
	{
		//while (true)
			float elapsedTime = 0f;
			// Lerping the rotation to the target rotation
			while (elapsedTime < lerpDuration)
			{
				elapsedTime += Time.deltaTime;
				float lerpAmount = elapsedTime / lerpDuration;
				Quaternion newRotation = Quaternion.Lerp(originalRotation, targetRotation.rotation, lerpAmount * rotationSpeed);
				// Apply the new rotation to the camera
				transform.rotation = newRotation;
				yield return null;
			}
		MenuScreen.GetComponent<Animation>().Play("SliderClose");
	}

	public GameObject MenuScreen;

	public IEnumerator OnReturnPos()
    {
		MenuScreen.GetComponent<Animation>().Play("SliderOpen");
		float elapsedTime = 0f;

				// Lerping the rotation back to the original rotation
				while (elapsedTime < lerpDuration)
				{
					elapsedTime += Time.deltaTime;
					float lerpAmount = elapsedTime / lerpDuration;
					Quaternion newRotation = Quaternion.Lerp(targetRotation.rotation, originalRotation, lerpAmount * rotationSpeed);
					// Apply the new rotation to the camera
					transform.rotation = newRotation;
					yield return null;
				}
		transform.GetComponent<Cinemachine.CinemachineBrain>().enabled = true;
		yield return null;
		// Wait for a brief moment before starting the rotation again
	}
}
