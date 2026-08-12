using System.Collections;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Rendering;

public static class Tag
{
    public static string BULLET_TAG = "Bullet";
    public static string BOOM_TAG = "BoomMissile";
    public static string STATE = "State";
    public static string PLAYER = "Player";
    public static string BLOCKCOLLIDER = "BlockCollider";
    public static string ITEMDROPENEMY = "ItemDropEnemy";
}

public class EnemyController : MonoBehaviour
{
    // 총알
    public GameObject enemyBullet;
    GameObject player;
    PlayerController playerController;
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

    // HP
    int hp;
    // 태그 임시 저장
    string tagName;

    // 점수
    int score;

    private void Start()
    {
        animator  = GetComponent<Animator>();
        rb2D      = GetComponent<Rigidbody2D>();
        player    = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        onDead    = false;
        moveSpeed = Random.Range(5f, 8f);
        time      = 0.0f;
        fireDelay = 2.5f;
        tagName = gameObject.tag;

        if (gameObject.CompareTag(Tag.ITEMDROPENEMY))
        {
            score = 30;
            hp = 3;
        }
        else
        {
            score = 10;
            hp = 1;
        }

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

            if (tagName == Tag.ITEMDROPENEMY)
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
        if (collision.CompareTag(Tag.BULLET_TAG))
        {
            hp -= playerController.Damage;
        }
        if (collision.CompareTag(Tag.BOOM_TAG))
        {
            hp -= playerController.boomDmg;
        }
        if (collision.CompareTag(Tag.BLOCKCOLLIDER))
        {
            OnDisappear();
        }
        if (hp <= 0)
        {
            animator.SetInteger(Tag.STATE, 1);
            OnDead();
        }
    }

    void OnDead()
    {
        onDead = true;

        if (gameObject.tag != "Untagged")
        {
            // 스코어 증가 코드 작성
            UIManager.instance.Score += score;
        }

        // 죽을 때 태그를 없애서 총알 중복 손실 방지
        gameObject.tag = "Untagged";
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
