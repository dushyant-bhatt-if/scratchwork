using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class charactercontroller : MonoBehaviour
{   

    public float WalkSpeed = 6.0F;
    public float sprintSpeed;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public float gravityMultiplyer =3;
    public Vector3 PlayerDirection;
    Vector3 PlayerMovement;
    float _OnSprint;
    public CharacterController characterController;
    int jumpcount;
    int maxjumpcount = 2;
    Camera MainCam;
    public CharInput playerinput;
    float velocity;
    public Animator animator;
    public float smoothTime;
    public Vector2 testVel = Vector2.zero ;
//LOCKMOUSE
//ROTATEUPDATE
//MOVEUPDATE5


private void Awake() {
   playerinput = new CharInput();
   playerinput.Enable();
  //  Cursor.lockState = CursorLockMode.Locked;
    characterController=   gameObject.GetComponent<CharacterController>();
    MainCam = Camera.main;

        playerinput.FindAction("Jump").started += ctx => OnJumpPressed(ctx);
        playerinput.FindAction("Jump").canceled += ctx => OnJumpReleased(ctx);


    }
    private void Update() {

    GravityTick();
    RotateTick();
    MoveTick();    
    animator.SetFloat("BlendX",PlayerDirection.x);
    animator.SetFloat("BlendY",PlayerDirection.y);
    characterController.Move(PlayerMovement);

}
void RotateTick(){
    float distToCam = Vector3.Distance(MainCam.transform.position,transform.position);
    gameObject.transform.LookAt(MainCam.transform.position - new Vector3(0,(MainCam.transform.position.y - transform.position.y),0));
    transform.Rotate(0,180,0);
}
    void MoveTick()
    {
    PlayerDirection = Vector2.SmoothDamp(PlayerDirection,playerinput.Player.Move.ReadValue<Vector2>(),ref testVel ,smoothTime);
       print(PlayerDirection);
        if (_OnSprint > 0)
        {
            PlayerMovement = ((transform.right * PlayerDirection.x)* sprintSpeed + (transform.forward * PlayerDirection.y)* sprintSpeed + (transform.up * PlayerMovement.y) )  * Time.deltaTime;
        }
        else
        {
            PlayerMovement = ((transform.right * PlayerDirection.x) * WalkSpeed + (transform.forward * PlayerDirection.y) * WalkSpeed + (transform.up * PlayerMovement.y)) * Time.deltaTime;
        }
        
        }
        void GravityTick(){
           if(characterController.isGrounded){
                velocity = 0.1f;
                jumpcount = 0;
           }
            else{
            velocity +=  gravity * gravityMultiplyer * Time.deltaTime;
            PlayerMovement.y += velocity;
        
            }
        }
    public void OnMove(InputValue ctx){ // gets called when any of the designted move bvuttons change 

 PlayerDirection = ctx.Get<Vector2>();


}
public void OnJumpPressed(InputAction.CallbackContext ctx){

    if(jumpcount >= maxjumpcount) return;
      
        velocity += jumpSpeed;
        jumpcount++;        
} 

public void OnJumpReleased(InputAction.CallbackContext ctx){       
} 
public void OnSprint(InputValue ctx)
    {
        _OnSprint = ctx.Get<float>();
    }

private void OnEnable() {
     //   playerinput.FindAction("Jump").started += ctx=>OnJumpPressed(ctx);
     //   playerinput.FindAction("Jump").canceled += ctx =>OnJumpReleased(ctx);

}
private void OnDisable() {
        playerinput.FindAction("Jump").started -= ctx=>OnJumpPressed(ctx);
        playerinput.FindAction("Jump").canceled -= ctx =>OnJumpReleased(ctx);
}
}
