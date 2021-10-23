using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIScoreText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textScore;

    public void SetValue(int value)
    {
        _textScore.text = value.ToString();
    }
}
