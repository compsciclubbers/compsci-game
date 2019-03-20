using UnityEngine;
using System.Collections;

/**
 * A data container responsible for polling inputs and caching them.
 * Since MonoBehavior is updated at a constant rate, so are these inputs.
 *
 * This is part of a rewrite of RPGCharacterInputControllerFREE, and uses the same input bindings.
 **/
public class PolledInputs : MonoBehaviour
{

    public bool jump, lightHit, death, attackLeft, attackRight, crouch, strafe, roll;
    public float aimVertical, aimHorizontal, inputHorizontal, inputVertical;

    // Use this for initialization
    void Start()
    {
        jump = false;
        lightHit = false;
        death = false;
        attackLeft = false;
        attackRight = false;
        crouch = false;
        strafe = false;
        roll = false;

        aimVertical = 0f;
        aimHorizontal = 0f;
        inputHorizontal = 0f;
        inputVertical = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        jump = Input.GetButtonDown("Jump");
        lightHit = Input.GetButtonDown("LightHit");
        death = Input.GetButtonDown("Death");
        attackLeft = Input.GetButtonDown("AttackL");
        attackRight = Input.GetButtonDown("AttackR");
        crouch = Input.GetButtonDown("SwitchUpDown");
        roll = Input.GetButtonDown("L3");
        strafe = Input.GetKey(KeyCode.LeftShift);

        aimVertical = Input.GetAxisRaw("AimVertical");
        aimHorizontal = Input.GetAxisRaw("AimHorizontal");
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
    }
}
