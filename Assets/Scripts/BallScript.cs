using System.Collections;
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

    BallAbility activeAbility;
  
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

            // Set the reference of whichever player's ability is active on the ball
            if (activeAbility == null)
            {
                activeAbility = targetController.GetBallAbility();
                Debug.Log(activeAbility.gameObject);
                Debug.Log("Set effect " + activeAbility);
            }
            else
            {
                activeAbility.Activate(targetController);
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
