using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCollisionMechanics : BaseCollisionMechanics
{
    protected override void OnCollisionEnter(Collision collision)
    {
        BasicCharacter character = collision.collider.GetComponent<BaseObjectView>().Character;
   
        if (character)
        {
            Debug.Log($"AsteroidCollisionMechanics.OnCollisionEnter: Столкнулись с { character.gameObject.name } ");
            _ownerCharacter.SetKiller(character);
            _ownerCharacter.SetDeath();
        }
    }
}
