using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingMechanics : MonoBehaviour
{
    [SerializeField] private int _amountFlashingAtSec = 2;

    private BasicMovingCharacter _character;

    private bool _isActive = true;

    private const float _oneSecond = 1f;
    private float _amountIteration = 2f;

    public void Constructor(PlayerManagerSystem playerManager)
    {
        _character = playerManager.Player;
    }

    public void Activate()
    {
        float time = (_oneSecond / _amountFlashingAtSec) / _amountIteration;
        StartCoroutine(FlashingTimer(time));
    }    
    public void Deactivate()
    {
        _isActive = false;
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
