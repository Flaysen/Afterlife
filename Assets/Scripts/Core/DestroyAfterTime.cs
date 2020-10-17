using UnityEngine;

namespace AfterlifeProject.Assets.Scripts.Core
{
    public class DestroyAfterTime : MonoBehaviour
    {     
        public void StartTimer(float time)
        {
            Destroy(this.gameObject, time);
        }
    }
}