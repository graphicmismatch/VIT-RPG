using UnityEngine;


public class savedPlayerValues
{
    public string playerName;
    public Vector3 playerPosition;
    public float playerHealth;
    public int worldId;
    public Stats stats;

}
public struct Stats
{
    public float MaxHealth;
    public float Atk;
    public float Def;
    public float Spd;
    public float InventorySpace;
}
public class playerValues : MonoBehaviour
{
    public string playerName;
    public float playerHealth;
    public int worldId;
    public Stats stats;
    public static playerValues inst;
    private void Awake()
    {
        inst = this;
    }
    public void Start()
    {
        playerName = null;
        playerHealth = 100;
        worldId = 0;
    }
    public static void takeAttack(float health) {
        inst.playerHealth -= 1+((health)*((100-inst.stats.Def)/100));
        CameraShake.sc.shake(0.5f);
    }
    public void AddStats(StatChange sc)
    {
        stats.MaxHealth += sc.MaxHealth;
        stats.Atk += sc.Atk;
        stats.Def += sc.Def;
        stats.Spd += sc.InventorySpace;
        stats.InventorySpace += sc.InventorySpace;
    }
    public void RemoveStats(StatChange sc)
    {
        stats.MaxHealth -= sc.MaxHealth;
        stats.Atk -= sc.Atk;
        stats.Def -= sc.Def;
        stats.Spd -= sc.InventorySpace;
        stats.InventorySpace -= sc.InventorySpace;
    }
}
