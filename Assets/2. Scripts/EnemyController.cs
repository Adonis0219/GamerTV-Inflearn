using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject enemyBullet;
    GameObject player;
    float fireDelay;

    Rigidbody2D rb2D;
    float moveSpeed;

    Animator animator;
    [SerializeField] AnimationClip deadClip;

    static string BULLET_TAG = "Bullet";
    static string STATE = "State";

    bool onDead;
    float time;

    private void Start()
    {
        animator  = GetComponent<Animator>();
        rb2D      = GetComponent<Rigidbody2D>();
        player    = GameObject.FindWithTag("Player");
        onDead    = false;
        moveSpeed = Random.Range(5f, 8f);
        time      = 0.0f;

        Move();
    }

    private void Update()
    {
        if (onDead)
        {
            time += Time.deltaTime;
        }
        if (time > 0.6f)
        {
            Destroy(gameObject);
        }

        FireBullet();
    }

    void Move()
    {
        if (player == null) return;

        Vector3 dist = player.transform.position - transform.position;
        Vector3 dir = dist.normalized;
        rb2D.linearVelocity = dir * moveSpeed;
    }

    public void FireBullet()
    {
        if (player == null) 
            return;

        fireDelay += Time.deltaTime;

        if (fireDelay > 3f)
        {
            Instantiate(enemyBullet, transform.position, Quaternion.identity);
            fireDelay -= 3f; 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(BULLET_TAG))
        {
            animator.SetInteger(STATE, 1);
            OnDead();
        }
    }

    void OnDead()
    {
        onDead = true;
    }

    #region 코루틴
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag(BULLET_TAG))
    //    {
    //        StartCoroutine(OnDead());
    //    }
    //}

    //IEnumerator OnDead()
    //{
    //    animator.SetInteger(STATE, 1);

    //    yield return new WaitForSeconds(deadClip.length);
    //    Destroy(gameObject);
    //}
    #endregion
}
