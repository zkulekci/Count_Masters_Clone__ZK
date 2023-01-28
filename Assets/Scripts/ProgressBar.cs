using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image progressFillBar;
    [SerializeField] private TMP_Text currentLevel;

    void Update()
    {
        progressFillBar.fillAmount = GameManager.Instance.progressValue;
    }

    private void OnEnable()
    {
        currentLevel.text = GameManager.Instance.currentLevel.ToString();
    }
}
