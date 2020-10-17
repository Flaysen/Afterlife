using System.Collections;
using UnityEngine;

namespace HUD
{
    public class HealthFadeEffect : MonoBehaviour
    {
        [SerializeField] private HealthBar _healthBar;

        [SerializeField] private float _fadeTick= .1f;

        [SerializeField] private float _fadeSpeed = .01f;

        [SerializeField] private float _fadeDelay = 0f;

        private void Awake()
        {
            _healthBar.OnSizeAdjusted += (scale) => { StopAllCoroutines(); StartCoroutine(Fade(scale)); };

        }
        
        private IEnumerator Fade(Vector3 healthBarScale)
        {
            float scaleDiff = transform.localScale.x - healthBarScale.x;

            if (scaleDiff > 0)
            {
                yield return new WaitForSeconds(_fadeDelay);

                for (float i = scaleDiff; i > 0; i -= _fadeSpeed)
                {
                    float size = healthBarScale.x + i;

                    transform.localScale = new Vector3(size, transform.localScale.y, transform.localScale.z);

                    yield return new WaitForSeconds(_fadeTick);
                }
            }
            else
            {
                StopAllCoroutines();

                transform.localScale = healthBarScale;
            }
        }
    }

}

