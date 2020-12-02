using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Projectiles;
using Effects;

[CreateAssetMenu(fileName = "cast_effect", menuName = "Projectile Modifiers/Cast Effect", order = 54)]
public class CastEffect : SpellBehaviour
{
    [SerializeField] private Effect _effect;

    public override void ProcessOnCastSpellBehaviour(Transform target)
    {
        EffectReciver effectReciver = target.gameObject.GetComponent<EffectReciver>();

        if(effectReciver)
        {
            effectReciver.AddEffect(_effect);
        }
    }
}
