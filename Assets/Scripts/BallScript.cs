﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallScript : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;

    Vector3 initialPosition;
    [SerializeField]
    float launchForce = 8.0f;

    BallEffect currentEffect;
  
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        initialPosition = transform.position;
        Services.Ball = this;
        Services.eventManager.RegisterListener<GoalScored>(ResetPosition);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Entity"))
        {
            AIController targetController = other.gameObject.GetComponent<AIController>();

            if (currentEffect == null)
            {
                currentEffect = targetController.ballAbility;
                Debug.Log("Set effect " + currentEffect);
            }
            else
            {
                currentEffect.Activate(targetController);
            }

            Transform otherTransform = other.gameObject.transform;

            Vector3 launchDir = transform.position - otherTransform.position;
            launchDir.Normalize();

            rb.AddForce(launchDir * launchForce, ForceMode.Impulse);

            Debug.Log("Kicked");
        }
        else if(other.gameObject.CompareTag("Red Goal"))
        {
            // TODO use a dot product check that we are entering the goal
            Services.eventManager.FireEvent(new GoalScored(true));
        }
        else if(other.gameObject.CompareTag("Blue Goal"))
        {
            Services.eventManager.FireEvent(new GoalScored(false));
        }
    }

    void ResetPosition(SGEvent e)
    {
        transform.position = initialPosition;
    }

    private void OnDestroy()
    {
        Services.eventManager.UnRegisterListener<GoalScored>(ResetPosition);
    }
}
