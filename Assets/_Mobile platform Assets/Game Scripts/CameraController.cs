using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraController : MonoBehaviour
{
    public static CameraController ins;
    CinemachineVirtualCamera _actiwCamera;
    int activeCameraPriorityModife = 32506;

    public CinemachineVirtualCamera Camera1stView;
    public CinemachineVirtualCamera Camera3rdView;
    public GameObject CharacterModal;

    private void Awake()
    {
        if (ins == null)
            ins = this;
    }
    //public CinemachineVirtualCamera CameraOrbit;
    // Start is called before the first frame update
    void Start()
    {
        Camera3rdView.Priority += activeCameraPriorityModife;
        _actiwCamera = Camera3rdView;
    }

    // Update is called once per frame
    void Update()
    {
      //  if (Input.GetKey(KeyCode.M))
        //   ChangeCamera();
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
