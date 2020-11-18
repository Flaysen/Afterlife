﻿using System.Collections;
using System.Collections.Generic;
using Core;
using Resource;
using UnityEngine;

[RequireComponent(typeof(TriggerOverlap))]
public class OnConctactDamage : MonoBehaviour
{
    private TriggerOverlap _triggerOverlap;

    private void Awake()
    {
        _triggerOverlap = GetComponent<TriggerOverlap>();
        _triggerOverlap.OnTrigger += DamagePlayer;    
    }

    private void DamagePlayer(Collider collider)
    {
        PlayerHealthBehaviour player = collider.GetComponent<PlayerHealthBehaviour>();

        if(player)
        {
            player.TakeDamage(1);
        }
    }
}
