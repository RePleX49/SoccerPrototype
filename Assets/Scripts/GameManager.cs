using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int numberOfAIToSpawn = 5;
    public AIController AIPrefab;
    public Transform[] SpawnLocations;

    // Start is called before the first frame update
    void Start()
    {
        Services.gameManager = this;
        Services.InitializeServices();
        Services.AIManager.Initialize(numberOfAIToSpawn, AIPrefab);
    }

    // Update is called once per frame
    public void Update()
    {
        Services.AIManager.Update();
    }

    public void OnDestroy()
    {
        Services.AIManager.DestroyAI();
    }
}
