using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int numberOfAIToSpawn = 5;
    public AIController AIPrefab;
    public Transform[] SpawnLocations;
    public int scoreThreshold = 3;
    public float defaultTimeLimit = 10.0f;
    public Text scoreText;
    public Text timerText;
    public GameObject menuPanel;
    public GameObject scorePanel;
    public GameObject gameOverPanel;
    GameObject[] uiPanels;

    void Awake()
    {
        InitializeServices();
        uiPanels = new GameObject[]{ menuPanel, scorePanel, gameOverPanel };
        ShowMenuPanel();
    }

    void InitializeServices()
    {
        // allow us to define order of initialization
        Services.gameManager = this;
        Services.InitializeServices();
        Services.AIManager.Initialize(numberOfAIToSpawn, AIPrefab);
        Services.gameStateController.Initialize();
    }

    // Update is called once per frame
    public void Update()
    {
        Services.gameStateController.Update();

        if(Services.gameStateController.CurrentState.GetType() == typeof(StateGameInProgress))
        {
            Services.AIManager.Update();
        }
    }

    public void OnDestroy()
    {
        Services.AIManager.DestroyAI();
    }

    public void BeginGame()
    {
        Services.eventManager.FireEvent(new StartGame());
    }

    public void ShowScorePanel()
    {
        ClearPanels();
        scorePanel.SetActive(true);
    }

    public void ShowGameOverPanel()
    {
        ClearPanels();
        gameOverPanel.SetActive(true);
    }

    public void ShowMenuPanel()
    {
        ClearPanels();
        menuPanel.SetActive(true);
    }

    void ClearPanels()
    {
        foreach (GameObject panel in uiPanels)
            panel.SetActive(false);
    }
}