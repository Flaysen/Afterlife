using System.Collections;
using System.Collections.Generic;
using InventorySystem;
using UnityEngine;

[System.Serializable]
public class ItemLoot : MonoBehaviour
{
    [System.Serializable]
    public struct ItemDropType
    {
        [Range(0, 100)]
        public int dropChance;

        public List<Item> itemsToDrop;
    }

    [Range(0, 100)]
    [SerializeField] private int dropChance;
    [Space]
    [Range(0,100)]
    [SerializeField] private int consumableDropChance;
    [Range(0, 100)]
    [SerializeField] private int equipmentDropChance;
    [SerializeField] private ItemDropType equipable = new ItemDropType();
    [SerializeField] private ItemDropType consumable = new ItemDropType();

    private List<ItemDropType> itemDropTypes = new List<ItemDropType>();

    private List <Item> itemTypeToDrop = new List<Item>();

    private void Start()
    {
        equipable.dropChance = equipmentDropChance;
        equipable.itemsToDrop = ItemLibrary.Instance.equipment;
        itemDropTypes.Add(equipable);

        consumable.dropChance = consumableDropChance;
        consumable.itemsToDrop = ItemLibrary.Instance.consumables;
        itemDropTypes.Add(consumable);
    }

    public void RollType()
    {
        Debug.Log("Roll Type");
        int typeWeight =+ consumableDropChance + equipmentDropChance;
            
        int randomValue = Random.Range(0, typeWeight);

        for (int j = 0; j < itemDropTypes.Count; j++)
        {
            if (randomValue <= itemDropTypes[j].dropChance)
            {
                itemTypeToDrop = itemDropTypes[j].itemsToDrop;
                return;
            }
            randomValue -= itemDropTypes[j].dropChance;
        }
    }
       
    public Item RollLoot()
    {
        Debug.Log("Roll loot");
        int rollDropChance = Random.Range(1, 101);

        if(rollDropChance > dropChance)
        {
            Debug.Log("No Drop");
            return null;
        }

        RollType();

        if (rollDropChance <= dropChance)
        {
            int randomValue = Random.Range(0, itemTypeToDrop.Count);
            return itemTypeToDrop[randomValue];
        }
        else Debug.Log("!!"); return null;  
    }
}
