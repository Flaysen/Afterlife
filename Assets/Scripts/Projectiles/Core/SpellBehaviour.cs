using System.Collections.Generic;
using UnityEngine;

namespace Projectiles
{
    public abstract class SpellBehaviour : ScriptableObject          
    {
        public virtual void ProcessInitProjectileBehaviour(IProjectile projectile) {}
        public virtual void ProcessOnCastSpellBehaviour(Transform target) {}
        public virtual void ProcessFlightBehaviour(IProjectile projectile, float speed) {}
        public virtual void ProcessOnHitBehaviour(Collider collider) {}
        public virtual void ProccessOnStayBehaviour(Collider collider) {}
        public virtual void ProcessDestroySpellBehaviour(Spell projectile) {}
        public virtual void ProcessDestroyProjectileBehaviour(IProjectile projectile) {}
    }
} 