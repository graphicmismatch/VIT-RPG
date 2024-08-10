using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct Item {
    public string Name;
    public StatChange effects;
    public bool equippable;
}
[System.Serializable]
public struct StatChange {
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
    public void AddItem(Item i)
    {

        inventory.Add(i);
    }
    public void AddItem(Item i,int n)
    {
        for (int j = 0; j < n; j++)
        {
            inventory.Add(i);
        }
    }
    public void EqwipItem(Item i) {
        equipped.Add(i);
        inventory.Remove(i);

        playerValues.inst.AddStats(i.effects);
    }
    public void UnequipItem(Item i)
    {
        inventory.Add(i);
        equipped.Remove(i);

        playerValues.inst.RemoveStats(i.effects);
    }

    public void ThrowItem(Item i) {
        inventory.Remove(i);
    }
    public void UseItem(Item i)
    {
        playerValues.inst.AddStats(i.effects);
        inventory.Remove(i);
    }
}
