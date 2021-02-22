using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RefereeFSM : AIController
{
    public float distanceLimitToBall = 5.0f;
    AudioSource whistleSource;
    RefereeState currentState;

    enum RefereeState
    {
        Observing,
        Foul
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    protected override void Initialize()
    {
        base.Initialize();
        whistleSource = GetComponent<AudioSource>();
        Services.eventManager.RegisterListener<Fouled>(TransitionFoul);
    }

    // Update is called once per frame
    public override void MoveUpdate()
    {
        switch(currentState)
        {
            case RefereeState.Observing:
                if (IsCloseToBall())
                {
                    break;
                }
            
                SimpleMoveToBall();
                break;
            case RefereeState.Foul:
                BlowWhistle();
                currentState = RefereeState.Observing;
                break;
        }
    }

    public bool IsCloseToBall()
    {
        return Vector3.Distance(transform.position, Services.Ball.transform.position) < distanceLimitToBall;
    }

    void BlowWhistle()
    {
        if(!whistleSource)
        {
            Debug.Log("Couldn't find whistle AudioSource");
            return;
        }

        whistleSource.Play();
    }

    void TransitionFoul(SGEvent e)
    {
        currentState = RefereeState.Foul;
    }

    private void OnDestroy()
    {
        Services.eventManager.UnRegisterListener<Fouled>(TransitionFoul);
    }
}
