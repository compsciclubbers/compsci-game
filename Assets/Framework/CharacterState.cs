using UnityEngine;
using System.Collections;

/**
 * Describes animation states of our character.
 * 
 * This implementation is from RPGCharacterStateFREE.
 * **/
public enum CharacterState
{

    Idle = 0,
    Move = 1,
    Jump = 2,
    DoubleJump = 3,
    Fall = 4,
    Block = 6,
    Roll = 8

}
