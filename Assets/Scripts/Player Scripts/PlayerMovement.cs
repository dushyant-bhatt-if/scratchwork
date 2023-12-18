using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    // current speed the player is traveling at.

    public float maxSpeed;// the max speed the player can go
    public float accelerationX;
    public float accelerationY;
    public Vector3 currentVel;
    public float gravity;
    public CharacterController characterController;
    public InputActionManager inputActionManager;
    public void Awake()
    {
        inputActionManager = GetComponent<InputActionManager>();

    }

    public void FixedUpdate()
    {
        // use fixeddeltatime


    }
    public void Update()
    {
        characterController.Move(new Vector3(0, -gravity, 0) * Time.deltaTime);

        if (inputActionManager.Move.x != 0)
        {
            if (currentVel.x < maxSpeed)
            {
                currentVel.x += inputActionManager.Move.x * accelerationX * Time.deltaTime;
                characterController.Move(currentVel);

            }
        }
        if(inputActionManager.Move.x == 0 )
        {
            if(currentVel.x >   0)  
            currentVel.x -= accelerationX * Time.deltaTime;   
            else if (currentVel.x < 0 )
            {
                currentVel.x += accelerationX * Time.deltaTime;
            }
        }
        
    }
}
