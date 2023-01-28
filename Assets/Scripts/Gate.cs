using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Gate : MonoBehaviour
{
    [SerializeField] private Gate_Type gateType;
    [SerializeField] private int size;
    [SerializeField] private TMP_Text sizeText;

    [SerializeField] private GameObject otherGate;

    void Start()
    {
        sizeText.text = gateType == Gate_Type.additive ? "+" : "x";
        sizeText.text += size.ToString();
    }

    public int GetSpawnableSize(int initialSize)
    {
        if (!isActive())
            return 0;

        return gateType == Gate_Type.additive ? size : initialSize * (size - 1); 
    }

    public void UpdateGatesState()
    {
        DeactiveGates();
        HideInnerGate();

    }

    private void DeactiveGates()
    {
        GetComponent<BoxCollider>().enabled = false;
        if (otherGate != null)
            otherGate.GetComponent<BoxCollider>().enabled = false;
    }    

    private void HideInnerGate()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    private bool isActive()
    {
        return GetComponent<BoxCollider>().enabled;
    }

    enum Gate_Type
    {
        additive,
        multiplier
    }

}
