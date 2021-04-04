using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallEffect
{
    public void Activate(AIController effectTarget)
    {
        ApplyEffect(effectTarget);
    }

    protected virtual void ApplyEffect(AIController effectTarget) { }
}

// This isn't very non-Tech designer friendly
// Would benefit from being turned into Monobehavior and effects made as prefabs
public class FreezeEffect : BallEffect
{
    public float freezeDuration = 1.2f;

    protected override void ApplyEffect(AIController effectTarget)
    {
        effectTarget.Freeze(freezeDuration);
    }
}
