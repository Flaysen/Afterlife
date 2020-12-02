using System;
using UnityEngine;

namespace Core
{
    public abstract class BaseState 
    {
        protected GameObject _gameObject;
        protected Transform _transform;

        public BaseState(GameObject gameObject)
        {
            _gameObject = gameObject;
            _transform = gameObject.transform;
        
        }
        public abstract Type StateUpdate();
    }
}


