using System;
using UnityEngine;

/**
 * Implements a simpler version of RPGCharacterMovementControllerFREE.
 * 
 * We use a state machine here just in case we want to prevent the player from doing
 * certain things over and over again - for example, we shouldn't be able to jump
 * infinitely, or die more than once.
 * **/
public class AnimationController : BasicStateMachine<CharacterState>
{

    public float kMoveSpeed;
    public float kMaxAccel;
    public float rotationSpeed;
    public float jumpSpeed;
    public float maxGravityVel;


    private Animator animator;
    private InputController inputController;
    private Rigidbody rb;

    private int jumpCount = 0;
    public bool isGround = false;
    public Vector3 currentVelocity = Vector3.zero;

    public AnimationController() : base(CharacterState.Idle)
    {
    }

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        inputController = GetComponent<InputController>();
        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            //Set restraints on startup if using Rigidbody - we don't want the character to fall over!
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
        RequestState(CharacterState.Move);
        SetDefaults();
    }

    void Start()
    {

    }

    void Update()
    {

        currentVelocity = Vector3.MoveTowards(currentVelocity, inputController.GetMoveInput() * kMoveSpeed, kMaxAccel * Time.deltaTime);
        Vector3 scalar = new Vector3(1.0f, 1.0f, 1.0f);
        Vector3 moveDelta = Vector3.Scale(currentVelocity, scalar * Time.deltaTime);
        transform.position += moveDelta;

        HandleCurrentState(GetCurrentState());

    }

    protected override CharacterState HandleRequestedState(CharacterState requestedState)
    {
        switch (requestedState)
        {
            case CharacterState.Jump:
                animator.SetTrigger("JumpTrigger");
                animator.SetBool("Moving", false);
                currentVelocity += new Vector3(0.0f, jumpSpeed, 0.0f);
                break;
        }

        return requestedState;
    }

    protected override void HandleCurrentState(CharacterState currentState)
    {
        switch (currentState)
        {
            case CharacterState.Move:
                animator.SetBool("Moving", true);
                animator.SetInteger("Jumping", 0);

                if (inputController.isStrafing())
                {
                    animator.SetFloat("Velocity X", transform.InverseTransformDirection(currentVelocity).x);
                    animator.SetFloat("Velocity Z", transform.InverseTransformDirection(currentVelocity).z);
                }
                else
                {
                    // Linearly interpolate between target rotation (the vector of our movement) and our current rotation
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(inputController.GetMoveInput()), Time.deltaTime * rotationSpeed);
                    animator.SetFloat("Velocity Z", currentVelocity.magnitude);
                }

                break;
            case CharacterState.Jump:
                animator.SetInteger("Jumping", 1);
                animator.SetFloat("Velocity Z", currentVelocity.magnitude);

                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            RequestState(CharacterState.Move);
            isGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = false;
        }
    }

    private void SetDefaults()
    {
        kMoveSpeed = 10;
        kMaxAccel = 10;
        rotationSpeed = 10;
        jumpSpeed = 30;
        maxGravityVel = 10;
    }

}
