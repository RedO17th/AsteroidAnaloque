using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScreenAxis { None = -1, HorizontalAxis, VerticalAxis }
public enum TriggerSide { None = -1, Upper, Right, Bottom, Left }

public class ScreenSystem : BaseSystem
{
    public delegate TriggerSide CheckSideCharacter(Vector3 position);
    CheckSideCharacter _sideMethodsList;

    [SerializeField] private List<ScreenOutSideTrigger> _outSideTriggers;
    [SerializeField] private float _outSideOffset = 1f;

    public float XMaxCoord => _xMaxCoord;
    public float YMaxCoord => _yMaxCoord;

    private float _xMaxCoord = 0f;
    private float _yMaxCoord = 0f;

    protected override void InitializeData()
    {
        _xMaxCoord = GetXMaxWayLength();
        _yMaxCoord = GetYMaxWayLength();

        for (int i = 0; i < _outSideTriggers.Count; i++)
            _outSideTriggers[i].Constructor(this);

        _sideMethodsList += CheckUpperSide;
        _sideMethodsList += CheckBottomSide;
        _sideMethodsList += CheckRightSide;
        _sideMethodsList += CheckleftSide;
    }

    private float GetXMaxWayLength()
    {
        Vector3 pxMaxDistance = new Vector3(Screen.width, 0f, 0f);
        Vector3 xVector = Camera.main.ScreenToWorldPoint(pxMaxDistance);

        return xVector.x;
    }
    private float GetYMaxWayLength()
    {
        //Вынести в UIManager [TODO][FIX]
        Vector3 pYxMaxDistance = new Vector3(0f, Screen.height, 0f);
        Vector3 yVector = Camera.main.ScreenToWorldPoint(pYxMaxDistance);

        return yVector.y;
    }

    public void TeleportObject(ScreenOutSideTrigger trigger, SpatialCharacter character)
    {
        Vector3 newPosition = GetReversePosition(trigger, character.Position);
        character.SetPosition(newPosition);
    }
    private Vector3 GetReversePosition(ScreenOutSideTrigger trigger, Vector3 position)
    {
        position *= trigger.ReverseCoeff;
        return SetCoefByTriggerAxis(trigger, position);
    }
    private Vector3 SetCoefByTriggerAxis(ScreenOutSideTrigger trigger, Vector3 position)
    {
        Vector3 newPosition = Vector3.zero;
        switch (trigger.TriggerAxis)
        {
            case ScreenAxis.HorizontalAxis:
                {
                    newPosition = new Vector3(position.x + trigger.AxisOffset, position.y, position.z);
                    break;
                }
            case ScreenAxis.VerticalAxis:
                {
                    newPosition = new Vector3(position.x, position.y + trigger.AxisOffset, position.z);
                    break;
                }
            default:
                {
                    Debug.LogError($"ScreenSystem.SetCoefByTriggerAxis: Unknown trigger axis");
                    newPosition = Vector3.zero;
                    break;
                }
        }

        return newPosition;
    }

    public void CheckPlayerPosition(SpatialCharacter character)
    {
        TriggerSide side = TriggerSide.None;

        for (int i = 0; i < _sideMethodsList.GetInvocationList().Length; i++)
        {
            var checkSide = _sideMethodsList.GetInvocationList()[i];
            side = (TriggerSide)checkSide.DynamicInvoke(character.Position);

            if (side != TriggerSide.None)
                break;
        }

        ScreenOutSideTrigger trigger = GetTriggerByside(side);

        if(trigger)
            TeleportObject(trigger, character);
    }

    private TriggerSide CheckUpperSide(Vector3 position)
    {
        return (position.y > (_yMaxCoord + _outSideOffset)) ? TriggerSide.Upper : TriggerSide.None;
    }
    private TriggerSide CheckBottomSide(Vector3 position)
    {
        return (position.y < (-_yMaxCoord - _outSideOffset)) ? TriggerSide.Bottom : TriggerSide.None;
    }

    private TriggerSide CheckRightSide(Vector3 position)
    {
        return (position.x > (_xMaxCoord + _outSideOffset)) ? TriggerSide.Right : TriggerSide.None;
    }
    private TriggerSide CheckleftSide(Vector3 position)
    {
        return (position.x < (-_xMaxCoord - _outSideOffset)) ? TriggerSide.Left : TriggerSide.None;
    }

    private ScreenOutSideTrigger GetTriggerByside(TriggerSide side) 
    {
        ScreenOutSideTrigger trigger = null;
        for (int i = 0; i < _outSideTriggers.Count; i++)
        {
            if (_outSideTriggers[i].TriggerSide == side)
                trigger = _outSideTriggers[i];
        }

        return trigger;
    }
}
