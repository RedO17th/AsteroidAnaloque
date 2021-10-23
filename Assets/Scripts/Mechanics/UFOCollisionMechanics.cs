using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOCollisionMechanics : BaseCollisionMechanics
{
    protected override void OnCollisionEnter(Collision collision)
    {
        MovementCharacter character = collision.rigidbody.GetComponent<MovementCharacter>();
        if (character)
        {
            _ownerCharacter.SetKiller(character);
            character.TakeDamage(_ownerCharacter.AmountCollisionDamage);
        }
    }
}
