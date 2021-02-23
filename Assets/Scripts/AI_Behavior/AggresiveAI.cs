using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AggresiveAI : AIController
{
    public float avoidDistance = 5.0f;
    BehaviorTree.Tree<AggresiveAI> behaviorTree;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        var BallInPlay = new Tree<AggresiveAI>
        (
            new Sequence<AggresiveAI>
            (
                new IsBallActive(),
                new CheckDistanceToBall(),
                new MoveToBall()
            )
        );

        behaviorTree = new Tree<AggresiveAI>
        (
            new Selector<AggresiveAI>
            (
                BallInPlay
            )
        );
    }

    public override void MoveUpdate()
    {
        BehaviorUpdate();
    }

    // Update is called once per frame
    void BehaviorUpdate()
    {
        behaviorTree.Update(this);
    }
}

public class IsBallActive : BehaviorTree.Node<AggresiveAI>
{
    public override bool Update(AggresiveAI context)
    {
        return Services.Ball != null;
    }
}

public class MoveToBall : BehaviorTree.Node<AggresiveAI>
{
    public override bool Update(AggresiveAI context)
    {
        context.SimpleMoveToBall();
        return true; // maybe return false on fail to move to?
    }
}

public class CheckDistanceToBall : BehaviorTree.Node<AggresiveAI>
{
    public override bool Update(AggresiveAI context)
    {
        float distance = Vector3.Distance(context.transform.position, Services.Ball.transform.position);
        if (distance < context.avoidDistance)
            return false;
        else
            return true;
    }
}
