using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TapToStart : MonoBehaviour
{
    [SerializeField] TMP_Text levelText;

    private void OnEnable()
    {
        levelText.text = "Level " + GameManager.Instance.currentLevel.ToString();
    }
}
