using AfterlifeProject.Assets.Scripts.SpellSystem.SO;
using Core;
using Projectiles;
using Resource;
using UnityEngine;


namespace AfterlifeProject.Assets.Scripts.SpellSystem.Spells
{
    [RequireComponent(typeof(CapsuleCollider), typeof(DestroyAfterTime))]
    [RequireComponent(typeof(TriggerOverlap), typeof(TriggerStay))]
    public class PointTargetSpell : Spell
    {
        private PointTargetSpellData _data;
        private TriggerOverlap _triggerOverlap;
        private TriggerStay _triggerStay;
        private CapsuleCollider _collider;
        private DestroyAfterTime _destroyAfterTime;
        
        private void Awake()
        {
            _triggerOverlap = GetComponent<TriggerOverlap>();
            _triggerStay = GetComponent<TriggerStay>();

            _triggerOverlap.OnTrigger += TriggerEnter;
            _triggerStay.OnStay += TriggerStay;

            _destroyAfterTime = GetComponent<DestroyAfterTime>();
        }

        private void Start()
        {
            _collider = GetComponent<CapsuleCollider>();
            _collider.radius = _data.Radius;
            _collider.height = 0.0f;
            _collider.isTrigger = true;

            _destroyAfterTime.StartTimer(_data.Lifetime);

            foreach(SpellBehaviour beh in _data.Modifiers)
            {
                beh.ProcessOnCastSpellBehaviour(Caster);
            }
        }

        public override void Initialize(SpellData data, Transform caster)
        {
            _data = (PointTargetSpellData)data;
        }

        private void TriggerEnter(Collider collider)
        {
            IDamagable damagable = collider.gameObject.GetComponent<IDamagable>();

            if(damagable != null && (damagable != GameObject.FindObjectOfType<Player>().GetComponent<IDamagable>()))
            {
                damagable.TakeDamage(_data.Damage);

                foreach(SpellBehaviour beh in _data.Modifiers)
                {
                    beh.ProcessOnHitBehaviour(collider);
                }      
            }
        }

        private void TriggerStay(Collider collider)
        {
            foreach(SpellBehaviour beh in _data.Modifiers)
            {
                beh.ProccessOnStayBehaviour(collider);
            }
        }
  
        // private void OnDisable()
        // {
        //     foreach(TargetedSpellBehaviour beh in _data.Modifiers)
        //     {
        //          beh.ProcessDestroyBehaviour(this);
        //     }
        // }
    }
}