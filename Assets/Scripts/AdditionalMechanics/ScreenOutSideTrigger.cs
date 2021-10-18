using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenOutSideTrigger : MonoBehaviour
{
    [SerializeField] private ScreenAxis _triggerAxis;
    [SerializeField] private Vector2 _reverseCoeff;
    
    [Range(-0.5f, 0.5f)]
    [SerializeField] private float _axisOffset;

    private ScreenSystem _screenSystem;

    public ScreenAxis TriggerAxis => _triggerAxis;
    public Vector2 ReverseCoeff => _reverseCoeff;
    public float AxisOffset => _axisOffset;

    public void Constructor(ScreenSystem screenSystem)
    {
        _screenSystem = screenSystem;
    }

    private void OnTriggerEnter(Collider other)
    {
        BaseCharacter character = other.attachedRigidbody.GetComponent<BaseCharacter>();
        if (character)
            _screenSystem.TeleportObject(this, character);
    }
}