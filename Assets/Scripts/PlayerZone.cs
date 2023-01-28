using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerZone : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] private TMP_Text counterText;
    [HideInInspector] public int PlayerCount; 

    private void Start()
    {
        UpdatePlayerCount();
    }

    private void Update()
    {
        UpdatePlayerCount();
    }

    private void UpdateCounterText()
    {
        counterText.text = PlayerCount.ToString();
    }

    public void HideCounterText()
    {
        counterText.gameObject.SetActive(false);
    }

    public void UpdatePlayerCount()
    {
        PlayerCount = transform.childCount - 1;
        UpdateCounterText();
    }

    public void SpawnPlayers(int spawnAmount)
    {
        GetComponent<Spawner>().SpawnGameObjects(playerPrefab, transform, spawnAmount);
    }
}
