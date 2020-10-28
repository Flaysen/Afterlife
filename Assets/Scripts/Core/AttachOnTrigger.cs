using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class AttachOnTrigger : MonoBehaviour {

    private TriggerOverlap _triggerEnter;
    private TriggerExit _triggerExit;

    private void Awake()
    {
        _triggerEnter = GetComponent<TriggerOverlap>();
        _triggerExit = GetComponent<TriggerExit>();

        _triggerEnter.OnTrigger += AttachPlayer;
        _triggerExit.OnExit += DetachPlayer;
    }

    private void AttachPlayer(Collider collider)
    {
        Player player = collider.GetComponent<Player>();
        if(player)
        {
            player.transform.parent = transform;
        }
    }

    private void DetachPlayer(Collider collider)
    {
        Player player = collider.GetComponent<Player>();
        if(player)
        {
            player.transform.parent = null;
        }
    }  
}

