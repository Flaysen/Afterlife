using System.Collections;
using System.Collections.Generic;
using Resource;
using UnityEngine;

public class Destroyable : MonoBehaviour, IDamagable
{
     [SerializeField] private GameObject destroyEffect;
     [SerializeField] private AudioClip destroySound;

    public void TakeDamage(float damage)
    {
        Instantiate(destroyEffect, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
