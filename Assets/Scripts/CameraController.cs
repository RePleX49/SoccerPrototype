using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Transform viewCamera;

    public float targetSmoothTime;
    Vector3 targetSmoothVelocity;

    public float currentCamDist;
    // The distance from the target we are currently lerping towards
    private float targetCamDist;

    bool bFreeCam = true;

    public Vector3 cameraOffset;
    Vector3 startingEuler;

    // Start is called before the first frame update
    void Start()
    {
        startingEuler = transform.eulerAngles;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            bFreeCam = !bFreeCam;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // SmoothDamp for camera lag
        if (target)
        {
            Vector3 newPosition = target.position + cameraOffset;
            transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref targetSmoothVelocity, targetSmoothTime);
        }
    }
}
