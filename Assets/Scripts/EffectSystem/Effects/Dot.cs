using UnityEngine;
using Resource;

namespace Effects
{
    [CreateAssetMenu(fileName = "dot", menuName = "Effects/Dot", order = 55)]
    public class Dot : Effect
    {
        [SerializeField] private float _damage;

        public override void Tick(Transform target)
        {
            IDamagable damagable = target.GetComponent<IDamagable>();

            if(damagable != null)
            {
                damagable.TakeDamage(_damage);

                PlayParticles(target);
            }       
        }
    }
}


