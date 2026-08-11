using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float x, y;

    public Vector3 limitMax;
    public Vector3 limitMin;

    public Vector3 temp;

    public GameObject[] prefabBullets;
    public float speed;
    float time;

    float fireDelay;
    Animator animator;
    bool onDead;

    // 아이템
    public int Damage;
    public int Boom;

    private void Start()
    {
        fireDelay = 0;
        speed = 10.0f;

        animator = GetComponent<Animator>();
        onDead = false;

        Damage = 1;
        Boom = 0;
    }

    private void Update()
    {
        Move();
        FireBullet(); 
        OnDeadCheck();
    }

    public void Move()
    {
        float x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float y = Input.GetAxis("Vertical")   * speed * Time.deltaTime;

        transform.Translate(new Vector3(x, y, 0));

        if (transform.position.x > limitMax.x)
        {
            temp.x = limitMax.x;
            temp.y = transform.position.y;
            transform.position = temp;
        }
        if (transform.position.y > limitMax.y)
        {
            temp.x = transform.position.x;
            temp.y = limitMax.y;
            transform.position = temp;
        }
        if (transform.position.x < limitMin.x)
        {
            temp.x = limitMin.x;
            temp.y = transform.position.y;
            transform.position = temp;
        }
        if (transform.position.y < limitMin.y)
        {
            temp.x = transform.position.x;
            temp.y = limitMin.y;
            transform.position = temp;
        }
    }

    public void FireBullet()
    {
        fireDelay += Time.deltaTime;
        
        if (fireDelay > 0.3f)
        {
            Instantiate(prefabBullets[Damage - 1], transform.position, Quaternion.identity);
            fireDelay -= .3f;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(limitMin, new Vector2(limitMax.x, limitMin.y));
        Gizmos.DrawLine(limitMin, new Vector2(limitMin.x, limitMax.y));
        Gizmos.DrawLine(limitMax, new Vector2(limitMax.x, limitMin.y));
        Gizmos.DrawLine(limitMax, new Vector2(limitMin.x, limitMax.y));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        {
            animator.SetInteger("State", 1);
            onDead = true;
        }
    }

    void OnDeadCheck()
    {
        if (onDead)
        {
            time += Time.deltaTime;
        }
        if (time > .6f)
        {
            Destroy(gameObject);
        }
    }
}
