using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AggresiveAI : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    Rigidbody rb;
    BehaviorTree.Tree<AggresiveAI> behaviorTree;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        var BallInPlay = new Tree<AggresiveAI>
        (
            new Sequence<AggresiveAI>
            (
                new IsBallActive(),
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

    // Update is called once per frame
    void BehaviorUpdate()
    {
        behaviorTree.Update(this);
    }

    public void MoveToBall()
    {
        //find the ball direction and set forward
        Vector3 ballDirection = Services.Ball.transform.position - transform.position;
        ballDirection.y = 0.0f;
        transform.forward = ballDirection;

        Vector3 newPosition = rb.position + (transform.forward * moveSpeed * Time.deltaTime);
        rb.MovePosition(newPosition);
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
        context.MoveToBall();
        return true; // maybe return false on fail to move to?
    }
}
