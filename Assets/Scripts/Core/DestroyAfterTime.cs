using UnityEngine;

namespace Core
{
    public class DestroyAfterTime : MonoBehaviour
    {    
        [SerializeField] private float _time = 0;

        private void Start()
        {
            if(_time > 0)
            {
                Destroy(this.gameObject, _time);
            }   
        }
        public void StartTimer(float time)
        {
            Destroy(this.gameObject, time);
        }
    }
}