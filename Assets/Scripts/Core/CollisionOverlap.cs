using System;
using UnityEngine;

namespace Core
{
    public class CollisionOverlap : MonoBehaviour
    {
        public event Action<Collision> OnCollision;
        
        private void OnCollisionEnter(Collision collision)
        {
            OnCollision?.Invoke(collision);
        }
    }

}
