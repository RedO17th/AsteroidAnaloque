using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjectView : BaseObjectView
{
    public override void Initialize(BasicCharacter character)
    {
        base.Initialize(character);
        GetCollider<SphereCollider>();
    }
}
