using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] private Transform start;
    [SerializeField] private Transform finish;

    public float GetStartPointZ()
    {
        return start.position.z;
    }

    public float GetFinishPointZ()
    {
        return finish.position.z;
    }
}
