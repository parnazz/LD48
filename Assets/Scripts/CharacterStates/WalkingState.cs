using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : CharacterState
{
    public override void HandleInput(Character character)
    {
        
    }

    public override void OnEnterState(Character character)
    {
        
    }

    public override void OnExitState(Character character)
    {
        
    }

    public override void Tick(Character character)
    {
        //character._rb.position = (Vector2)character.transform.position 
        //    + character.transform.right * Time.fixedDeltaTime * _playerSpeed;
    }
}
