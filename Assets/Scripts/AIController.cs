using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void MoveUpdate(Transform ballTransform)
    {
        if(!ballTransform)
        {
            return;
        }

        transform.LookAt(ballTransform);

        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
