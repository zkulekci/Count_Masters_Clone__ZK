using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder : MonoBehaviour
{
    [SerializeField] Vector3 rotate;
    [SerializeField] float rotateSpeed;

    void Update()
    {
        transform.Rotate(rotate * Time.deltaTime * rotateSpeed);       
    }
}
