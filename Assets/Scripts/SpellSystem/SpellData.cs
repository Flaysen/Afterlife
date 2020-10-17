using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using System;

public enum CastType
{
    Projectile,
    PointTarget,
    SelfCast,
    GlobalCast
}

public abstract class SpellData : ScriptableObject
{
    [SerializeField] private string _name;

    [SerializeField] private Spell _spellPrefab;
 
    [SerializeField] private GameObject _hitParticlesPrefab;
    
    [SerializeField] private float _cooldown;

    [SerializeField] private CastType _castType;

    public string Name => _name;
  
    public float Cooldown => _cooldown;

    public CastType CastType => _castType;

    public Spell SpellPrefab => _spellPrefab;

    public GameObject HitParticlesPrefab => _hitParticlesPrefab;

    public float DamageToCast {get; set;}

    public virtual void Cast(Transform transform, List<Transform> targets) {}
}
