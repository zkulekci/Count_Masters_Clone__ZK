using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyZone : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] private int spawnAmount;
    [SerializeField] private int currentSize = 0;
    [SerializeField] private TMP_Text counterText;

    void Start()
    {
        GetComponent<Spawner>().SpawnGameObjects(enemyPrefab, transform, spawnAmount);
        currentSize = spawnAmount;
    }

    private void Update()
    {
        counterText.text = currentSize.ToString();

        if (currentSize <= 0)
        {
            GameManager.Instance.ChangeGameState(GameManager.GameState.IN_GAME_PHASE_1);
            Destroy(gameObject);
        }
            
    }

    public void AnEnemyDied()
    {
        currentSize--;
    }
}