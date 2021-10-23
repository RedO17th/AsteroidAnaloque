using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidView : BaseObjectView
{

    public override void Initialize(BasicCharacter character)
    {
        base.Initialize(character);
        GetCollider<SphereCollider>();
    }

    public void SetScale(float value)
    {
        transform.localScale = new Vector3(value, value, value);
    }
}
