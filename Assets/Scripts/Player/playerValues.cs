using UnityEngine;

public class playerValues: MonoBehaviour{
    public string playerName;
    public Vector3 playerPosition;
    public float playerHealth;
    public int worldId;
    public static float movementSpeed;
    

    public void Start(){
        playerName = null;
        playerPosition = new Vector3(0, 0, 0);
        playerHealth = 100;
        worldId = 0;
        movementSpeed = 5.0f;
    }
}