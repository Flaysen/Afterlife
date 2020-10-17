using UnityEngine;
using Core;
using System;
using Resource;
using Projectiles;

[RequireComponent(typeof(Rigidbody), typeof(TriggerOverlap))]
public class ProjectileSpell : Spell, IProjectile
{
    private Rigidbody _rb;

    private ProjectileSpellData _data;

    private TriggerOverlap _triggerOverlap;

    private IDamagable _targetToAvoid;

    public Rigidbody Rigidbody => _rb;

    public IDamagable TargetToAvoid => _targetToAvoid;

    public Transform Target { get; set; }

    public Transform Transform => transform;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        _triggerOverlap = GetComponent<TriggerOverlap>();

        _triggerOverlap.OnTrigger += TriggerEnter;     
    }

    private void Start()
    {         
        foreach(SpellBehaviour mod in _data.Modifiers)
        {
            mod.ProcessInitProjectileBehaviour(this);
        }

       AddForce();
    }
 
    void Update()
    {      
        foreach(SpellBehaviour mod in _data.Modifiers)
        {
            mod.ProcessFlightBehaviour(this, _data.Speed);
        }
    }

    private void TriggerEnter(Collider collider)
    {
        IDamagable damagable = collider.gameObject.GetComponent<IDamagable>();

        if(damagable != null && (damagable != GameObject.FindGameObjectWithTag("Player").GetComponent<IDamagable>()))
        {
            damagable.TakeDamage(_data.Damage);

            foreach(SpellBehaviour mod in _data.Modifiers)
            {
                mod.ProcessOnHitBehaviour(collider);
            }        
        }
        DisplayEffect(_data.HitParticlesPrefab);
    }

    private void AddForce()
    {
        transform.rotation *= Quaternion.Euler(0f,
            UnityEngine.Random.Range(-_data.Recoil, _data.Recoil), 0f);

         _rb.velocity = transform.forward * _data.Speed;   
    }

    public override void Initialize(SpellData data, Transform transform)
    {
        _data = (ProjectileSpellData)data;
        _targetToAvoid = transform.GetComponent<IDamagable>();
    }

    private void OnDisable() 
    {
        foreach(SpellBehaviour mod in _data.Modifiers)
        {
            mod.ProcessDestroyProjectileBehaviour(this);
        }   
    }
}
