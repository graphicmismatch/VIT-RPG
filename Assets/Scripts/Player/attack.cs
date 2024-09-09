using UnityEngine;

public class attack : MonoBehaviour
{

    public PlayerMovement playerMovement;
    public Transform attacker;
    public float damage = 10f;
    public float range = 5f;
    public LayerMask Enemy;
    public Animator anim;
    public string[] TargetTags;

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
        anim.SetTrigger("Atk");
        RaycastHit2D hit = Physics2D.Raycast(attacker.position, playerMovement.direction, range,Enemy);
        Debug.Log(hit);
        if (hit.collider != null)
        {
            if (checkTags(hit.collider.tag, TargetTags))
            {
                Debug.Log("HIT");
                Target target = hit.transform.GetComponent<Target>();
                target.TakeDamage(damage);
            }
            else if (hit.collider.tag == "Projectile") {
                ProjectileBehaviour target = hit.transform.GetComponent<ProjectileBehaviour>();
                target.onHit(PlayerMovement.inst.direction);
            }
        }
        //anim.ResetTrigger("Atk");
    }
    private bool checkTags(string s, string[] tags) {
        foreach (string p in tags) {
            if (s == p) {
                return true;
            }
        }
        return false;
    }
}
