using UnityEngine;
using System.Collections.Generic;

namespace Core
{
    public abstract class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] protected T _objectPrefab = null;

        [SerializeField] protected int _initialPoolCount = 10;

        [SerializeField] protected bool _isExtendable = false;

        public List<T> PoolObjects { get; private set; }

        public T Get()
        {
            for (int i = 0; i < PoolObjects.Count; i++)
            {
                if (!PoolObjects[i].gameObject.activeInHierarchy)
                {
                    return PoolObjects[i];
                }
            }

            return (_isExtendable) ? AddObjectToPool() : null;
        }

        protected void FillPool()
        {
            PoolObjects = new List<T>();

            for (int i = 0; i < _initialPoolCount; i++)
            {
                AddObjectToPool();
            }
        }

        private T AddObjectToPool()
        {
            var poolObject = Instantiate(_objectPrefab, transform) as T;

            poolObject.gameObject.SetActive(false);

            PoolObjects.Add(poolObject);

            return poolObject;
        }
    }
}

