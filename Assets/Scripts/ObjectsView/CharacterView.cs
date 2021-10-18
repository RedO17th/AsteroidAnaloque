using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterView : BaseObjectView
{
    public override void Initialize()
    {
        GetCollider<BoxCollider>();
    }
}
