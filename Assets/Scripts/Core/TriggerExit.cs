using System;
using UnityEngine;

namespace Core
{
    public class TriggerExit : MonoBehaviour
    {
        public event Action<Collider> OnExit;
        
        private void OnTriggerExit(Collider other)
        {
             OnExit?.Invoke(other);
        }
        
    }
}

