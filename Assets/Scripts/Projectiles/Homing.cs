using UnityEngine;
using Resource;

namespace Projectiles
{
    [CreateAssetMenu(fileName = "homing", menuName = "Projectile Modifiers/Homing", order = 53)]
    public class Homing : SpellBehaviour
    {
        [SerializeField] private float _awareRange ;  
        [SerializeField] private float _turningVelocity;

        public override void ProcessFlightBehaviour(IProjectile projectile, float speed)
        {
            if(!projectile.Target)
            {
                LookForTarget(projectile);
            }
            else
            {
                AimAtTheTarget(projectile, speed);
            }   
        }
        public override void ProcessInitProjectileBehaviour(IProjectile projectile)
        {
            projectile.Rigidbody.angularVelocity = Vector3.zero;
        }
         private void LookForTarget(IProjectile projectile)
        {
            Collider [] colliders = Physics.OverlapSphere(projectile.Transform.position, _awareRange);

            foreach(Collider collider in colliders)
            {
                IDamagable damagable = collider.GetComponent<IDamagable>();
                if(damagable != null &&!(damagable.GetType().IsAssignableFrom(projectile.TargetToAvoid.GetType())))          
                {   
                    projectile.Target = collider.transform;                                           
                    break;
                }
            }                 
        }
        private void AimAtTheTarget(IProjectile projectile, float speed)
        {
            Vector3 direction = (projectile.Target.position -projectile.Transform.position).normalized;
            Vector3 rotateAmount = Vector3.Cross(projectile.Transform.forward, direction);
            projectile.Rigidbody.angularVelocity = rotateAmount * _turningVelocity;
            projectile.Rigidbody.velocity = projectile.Transform.forward * speed;
        }
    }
}