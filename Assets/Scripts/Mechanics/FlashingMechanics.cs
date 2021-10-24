using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingMechanics : BaseMechanic
{
    //[SerializeField] private int _amountFlashingAtSec = 2;

    private SpatialCharacter _character;

    private bool _isActive = true;

    private int _amountFlashingAtSec = 2;
    private const float _oneSecond = 1f;
    private float _amountIteration = 2f;

    public override void Constructor(SystemInitializer systemInitializer)
    {
        base.Constructor(systemInitializer);
        _character = ((PlayerManagerSystem)systemInitializer.GetSystem(SystemType.PlayerManagerSys)).Player;

        _amountFlashingAtSec = systemInitializer.Data.PlayerData.AmountFlashingAtSec;
    }

    public void Activate()
    {
        _isActive = true;
        float time = (_oneSecond / _amountFlashingAtSec) / _amountIteration;
        StartCoroutine(FlashingTimer(time));
    }    
    private void Deactivate()
    {
        _isActive = false;
    }

    public override void TurnOffMechanics()
    {
        Deactivate();
    }

    IEnumerator FlashingTimer(float time)
    {
        while (_isActive)
        {
            _character.ViewObject.Activate();
            yield return new WaitForSeconds(time);

            _character.ViewObject.Activate(false);
            yield return new WaitForSeconds(time);
        }

        _character.ViewObject.Activate(true);
    }
}
