using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidView : BaseObjectView
{
    public void SetScale(float value)
    {
        transform.localScale = new Vector3(value, value, value);
    }
}
