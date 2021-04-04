using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallEffect : MonoBehaviour
{
    public ParticleSystem particleFX;
    public AudioClip impactSound;

    public void Activate()
    {
        particleFX.Play();
    }

    public virtual void ApplyEffect()
    {

    }
}
