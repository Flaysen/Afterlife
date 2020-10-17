using System;
using UnityEngine;

namespace Core
{
    public class TriggerStay : MonoBehaviour
    {
        public event Action<Collider> OnStay;

        private void OnTriggerStay(Collider other)
        {
            OnStay?.Invoke(other);    
        }
    }
}