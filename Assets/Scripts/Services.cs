using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public static class Services
{
    public static AILifeCycleManager AIManager;
    public static GameManager gameManager;
    public static GameState gameStateController;
    public static EventManager eventManager;

    // possibility to add array of players to access
    public static PlayerController player;

    //private and public field classic C# practice
    private static BallScript _ball;

    public static BallScript Ball
    {
        get
        {
            Debug.Assert(_ball != null);
            return _ball;
        }

        set => _ball = value;
    }

    public static void InitializeServices()
    {
        AIManager = new AILifeCycleManager();
        eventManager = new EventManager();
        gameStateController = new GameState();
    }   
}
