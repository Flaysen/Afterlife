using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    public class EffectReciver : MonoBehaviour
    {
        [SerializeField] private List<Effect> _effects = new List<Effect>();

        public void AddEffect(Effect effect)
        {
            _effects.Add(effect);
            StartCoroutine(ProceedEffect(effect));       
        }
        public void RemoveEffect(Effect effect)
        {
            StopCoroutine(ProceedEffect(effect));
            _effects.Remove(effect);
        }
        private IEnumerator ProceedEffect(Effect effect)
        {
            effect.BeginEffect(transform);

            for (int i = 0; i <= effect.PeriodsCount; i++)
            {
                effect.Tick(transform);
                yield return new WaitForSeconds(effect.PeriodTime);
                i++;
            }
            effect.EndEffect(transform);
            RemoveEffect(effect);
        }
    }
}


