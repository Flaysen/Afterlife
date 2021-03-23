using Core;
using UnityEngine;

namespace Effects 
{
    public abstract class Effect : ScriptableObject
    {
        [SerializeField] protected float _duration;
        [SerializeField] private int _periodsCount;
        [SerializeField] private GameObject _particles;

        public float Duration => _duration;
        public float PeriodsCount => _periodsCount;
        public GameObject Particles => _particles;
        
        public float PeriodTime => (int)(_duration / _periodsCount); 
        public virtual void Tick(Transform target) {}
        public virtual void BeginEffect(Transform target) {}
        public virtual void EndEffect(Transform target) {}

        protected void PlayParticles(Transform target)
        {
            GameObject particles = Instantiate(_particles, target.position, Quaternion.identity);
            particles.transform.parent = target;
            particles.GetComponent<DestroyAfterTime>().StartTimer(_duration);
        }
    }
}
