using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOCollisionMechanics : BaseCollisionMechanics
{
    protected override void OnCollisionEnter(Collision collision)
    {
        BasicCharacter character = collision.collider.GetComponent<BaseObjectView>().Character;
        if (character)
        {
            _ownerCharacter.SetKiller(character);
            _ownerCharacter.SetDeath();
        }
    }
}
