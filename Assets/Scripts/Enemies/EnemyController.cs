using UnityEngine;
using Combat;
using System;
using Core;

namespace Enemies
{
    public class EnemyController : MonoBehaviour, IAttackInvoker, IController
    {
        public bool IsControlDisabled { get ; set; }

        public event Action OnAttack;

        private Rigidbody _rigidbody;

        private Transform _player;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();

            _player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Start()
        {
            InvokeRepeating("Attack", 0f, 1f);
        }

        private void Update()
        {
            if(!IsControlDisabled)
            {
                transform.LookAt(_player.transform.position);
                transform.Translate(Vector3.forward * Time.deltaTime);
            }          
        }

        private void Attack()
        {                   
            if(!IsControlDisabled)OnAttack?.Invoke();
        }
    }
}

