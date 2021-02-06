using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    Transform ballTransform;

    [SerializeField]
    float moveSpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!ballTransform)
        {
            return;
        }

        transform.LookAt(ballTransform);

        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
