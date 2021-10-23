using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionMechanics : BaseCollisionMechanics
{
    private Player _player;

    public override void Constructor(BasicCharacter character)
    {
        base.Constructor(character);
        _player = (Player)character;
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        _player.PlayerManagerSystem.SetStartFlashingMechanic();
    }
}
