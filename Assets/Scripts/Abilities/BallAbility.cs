using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAbility : MonoBehaviour
{
    // use an enum as option menu for selecting Ability Type/Effect
    public enum AbilityType
    {
        Freeze
    }

    public AbilityType abilityType;
    public ParticleSystem ballAbilityFX;
    BallEffect ballEffect;

    public void Initialize()
    {
        // create actual BallEffect
        switch(abilityType)
        {
            case AbilityType.Freeze:
                ballEffect = new FreezeEffect();
                break;
        }
    }

    public virtual void PlayFX() 
    {
        if (!ballAbilityFX)
            return;

        ballAbilityFX.Play();
    }

    public void Activate(AIController target)
    {
        PlayFX();
        ballEffect.Activate(target);
    }

    public BallEffect GetBallEffect()
    {
        return ballEffect;
    }
}
