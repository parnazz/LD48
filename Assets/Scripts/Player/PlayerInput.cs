using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerInput : IFixedTickable
{
    private SignalBus _signalBus;

    public PlayerInput(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    public void FixedTick()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput != 0)
            _signalBus.Fire(new MoveSignal { moveInput = new Vector2(horizontalInput, 0) });
    }
}
