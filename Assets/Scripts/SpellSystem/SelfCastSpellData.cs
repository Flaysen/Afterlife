using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Projectiles;

[CreateAssetMenu(fileName = "self_cast_spell_data", menuName = "Spells/Self Cast Spell Data", order = 52)]
public class SelfCastSpellData : SpellData
{
    [SerializeField] private float _duration;

    [SerializeField] List<SpellBehaviour> _modifiers = new List<SpellBehaviour>(); 

    public float Duration => _duration;

    public List<SpellBehaviour> Modifiers => _modifiers;

    public override void Cast(Transform transform, List<Transform> targets)
    {
        Spell spell = Instantiate(SpellPrefab, targets.FirstOrDefault().position, Quaternion.identity);

        spell.transform.parent = transform;

        spell.Initialize(this, transform);

        //Destroy(spell.gameObject, _duration);
    }
}
