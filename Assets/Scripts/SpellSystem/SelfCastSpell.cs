using AfterlifeProject.Assets.Scripts.Core;
using Core;
using Projectiles;
using Resource;
using UnityEngine;

namespace AfterlifeProject.Assets.Scripts.SpellSystem.Spells
{
    [RequireComponent(typeof(CapsuleCollider), typeof(DestroyAfterTime))]
    [RequireComponent(typeof(TriggerOverlap), typeof(TriggerStay))]
    public class SelfCastSpell : Spell
    {
        private SelfCastSpellData _data;
        private TriggerOverlap _triggerOverlap;
        private TriggerStay _triggerStay;
        private CapsuleCollider _collider;
        private DestroyAfterTime _destroyAfterTime;
        private Transform _caster;
        
        private void Awake()
        {
            _triggerOverlap = GetComponent<TriggerOverlap>();
            _triggerStay = GetComponent<TriggerStay>();

            _triggerOverlap.OnTrigger += TriggerEnter;
            _triggerStay.OnStay += TriggerStay;
        }

        private void Start()
        {
            foreach(SpellBehaviour beh in _data.Modifiers)
            {
                beh.ProcessOnCastSpellBehaviour(_caster);
            }
        }

        public override void Initialize(SpellData data, Transform caster)
        {
            _data = (SelfCastSpellData)data;
            _caster = caster;
        }

        private void TriggerEnter(Collider collider)
        {
            IDamagable damagable = collider.gameObject.GetComponent<IDamagable>();

            if(damagable != null && (damagable != GameObject.FindObjectOfType<Player>().GetComponent<IDamagable>()))
            {
                //damagable.TakeDamage(_data.Damage);
 
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