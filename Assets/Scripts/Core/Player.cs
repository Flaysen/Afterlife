using Resource;
using UnityEngine;

namespace Core
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private GameObject _modelPrefab;

        private void Awake()
        {
            PlayerHealthBehaviour.OnPlayerDeath += HandlePlayerDeath;
        }

        private void HandlePlayerDeath()
        {
            // foreach(Transform modelPart in _modelPrefab.transform)
            // {
            //     modelPart.gameObject.SetActive(false);
            // }
            _modelPrefab.transform.localScale = Vector3.zero;
        }

        private void OnDisable()
        {
            PlayerHealthBehaviour.OnPlayerDeath -= HandlePlayerDeath;
        }
    }
}