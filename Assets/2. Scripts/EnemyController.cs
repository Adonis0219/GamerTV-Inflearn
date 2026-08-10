using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // 총알
    public GameObject enemyBullet;
    GameObject player;
    float fireDelay;

    // 아이템
    public GameObject[] items;

    // 파괴
    Animator animator;
    [SerializeField] AnimationClip deadClip;

    bool onDead;
    float time;

    // 이동
    Rigidbody2D rb2D;
    float moveSpeed;

    static string BULLET_TAG = "Bullet";
    static string STATE = "State";
    static string BLOCKCOLLIDER = "BlockCollider";
    static string ITEMDROPENEMY = "ItemDropEnemy";

    private void Start()
    {
        animator  = GetComponent<Animator>();
        rb2D      = GetComponent<Rigidbody2D>();
        player    = GameObject.FindWithTag("Player");
        onDead    = false;
        moveSpeed = Random.Range(5f, 8f);
        time      = 0.0f;
        fireDelay = 2.5f;

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

            if (gameObject.CompareTag(ITEMDROPENEMY))
            {
                int tmp = Random.Range(0, 2);
                Instantiate(items[tmp], transform.position, Quaternion.identity);
            }
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
        if (collision.CompareTag(BLOCKCOLLIDER))
        {
            OnDisappear();
        }
    }

    void OnDead()
    {
        onDead = true;
    }

    void OnDisappear()
    {
        Destroy(gameObject);
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
