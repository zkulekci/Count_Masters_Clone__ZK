using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float moveSpeedToCenter = 4f;

    // It keeps the enemyZone game object as a target during a player-enemy fight 
    [HideInInspector] public GameObject temporaryEnemyZone = null;
    [HideInInspector] public GameObject playerZone;

    void Start()
    {
        playerZone = transform.parent.gameObject;
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.currentGameState == GameManager.GameState.IN_GAME_FIGHT)
        {
            transform.position = Vector3.MoveTowards(transform.position, temporaryEnemyZone.transform.position, Time.fixedDeltaTime * moveSpeedToCenter);
        }
        else if (GameManager.Instance.currentGameState == GameManager.GameState.IN_GAME_PHASE_1)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerZone.transform.position, Time.fixedDeltaTime * moveSpeedToCenter);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Gate"))
        {
            Gate gate = other.transform.GetComponent<Gate>();
            int spawnAmount = gate.GetSpawnableSize(playerZone.GetComponent<PlayerZone>().PlayerCount);

            playerZone.GetComponent<PlayerZone>().SpawnPlayers(spawnAmount);
            gate.UpdateGatesState();
        }
        
        else if (other.gameObject.CompareTag("EnemyZone"))
        {
            for (int i = 1; i < playerZone.transform.childCount; i++)
            {
                playerZone.transform.GetChild(i).GetComponent<Player>().temporaryEnemyZone = other.gameObject;
            }

            GameManager.Instance.ChangeGameState(GameManager.GameState.IN_GAME_FIGHT);            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
