using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
public class CameraController : MonoBehaviour
{
    public static CameraController ins;
    CinemachineVirtualCamera _actiwCamera;
    int activeCameraPriorityModife = 32506;

    public CinemachineVirtualCamera Camera1stView;
    public CinemachineVirtualCamera Camera3rdView;
    public GameObject CharacterModal;

    public GameObject player;
    public Transform _pos;

    private void Awake()
    {
        if (ins == null)
            ins = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Camera3rdView.Priority += activeCameraPriorityModife;
        _actiwCamera = Camera3rdView;
        int a = PlayerPrefs.GetInt("isReturn");
        Debug.Log("Here... calls from " + transform.name + PlayerPrefs.GetInt("isReturn"));
        Debug.Log(a + "......... isReturn ");
        if (a==1)
        { player.transform.position = _pos.localPosition;// new Vector3(-47, 9.6f, -100);
            PlayerPrefs.SetInt("isReturn", 0);

        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        { SetCamToFirstView(); }
    }

   public void ChangeCamera()
    {
        if(Camera3rdView == _actiwCamera)
        {
            CharacterModal.SetActive(false);
            SetCameraPriorities(Camera3rdView, Camera1stView);
          //  usingOrbitCam = false;
        }
        else if(Camera1stView == _actiwCamera)
        {
            CharacterModal.SetActive(true);
            SetCameraPriorities(Camera1stView, Camera3rdView);
           // usingOrbitCam = true;
        }
        else
        {
            Camera3rdView.Priority += activeCameraPriorityModife;
            _actiwCamera = Camera3rdView;
        }
    }

    public void SetCamToFirstView()
    {
        CharacterModal.SetActive(false);
        SetCameraPriorities(Camera3rdView, Camera1stView);
    }

    public void SetCamToThirdView()
    {
        CharacterModal.SetActive(true);
        SetCameraPriorities(Camera1stView, Camera3rdView);
    }
    void SetCameraPriorities(CinemachineVirtualCamera currentCam, CinemachineVirtualCamera NextCam)
    {
        currentCam.Priority -= activeCameraPriorityModife;
        NextCam.Priority += activeCameraPriorityModife;
        _actiwCamera = NextCam;
    }
}
