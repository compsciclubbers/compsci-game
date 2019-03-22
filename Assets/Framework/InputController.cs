using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PolledInputs))]
/**
 * This class is responsible for taking inputs and transforming them into a format 
 * suitable for controlling a character (Vector3).
 * 
 * This is a rewrite of RPGCharacterControllerFREE.
 * **/
public class InputController : MonoBehaviour
{

    [HideInInspector]
    private PolledInputs polledInputs;
    [HideInInspector]
    private AnimationController animController;

    private bool inputsAllowed = true;

    // Character moves up/down, left/right, forward/backward
    private Vector3 moveInput;
    // Character can look left/right, up/down
    private Vector2 aimInput;

    // Use this for initialization
    void Start()
    {
        polledInputs = GetComponent<PolledInputs>();
        animController = GetComponent<AnimationController>();
        ZeroInputs();
    }

    // Update is called once per frame
    void Update()
    {
        if(inputsAllowed)
        {
            moveInput = CameraRelativeInput(polledInputs.inputHorizontal, polledInputs.inputVertical);
            aimInput = new Vector2(polledInputs.aimHorizontal, polledInputs.aimVertical);

            if(polledInputs.jump)
            {
                animController.RequestState(CharacterState.Jump);
            }

        }
    }

    /**
     * Transforms inputs so they are relative to the direction that the camera
     * is facing. 
     * 
     * This implementation is from RPGCharacterInputControllerFREE.
     * **/
    Vector3 CameraRelativeInput(float inputHorizontal, float inputVertical)
    {
        //Forward vector relative to the camera along the x-z plane   
        Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
        forward.y = 0;
        forward = forward.normalized;
        //Right vector relative to the camera always orthogonal to the forward vector.
        Vector3 right = new Vector3(forward.z, 0, -forward.x);
        Vector3 relativeVelocity = inputHorizontal * right + inputVertical * forward;
        //Reduce input for diagonal movement.
        if (relativeVelocity.magnitude > 1)
        {
            relativeVelocity.Normalize();
        }
        return relativeVelocity;
    }

    public void ZeroInputs()
    {
        moveInput = new Vector3();
        aimInput = new Vector2();
    }

    public bool isStrafing()
    {
        return polledInputs.strafe;
    }

    public Vector3 GetMoveInput()
    {
        return moveInput;
    }

    public Vector3 GetAimInput()
    {
        return aimInput;
    }

}
