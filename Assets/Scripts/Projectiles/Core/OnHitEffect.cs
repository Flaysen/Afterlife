using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Projectiles;
using Effects;

[CreateAssetMenu(fileName = "on_hit_effect", menuName = "Projectile Modifiers/On Hit Effect", order = 53)]
public class OnHitEffect : SpellBehaviour
{
    [SerializeField] private Effect _effect;

    public override void ProcessOnHitBehaviour(Collider collider)
    {
        EffectReciver effectReciver = collider.gameObject.GetComponent<EffectReciver>();

        if(effectReciver)
        {
            effectReciver.AddEffect(_effect);
        }
    }
}
