using Core;

namespace Projectiles
{
    public class ProjectilePool : ObjectPool<Projectile>
    {
        private void Start()
        {
            FillPool();
        }
    }
}


