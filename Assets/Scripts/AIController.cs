using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AIController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 10.0f;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public void MoveUpdate(Transform ballTransform)
    {
        if(!ballTransform)
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
}
