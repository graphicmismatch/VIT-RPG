using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    protected Vector2 movement;
    public Vector2 direction;
    private Rigidbody2D rb;
    private float centeringTimer;
    private bool centering;
    private Vector2 centeringTarget;
    private Vector2 centeringOrigin;
    private bool feasibleCenteringPath;

    public Animator anim;
    public float speed;
    public LayerMask wallLayers;
    public float centeringTime;
    public static Vector3 position;
    public static PlayerMovement inst;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        centeringTimer = 0f;
        centering = false;
        rb = GetComponent<Rigidbody2D>();
        inst = this;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = movement * speed;
        if (Mathf.Approximately(movement.sqrMagnitude, 0))
        {
            if (!centering)
            {
                centering = true;
                centeringTarget = new Vector2(((direction.x > 0) ? Mathf.CeilToInt(this.transform.position.x) : Mathf.FloorToInt(this.transform.position.x)), ((direction.y > 0) ? Mathf.CeilToInt(this.transform.position.y) : Mathf.FloorToInt(this.transform.position.y)));
                //direction = Vector2.zero;
                centeringOrigin = this.transform.position;
                feasibleCenteringPath = Physics2D.LinecastAll(centeringOrigin, centeringTarget, wallLayers).Length == 0;
            }
            if (feasibleCenteringPath)
            {

                this.transform.position = Vector2.Lerp(centeringOrigin, centeringTarget, centeringTimer / centeringTime);

            }
            if (centeringTimer <= centeringTime)
            {
                centeringTimer += Time.deltaTime;
            }
        }
        else
        {
            centering = false;
            centeringTimer = 0;
        }
        anim.SetFloat("DirX", direction.x);
        anim.SetFloat("DirY", direction.y);
        anim.SetBool("moving", rb.velocity.SqrMagnitude()>0);
        position = this.transform.position;
    }
    public void OnMove(InputValue ctx)
    {
        if (ctx.Get() != null)
        {
            direction = (((Vector2)ctx.Get()).sqrMagnitude == 0)?direction: (Vector2)ctx.Get();
            movement = ((Vector2)ctx.Get()).normalized;
        }
        else
        {
            movement = Vector2.zero;
        }
    }
}
