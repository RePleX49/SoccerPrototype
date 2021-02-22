using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AIController : MonoBehaviour
{
    [SerializeField]
    protected float moveSpeed = 10.0f;

    protected Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public virtual void MoveUpdate()
    {
        SimpleMoveToBall();
    }

    protected virtual void SimpleMoveToBall()
    {
        Transform ballTransform = Services.Ball.transform;

        if (!ballTransform)
        {
            return;
        }

        //find the ball direction and set forward
        Vector3 ballDirection = ballTransform.position - transform.position;
        ballDirection.y = 0.0f;
        transform.forward = ballDirection;

        Vector3 newPosition = rb.position + (transform.forward * moveSpeed * Time.deltaTime);
        rb.MovePosition(newPosition);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Entity"))
        {
            Services.eventManager.FireEvent(new Fouled());
        }
    }
}
