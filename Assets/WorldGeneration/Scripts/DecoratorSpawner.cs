using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelGeneration
{
    public class DecoratorSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject [] _decoratorMeshes;
        [SerializeField] private Material []  _materials;
        [SerializeField] private float _scaleDeviation;
        [SerializeField] private float _rotationDeviation; 
        [SerializeField] private bool _fixedRoation;
        [SerializeField] private bool _fixedPosition;
    
        private void Start()
        {
            SpawnObject();    
        } 
        private Vector3 SetPrefabPosition()
        {
            return new Vector3(
                transform.position.x,
                (_fixedPosition) ?
                    transform.position.y :
                    transform.position.y,    //+ (transform.localScale.y - 0.1f) / 2
                transform.position.z);
        }

        private Vector3 SetPrefabScale()
        {
            return new Vector3(
                Random.Range(1 - _scaleDeviation, 1 + _scaleDeviation),
                Random.Range(1 - _scaleDeviation, 1 + _scaleDeviation),
                Random.Range(1 - _scaleDeviation, 1 + _scaleDeviation));
        }

        private Quaternion SetPrefabRotation()
        {
            return (_fixedRoation) ? 

            Quaternion.Euler(
                Quaternion.identity.x,
                _rotationDeviation,
                Quaternion.identity.z) :

            Quaternion.Euler(
                Random.Range(-_rotationDeviation, _rotationDeviation),
                Random.Range(0, 360),
                Random.Range(-_rotationDeviation, _rotationDeviation));
        }

        private Material SetMaterial()
        {
            return _materials[Random.Range(0, _materials.Length)];
        }

        private void SpawnObject()
        {
            GameObject @object = Instantiate(
                _decoratorMeshes[Random.Range(0, _decoratorMeshes.Length)],
                SetPrefabPosition(),
                SetPrefabRotation());

            //@object.transform.localScale = SetPrefabScale();

            if(_materials != null && _materials.Length > 0)
            {
                @object.GetComponentInChildren<Renderer>().material = SetMaterial();
            }
        }
    } 
}

