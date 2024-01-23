using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject MenuScreen;

    private void Start() {
       // DontDestroyOnLoad(this);              
    }
    private void Update()
    {
       // RenderSettings.skybox.SetFloat("_Rotation", Time.time * 2f);
    }
    public void LoadNewScene(string scene){
    SceneManager.LoadScene(scene);
}
    public void UI_ExitBtn()
    {
        PlayerPrefs.SetString("membercode", "");
        PlayerPrefs.SetString("Passcode", "");
        SceneManager.LoadScene(0);

    }

    public void UI_Setting()
    {

    }
    public void onSliderClick()
    {
        var thirdPersonFollow = CameraController.ins.Camera3rdView.GetCinemachineComponent<Cinemachine.Cinemachine3rdPersonFollow>();

        if (MenuScreen.transform.localPosition.x == 0)
        { MenuScreen.GetComponent<Animation>().Play("SliderClose");
            StartCoroutine(LerpPosition(0f, 0.5f));
            Debug.Log("SliderClose 5:");

           // thirdPersonFollow.CameraDistance = 5;
        }
        else
        {   MenuScreen.GetComponent<Animation>().Play("SliderOpen");
            StartCoroutine(LerpPosition(0.5f, 0f));
           // thirdPersonFollow.CameraDistance = 3;

            Debug.Log("SliderOpen 3 :");


        }

    }

    IEnumerator LerpPosition(float currentPos, float targetPos)
    {
        float time = 0;
        var thirdPersonFollow = CameraController.ins.Camera3rdView.GetCinemachineComponent<Cinemachine.Cinemachine3rdPersonFollow>();
        float duration = 0.5f;

        while (time < 0.7f)
        {
            thirdPersonFollow.CameraSide = Mathf.Lerp(currentPos, targetPos, time / duration);

            time += Time.deltaTime;
            yield return null;
        }
        //transform.position = targetPosition;
    }

}
