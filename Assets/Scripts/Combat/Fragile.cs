using Resource;
using UnityEngine;

namespace Combat
{
    public class Fragile : MonoBehaviour, IDamagable
    {
        public void TakeDamage(float damage)
        {
            Destroy(gameObject);
        }
    }
}
