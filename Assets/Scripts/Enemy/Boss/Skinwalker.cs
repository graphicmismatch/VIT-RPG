using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Skinwalker : MonoBehaviour
{
    public Animator anim;
    public Vector2 attackTime;
    private float AtkAfter;
    private float timer;
    public GameObject[] projectile;
    public float[] timeBetweenBullets;
    public LayerMask proj;
    public int arcBounds;
    public int bulletsInArc;
    public int frameCount;
    public float maxhealth;
    float health;
    public Image bar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxhealth;
        resetTimer();
    }
    void resetTimer()
    {
        AtkAfter = Random.Range(attackTime.x, attackTime.y);
        timer = 0;
    }
    // Update is called once per frame
    void Update()
    {
        frameCount++;
        timer += Time.deltaTime;
        if (timer >= AtkAfter)
        {
            anim.SetTrigger("atk");
            resetTimer();
            
        }
        if (frameCount >= 15) {
            frameCount = 0;
            Collider2D[] colls = Physics2D.OverlapBoxAll(transform.position, new Vector2(4, 4), 0, proj);
            if (colls.Length > 0) {
                foreach (Collider2D c in colls) {
                    if (c.gameObject.GetComponent<ProjectileBehaviour>().reflected)
                    {
                        health -= c.gameObject.GetComponent<ProjectileBehaviour>().damage;
                        Destroy(c.gameObject);
                        bar.fillAmount = (health / maxhealth);
                    }
                }
            }
        }

    }
    public void startAtk() {
        StartCoroutine(attack());
    }
    IEnumerator attack()
    {
        int atkId = Random.Range(0, 3);
        switch (atkId)
        {
            case 0:
                for (int i = -arcBounds; i <= arcBounds; i += (int)((2 * arcBounds) / bulletsInArc))
                {
                    Quaternion rotation = Quaternion.Euler(0, 0, i);
                    Instantiate(projectile[0], this.transform.position, rotation);
                    yield return new WaitForSeconds(timeBetweenBullets[0]);
                }
                for (int i = arcBounds; i >= -arcBounds; i -= (int)((2 * arcBounds) / bulletsInArc))
                {
                    Quaternion rotation = Quaternion.Euler(0, 0, i);
                    Instantiate(projectile[0], this.transform.position, rotation);
                    yield return new WaitForSeconds(timeBetweenBullets[0]);
                }
                break;
            case 1:
                Instantiate(projectile[1], this.transform.position, Quaternion.identity);
                yield return new WaitForSeconds(timeBetweenBullets[1]);
                break;
            case 2:
                for (int i = -arcBounds; i <= arcBounds; i += (int)((2 * arcBounds) / bulletsInArc))
                {
                    Quaternion rotation = Quaternion.Euler(0, 0, i);
                    Instantiate(projectile[0], this.transform.position, rotation);
                   
                }
                break;
            default:
                break;
        }
        yield return null;
    }


}
