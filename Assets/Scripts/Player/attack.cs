using UnityEngine;

public class attack : MonoBehaviour
{

    public PlayerMovement playerMovement;
    public Transform attacker;
    public float damage = 10f;
    public float range = 5f;
    public LayerMask Enemy;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Fired");
            doattack();
        }
    }

    void doattack()
    {
        RaycastHit2D hit = Physics2D.Raycast(attacker.position, playerMovement.direction, range,Enemy);
        Debug.Log(hit);
        if (hit.collider != null)
        {
            Debug.Log("HIT");
            Target target = hit.transform.GetComponent<Target>();
            target.TakeDamage(damage);
        }
    }
}
