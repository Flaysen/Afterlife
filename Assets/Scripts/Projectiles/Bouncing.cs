using UnityEngine;
using Resource;

namespace Projectiles
{
[CreateAssetMenu(fileName = "bouncing", menuName = "Projectile Modifiers/Bouncing", order = 53)]
    public class Bouncing : SpellBehaviour
    {
        public override void ProcessInitProjectileBehaviour(IProjectile projectile)
        {
            projectile.Rigidbody.constraints = RigidbodyConstraints.FreezeRotationY;
        }

        public override void ProcessFlightBehaviour(IProjectile projectile, float speed)
        {
            Bounce(projectile, speed);
        }

        private void Bounce(IProjectile projectile, float speed)
        {
            Ray ray = new Ray(projectile.Transform.position, projectile.Transform.forward);
            RaycastHit hit;

            bool isHit = Physics.Raycast(ray, out hit, Time.deltaTime * speed + .3f,  ~(projectile as MonoBehaviour).gameObject.layer);
            
            if(isHit && hit.collider.gameObject.GetComponent<IDamagable>() == null)
            {
                Vector3 direction = Vector3.Reflect(projectile.Rigidbody.velocity, hit.normal);    

                float rotation = 90 - Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;

                projectile.Transform.eulerAngles = new Vector3(0,rotation,0);  

                projectile.Rigidbody.velocity = projectile.Transform.forward * speed;   
            }
        }
    }
}

