using UnityEngine;
using System.Collections.Generic;


public enum itemInteraction { 
    USEANDTHROW,USEANDKEEP,EQUIPPABLE
}
[System.Serializable]
public struct Item
{
    public string Name;
    public StatChange effects;
    public itemInteraction itemInt;
    public bool KeyItem;
}
[System.Serializable]
public struct StatChange
{
    public float MaxHealth;
    public float Atk;
    public float Def;
    public float Spd;
    public float InventorySpace;
}
public class InventoryManager : MonoBehaviour
{
    public List<Item> inventory = new List<Item>();
    public List<Item> equipped = new List<Item>();
    public static InventoryManager inst;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        inst = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool AddItem(Item i)
    {
        if (inventory.Count >= playerValues.inst.stats.InventorySpace) {
            return false;
        }
        inventory.Add(i);
        return true;
    }
    public int AddItem(Item i, int n)
    {
        for (int j = 0; j < n; j++)
        {
            if (inventory.Count >= playerValues.inst.stats.InventorySpace)
            {
                return j;
            }
            inventory.Add(i);
        }
        return n;
    }
    public bool EqwipItem(Item i)
    {
        if (i.itemInt != itemInteraction.EQUIPPABLE) {
            return false;
        }
        equipped.Add(i);
        inventory.Remove(i);

        playerValues.inst.AddStats(i.effects);
        return true;
    }
    public void UnequipItem(Item i)
    {
        inventory.Add(i);
        equipped.Remove(i);

        playerValues.inst.RemoveStats(i.effects);
    }

    public bool ThrowItem(Item i)
    {
        if (i.KeyItem)
        {
            return false;
        }
        inventory.Remove(i);
        return true;
    }
    public bool UseItem(Item i)
    {
        if (i.itemInt != itemInteraction.USEANDKEEP&&i.itemInt != itemInteraction.USEANDTHROW)
        {
            return false;
        }
        playerValues.inst.AddStats(i.effects);
        if (i.itemInt == itemInteraction.USEANDTHROW)
        {
            inventory.Remove(i);
        }
        return true;
    }
}
