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
        AIPlayers.Add(Object.Instantiate(refereePrefab, Services.gameManager.SpawnLocations[0].position, Quaternion.identity));
    }

    public void CreateAI(int numToSpawn, AIController AIPrefab)
    {
        for (int i = 0; i < numToSpawn; i++)
        {
            AIPlayers.Add(Object.Instantiate(AIPrefab, Services.gameManager.SpawnLocations[i].position, Quaternion.identity));
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

    public AIController GetClosestToBall()
    {
        AIController closestPlayer = AIPlayers[0];

        float closestDistance = int.MaxValue;
        Transform ballTransform = Services.Ball.transform;

        foreach(AIController ai in AIPlayers)
        {
            float distance = ai.transform.position.x * ballTransform.position.x
                + ai.transform.position.y * ballTransform.position.y
                + ai.transform.position.z * ballTransform.position.z;
            if(distance < closestDistance)
            {
                closestDistance = distance;
                closestPlayer = ai;
            }
        }

        return closestPlayer; 
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
