using UnityEngine;

     public abstract class TargetedSpellBehaviour : ScriptableObject          
     {
        public virtual void ProcessInitBehaviour(Spell spell) {}
        public virtual void ProcessOnHitBehaviour(Collider collider) {}
        public virtual void ProccessOnStayBehaviour(Collider collider) {}
        public virtual void ProcessDestroyBehaviour(Spell spell) {}
    }
