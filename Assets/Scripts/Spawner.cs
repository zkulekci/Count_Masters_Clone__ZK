using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public void SpawnGameObjects(GameObject prefab, Transform parentTransform, int spawnAmount)
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Instantiate(prefab, transform.position + Random.insideUnitSphere * 0.2f, Quaternion.identity, parentTransform);
        }
    }
}
