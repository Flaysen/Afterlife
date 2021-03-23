using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Core;
using Stats;
using Resource;

namespace Projectiles
{
    [RequireComponent(typeof(Rigidbody), typeof(TriggerOverlap))]
    public class Projectile : MonoBehaviour, IProjectile
    {
        [SerializeField] private List<SpellBehaviour> _modifiers = new List<SpellBehaviour>();

        [SerializeField] private GameObject _hitParticlesPrefab;

        private float _damage, _speed, _range, _size, _recoil;

        private float _timeToRemove;

        private Vector3 _initialScale;

        private IDamagable _targetToAvoid;

        private Transform _transform;

        private Rigidbody _rigidbody;

        private TrailRenderer _trail;

        private TriggerOverlap _triggerOverlap;
        private CollisionOverlap _collisionOverlap;

        public float Speed => _speed;

        public IDamagable TargetToAvoid => _targetToAvoid;

        public Rigidbody Rigidbody => _rigidbody;

        public Transform Target {get; set;}

        public Transform Transform => transform;

        private void Awake()
        {
            _transform = GetComponent<Transform>();

            _initialScale = transform.localScale;

            _rigidbody = GetComponent<Rigidbody>();

            _trail = GetComponent<TrailRenderer>();

            _triggerOverlap = GetComponent<TriggerOverlap>();

            _collisionOverlap = GetComponent<CollisionOverlap>();

            _triggerOverlap.OnTrigger += ProjectileHitHandle;

            _collisionOverlap.OnCollision += ProjectileCollisionHandle;

            foreach(SpellBehaviour modifier in _modifiers)
            {
                modifier.ProcessInitProjectileBehaviour(this);
            }
        }

        private void OnEnable()
        {
            Target = null;

            _transform.localScale = _initialScale * _size;

            _rigidbody.velocity = Vector3.zero;

            _rigidbody.velocity = _transform.forward * _speed;

            _rigidbody.constraints = RigidbodyConstraints.FreezePositionY;

            StartCoroutine(RemoveAfterTime(_timeToRemove));    
        }

        private void Update()
        {
            foreach(SpellBehaviour modifier in _modifiers)
            {
                modifier.ProcessFlightBehaviour(this, _speed);
            }
        }

        public void Initialize(List<SpellBehaviour> modifiers, StatsBehaviour stats,
          Transform transform, IDamagable avoidTarget)
        {
            _transform.position = transform.position;

            _transform.rotation = transform.rotation;

            _transform.rotation *= Quaternion.Euler(0f,
             Random.Range( -_recoil, _recoil), 0f);

            _damage = stats.GetStatValue(StatType.ProjectileDamage);

            _speed = stats.GetStatValue(StatType.ProjectileSpeed);

            _range = stats.GetStatValue(StatType.AttackRange);

            _size = stats.GetStatValue(StatType.ProjectileSize);

            _recoil = stats.GetStatValue(StatType.AttackRecoil);

            _targetToAvoid = avoidTarget;

            _timeToRemove = _range / _speed;

            _modifiers = modifiers;
        }

        private IEnumerator RemoveAfterTime(float time)
        {
            yield return new WaitForSeconds(time);

            RemoveProjectile();

            StopAllCoroutines();
        }

        private void ProjectileHitHandle(Collider collider)
        {
            IDamagable damagable = collider.gameObject.GetComponent<IDamagable>();

            if((damagable != null) && !(damagable.GetType().IsAssignableFrom(_targetToAvoid.GetType())))             
            {
                foreach(SpellBehaviour modifier in _modifiers)
                {
                    modifier.ProcessOnHitBehaviour(collider);
                }
                damagable.TakeDamage(_damage);
    
                SpawnParticles();
    
                RemoveProjectile();        
            }
        }

        private void ProjectileCollisionHandle(Collision collision)
        {
            IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();
            if(damagable == null  && collision.gameObject.layer != gameObject.layer)
            {
                SpawnParticles();
                RemoveProjectile();
            }
        }

        private void SpawnParticles()
        {
            if(_hitParticlesPrefab)Instantiate(_hitParticlesPrefab, transform.position, Quaternion.identity);

            _hitParticlesPrefab.GetComponent<DestroyAfterTime>().StartTimer(0.05f);
        }

        private void OnDisable()
        {
            foreach(SpellBehaviour modifier in _modifiers)
            {
                modifier.ProcessDestroyProjectileBehaviour(this);
            }
            _transform.localScale = _initialScale;
        }

        private void RemoveProjectile()
        {   
            _trail.Clear();
            gameObject.SetActive(false); 
        }
    }
}
