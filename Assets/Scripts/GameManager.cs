using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Camera mainCamera;
    public GameObject[] prefabLevels;

    [HideInInspector] public GameObject currentLevelGO;
    [HideInInspector] public GameObject roadGO;
    [HideInInspector] public GameObject playerZoneGO;
    [HideInInspector] public int currentLevel = 1;
    [HideInInspector] public GameState currentGameState;
    [HideInInspector] public float progressValue = 0f;

    [Header("UI Panels", order = 1)]
    [SerializeField] public GameObject UI_TapToStart;
    [SerializeField] public GameObject UI_ProgressBar;
    [SerializeField] public GameObject UI_WinPanel;
    [SerializeField] public GameObject UI_LosePanel;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }

        LoadLevel(currentLevel);
    }

    void Start()
    {
        ChangeGameState(GameState.READY_TO_START);
    }

    void Update()
    {
        if (currentGameState == GameState.READY_TO_START)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ChangeGameState(GameState.IN_GAME_PHASE_1);
            }   
        }

        if (currentGameState == GameState.IN_GAME_PHASE_1)
        {
            CalculateProgressValue();

            if (IsPlayerCountZero())
            {
                StartCoroutine(nameof(LevelFailed));
            }

            if (IsLevelFinished())
            {
                ChangeGameState(GameState.IN_GAME_PHASE_2);
                StartCoroutine(nameof(LevelCompleted));
                playerZoneGO.GetComponent<PlayerZone>().HideCounterText();
            }
        }

        if (currentGameState == GameState.IN_GAME_FIGHT)
        {
            if (IsPlayerCountZero())
            {
                StartCoroutine(nameof(LevelFailed));
            }
        }

    }

    private void LoadLevel(int level)
    {
        UnloadLevel();
        currentLevelGO = Instantiate(prefabLevels[level - 1]);
        AssignGameObjects(currentLevelGO);
        ChangeGameState(GameState.READY_TO_START);
    }

    private void UnloadLevel()
    {
        if (currentLevelGO != null)
            Destroy(currentLevelGO);
    }

    private void AssignGameObjects(GameObject parent)
    {
        playerZoneGO = parent.gameObject.transform.GetChild(0).gameObject;
        roadGO = parent.gameObject.transform.GetChild(1).gameObject;
        mainCamera.Setup();
    } 

    private bool IsPlayerCountZero()
    {
        return playerZoneGO.GetComponent<PlayerZone>().PlayerCount == 0;
    }

    private bool IsLevelFinished()
    {
        return progressValue >= 1f;
    }

    private void CalculateProgressValue()
    {
        float completedRoadLength = playerZoneGO.transform.position.z - roadGO.GetComponent<Road>().GetStartPointZ();
        float totalRoadLength = roadGO.GetComponent<Road>().GetFinishPointZ() - roadGO.GetComponent<Road>().GetStartPointZ();

        progressValue = completedRoadLength / totalRoadLength;
    }

    public void ChangeGameState(GameState gameState)
    {
        currentGameState = gameState;
        UpdateUI();
    }

    public GameState GetCurrentGameState()
    {
        return currentGameState;
    }

    public void RestartLevel()
    {
        LoadLevel(currentLevel);
    }

    public void NextLevel()
    {
        currentLevel++;
        if (currentLevel > prefabLevels.Length)
            currentLevel = 1;
        LoadLevel(currentLevel);
    }

    public void UpdateUI()
    {
        HideAllPanels();
        switch (currentGameState)
        {
            case GameState.READY_TO_START:
                UI_TapToStart.SetActive(true);
                break;
            case GameState.IN_GAME_PHASE_1:
            case GameState.IN_GAME_PHASE_2:
            case GameState.IN_GAME_FIGHT:
            case GameState.WAIT_FOR_LEVEL_COMPLETED:
            case GameState.WAIT_FOR_LEVEL_FAILED:
                UI_ProgressBar.SetActive(true);
                break;
            case GameState.LEVEL_COMPLETED:
                UI_WinPanel.SetActive(true);
                break;
            case GameState.LEVEL_FAILED:
                UI_LosePanel.SetActive(true);
                break;
            default:
                break;
        }
    }

    private void HideAllPanels()
    {
        UI_TapToStart.SetActive(false);
        UI_ProgressBar.SetActive(false);
        UI_WinPanel.SetActive(false);
        UI_LosePanel.SetActive(false);
    }

    IEnumerator LevelCompleted()
    {
        yield return new WaitForSeconds(2f);
        ChangeGameState(GameState.WAIT_FOR_LEVEL_COMPLETED);
        yield return new WaitForSeconds(2f);
        ChangeGameState(GameState.LEVEL_COMPLETED);
    }

    IEnumerator LevelFailed()
    {
        ChangeGameState(GameState.WAIT_FOR_LEVEL_FAILED);
        yield return new WaitForSeconds(2f);
        ChangeGameState(GameState.LEVEL_FAILED);
    }

    public enum GameState
    {
        READY_TO_START,
        IN_GAME_PHASE_1,
        IN_GAME_PHASE_2,
        IN_GAME_FIGHT,
        WAIT_FOR_LEVEL_COMPLETED,
        LEVEL_COMPLETED,
        WAIT_FOR_LEVEL_FAILED,
        LEVEL_FAILED
    }
}
