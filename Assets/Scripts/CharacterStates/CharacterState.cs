using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterState
{
    public abstract void Tick(Character character);
    public abstract void HandleInput(Character character);
    public abstract void OnEnterState(Character character);
    public abstract void OnExitState(Character character);
}
