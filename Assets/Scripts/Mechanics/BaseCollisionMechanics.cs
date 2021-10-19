using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCollisionMechanics : MonoBehaviour
{
    private BasicCharacter _ownerCharacter;

    public void Constructor(BasicCharacter character)
    {
        _ownerCharacter = character;
    }

    private void OnCollisionEnter(Collision collision)
    {
        BasicCharacter character = collision.collider.GetComponent<BaseObjectView>().Character;
        if (character)
        {
            character.TakeDamage(_ownerCharacter.AmountCollisionDamage);
            _ownerCharacter.SetDeath();
        }
    }
}
