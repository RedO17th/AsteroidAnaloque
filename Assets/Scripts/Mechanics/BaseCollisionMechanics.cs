using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCollisionMechanics : MonoBehaviour
{
    protected BasicCharacter _ownerCharacter;

    public virtual void Constructor(BasicCharacter character)
    {
        _ownerCharacter = character;
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        BasicCharacter character = collision.collider.GetComponent<BaseObjectView>().Character;
        if (character)
        {
            character.SetKiller(_ownerCharacter);
            character.TakeDamage(_ownerCharacter.AmountCollisionDamage);

            _ownerCharacter.SetDeath();
        }
    }
}
