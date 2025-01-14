using UnityEngine;

public class Target : MonoBehaviour
{

    public float health = 100f;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            die();
        }
    }

    void die()
    {
        Destroy(gameObject);
    }
}
