using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleCylinder : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 2f;

    void Update()
    {
        transform.Rotate(new Vector3(0f, rotateSpeed * Time.deltaTime, 0f));
    }
}
