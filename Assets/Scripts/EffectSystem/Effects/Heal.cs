using Resource;
using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(fileName = "heal", menuName = "Effects/Heal", order = 56)]
    public class Heal : Effect
    {
        [SerializeField] private float _value;

        public override void BeginEffect(Transform target)
        {
            IHealable healable = target.GetComponent<IHealable>();
            
            if(healable != null)
            {
                healable.Heal(_value);
            }

            PlayParticles(target); 
        }
    }
}