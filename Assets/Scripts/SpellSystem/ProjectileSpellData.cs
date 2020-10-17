using System.Collections;
using System.Collections.Generic;
using Projectiles;
using UnityEngine;


[CreateAssetMenu(fileName = "projectile_spell_data", menuName = "Spells/Projecitle Spell Data", order = 51)]
public class ProjectileSpellData : SpellData

{
    [SerializeField] private float _damage;

    [SerializeField] private float _speed;

    [SerializeField] private float _range;

    [SerializeField] private float _recoil;

    [SerializeField] private int _projectileCount;

    [SerializeField] List<SpellBehaviour> _modifiers = new List<SpellBehaviour>();

    public float Damage => _damage;

    public float Speed => _speed;

    public float Range => _range;

    public float Recoil => _recoil;

    public List<SpellBehaviour> Modifiers  => _modifiers;

    public override void Cast(Transform transform, List<Transform> targets)     
    {
        for (int i = 0; i < _projectileCount; i++)
        {
            Spell spellInstance = Instantiate(SpellPrefab, targets[i].position, targets[i].transform.rotation);

            spellInstance.Initialize(this, transform); 
        }        
    }  
}
