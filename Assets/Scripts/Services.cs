using System.Collections;
using System.Collections.Generic;

public static class Services
{
    public static AILifeCycleManager AIManager;
    public static GameManager gameManager;
    public static PlayerController player;
    public static BallScript ball;

    public static void InitializeServices()
    {
        AIManager = new AILifeCycleManager();
    }   
}
