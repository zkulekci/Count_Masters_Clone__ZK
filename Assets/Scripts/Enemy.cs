using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float moveSpeedToCenter = 4f;
    [HideInInspector] public GameObject enemyZone;

    void Start()
    {
        enemyZone = transform.parent.gameObject;
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.currentGameState == GameManager.GameState.IN_GAME_FIGHT)
        {
            Transform playerZone = GameManager.Instance.playerZoneGO.transform;
            transform.position = Vector3.MoveTowards(transform.position, playerZone.position, Time.fixedDeltaTime * moveSpeedToCenter);
        }
        else if (GameManager.Instance.currentGameState == GameManager.GameState.IN_GAME_PHASE_1)
        {
            transform.position = Vector3.MoveTowards(transform.position, enemyZone.transform.position, Time.fixedDeltaTime * moveSpeedToCenter);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemyZone.GetComponent<EnemyZone>().AnEnemyDied();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
