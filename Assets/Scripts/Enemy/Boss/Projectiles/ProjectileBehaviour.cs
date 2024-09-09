using UnityEngine;
using System.Collections;

public class ProjectileBehaviour : MonoBehaviour
{
    Vector3 dir;
    [Range(0,1)]
    public int behaviour;
    public float speed;
    public float TimeToDie;
    public float projradius;
    public float damage;
    public bool reflectable;
    public float reflectedSpeedMult;
    public bool reflected;
    void Start()
    {
        dir = Vector2.down;
        StartCoroutine(die());
    }
    private void Update()
    {
        switch (behaviour) {
            case 0:
                transform.Translate(dir * speed * Time.deltaTime);
                break;
            case 1:
                float a = Vector3.Angle(this.transform.position, PlayerMovement.position);
                transform.eulerAngles = new Vector3(0, 0, a);
                transform.position += (PlayerMovement.position - transform.position).normalized * speed * Time.deltaTime;
                break;
            default:
                Destroy(gameObject);
                break;
        }
        
    }
    void LateUpdate()
    {
        if (Vector2.Distance(transform.position, PlayerMovement.position) <= projradius) {
            playerValues.takeAttack(damage);
            Destroy(gameObject);
        }
    }
    public void onHit(Vector2 dir) {
        if (reflectable) {
            behaviour = 0;
            this.dir = dir;
            speed *= reflectedSpeedMult;
            reflected = true;
        }
    }
    IEnumerator die()
    {
        yield return new WaitForSeconds(TimeToDie);
        Destroy(this.gameObject);
    }
}
