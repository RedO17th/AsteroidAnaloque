using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIContinueButton : MonoBehaviour
{
    private Button _continueButton;

    private void Awake()
    {
        _continueButton = GetComponent<Button>();
    }

    public void ActivateFunctional(bool state)
    {
        _continueButton.interactable = state;
    }

    public void AddLisener(UnityAction continueMethod)
    {
        _continueButton.onClick.AddListener(continueMethod);
    }
    public void RemoveListener(UnityAction continueMethod)
    {
        _continueButton.onClick.RemoveListener(continueMethod);
    }
}
