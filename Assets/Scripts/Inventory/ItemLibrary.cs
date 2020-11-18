using System.Collections;
using System.Collections.Generic;
using InventorySystem;
using UnityEngine;

public class ItemLibrary : MonoBehaviour
{
    private static ItemLibrary _instance;

    public static ItemLibrary Instance
    {
        get
        {
            if(_instance == null)
            {
                GameObject go = new GameObject("ItemLibrary");
                go.AddComponent<ItemLibrary>();       
            }
            return _instance;
        }
    }

    public List<Item> equipment = new List<Item>();

    public List<Item> amplifiers = new List<Item>();

    public List<Item> consumables = new List<Item>();

    public List<Item> scrolls = new List<Item>();

    private void Awake()
    {
        _instance = this;
    }
 
}

