using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AILifeCycleManager
{   
    List<AIController> AIPlayers;

    public void Initialize(int numToSpawn, AIController AIPrefab, AIController refereePrefab)
    {
        AIPlayers = new List<AIController>();
        CreateAI(numToSpawn, AIPrefab);
        AIPlayers.Add(Object.Instantiate(refereePrefab, Services.gameManager.SpawnLocations[0]));
    }

    public void CreateAI(int numToSpawn, AIController AIPrefab)
    {
        for (int i = 0; i < numToSpawn; i++)
        {
            AIPlayers.Add(Object.Instantiate(AIPrefab, Services.gameManager.SpawnLocations[i]));
        }    
    }

    // called by GameManager
    public void Update()
    {
        foreach(AIController ai in AIPlayers)
        {
            // checkout if ball is still in play
            if (!Services.Ball)
            {
                break;
            }

            ai.MoveUpdate();
        }
    }

    public void DestroyAI()
    {
        foreach(AIController ai in AIPlayers)
        {
            Object.Destroy(ai);
        }

        AIPlayers.Clear();
        Services.AIManager = null;
    }
}
