﻿using System;
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

    private Animator animator;
    private InputController inputController;
    private Rigidbody rb;

    private int jumpCount = 0;
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
        // Zero out current velocity
        currentVelocity = Vector3.zero;

        currentVelocity = Vector3.MoveTowards(currentVelocity, inputController.GetMoveInput() * kMoveSpeed, kMaxAccel * Time.deltaTime);
        Vector3 moveDelta = Vector3.Scale(currentVelocity, new Vector3(1.0f, 0.0f, 1.0f)) * Time.deltaTime;
        transform.position += moveDelta;

        HandleCurrentState(GetCurrentState());

    }

    protected override CharacterState HandleRequestedState(CharacterState requestedState)
    {
        switch (requestedState)
        {
            case CharacterState.Jump:
                animator.SetInteger("Jumping", 1);
                animator.SetTrigger("JumpTrigger");
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
                break;
        }
    }

    private void SetDefaults()
    {
        kMoveSpeed = 300.0f;
        kMaxAccel = 300.0f;
        rotationSpeed = 20.0f;
        jumpSpeed = 300.0f;
    }

}
