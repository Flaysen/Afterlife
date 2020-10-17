using System;
using UnityEngine;

namespace Core
{
    public class TriggerOverlap : MonoBehaviour
    {
        public event Action<Collider> OnTrigger;
        
        private void OnTriggerEnter(Collider other)
        {
             OnTrigger?.Invoke(other);
        }
        
    }
}

