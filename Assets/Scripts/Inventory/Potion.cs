using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using InventorySystem;
using Resource;
using UnityEngine;

public class Potion : Item
{
    private Player _player;

    public override void GetItemBehaviour()
    {
        RestoreHealth();  
    }

    private  void Awake()
    {   
        _player = GameObject.FindObjectOfType<Player>();
    }

    private void RestoreHealth()
    {
        IHealable healable = _player.GetComponent<IHealable>();
        if(healable != null)
        {
           healable.Heal(1.0f);
        }
    }
}
