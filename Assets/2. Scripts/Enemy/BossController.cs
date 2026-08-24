using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    //
    public GameObject player;
    public PlayerController playerController;
    // 체력바
    public float hp1; // 초록색
    public float hp2; // 빨간색
    // 
    Animator animator;
    //
    bool onDead;
    bool isSpawn;
    //
    // 점수
    int score;
    //
    float time;
    //
    Transform spawnMovePos;
    //
    float speed;

    // 총알 쏘는 위치
    public Transform LAtkPos;
    public Transform RAtkPos;
    // 총알
    public GameObject bossBullet;
    // 총알 딜레이
    public float fireDelay;

    // 애니메이션 상태 확인용
    int animNumber;

    private void Awake()
    {
        hp1 = 150.0f;
        hp2 = 150.0f;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnMovePos = GameObject.Find("BossPos").GetComponent<Transform>();
        animator = GetComponent<Animator>();

        onDead = false;
        isSpawn = true;

        score = 1000;

        speed = 10;

        animNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpawn)
        {
            BossSpawn();
        }
        if (onDead)
        {
            time += Time.deltaTime;
        }
        if (time > 0.6f)
        {
            Destroy(gameObject);
        }
        if (player == null && GameManager.instance.LifeCnt >= 0)
        {
            PlayerFind();
        }

        FireBullet();
        AnimationSystem();
    }

    public void PlayerFind()
    {
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    void FireBullet()
    {
        // 총알 발사 애니메이션
        if (hp1 > 0 && isSpawn == false)
        {
            fireDelay += Time.deltaTime;

            // 공격 딜레이가 1.0초 지나고 L공격 상태가 아니면
            if (fireDelay > 1.0f && animNumber != 1)
            {
                // L공격
                animNumber = 1;
                fireDelay -= fireDelay;
            }
        }
        if (hp1 <= 0)
        {
            fireDelay += Time.deltaTime;

            // 공격 딜레이가 1.0초 지나고 L공격 상태가 아니면
            if (fireDelay > 1.0f && animNumber != 2)
            {
                // L공격
                animNumber = 2;
                fireDelay -= fireDelay;
            }
        }
    }
    
    // 애니메이션은 따로 관리
    void AnimationSystem()
    {
        if (animNumber == 0)
        {
            StartCoroutine(Co_Idle());
        }
        if (animNumber == 1)
        {
            StartCoroutine(Co_LAtk());
        }
        if (animNumber == 2)
        {
            StartCoroutine(Co_RAtk());
        }
    }

    IEnumerator Co_Idle()
    {
        animNumber = -1;
        animator.SetTrigger(Tag.IDLE);
        yield return new WaitForSeconds(.6f);
    }

    IEnumerator Co_LAtk()
    {
        animNumber = -1;
        animator.SetTrigger(Tag.LATK);
        yield return new WaitForSeconds(.6f);
        animNumber = 0;
    }

    IEnumerator Co_RAtk()
    {
        animNumber = -1;
        animator.SetTrigger(Tag.RATK);
        yield return new WaitForSeconds(.6f);
        animator.SetTrigger(Tag.RATK);
        yield return new WaitForSeconds(.6f);
        animator.SetTrigger(Tag.RATK);
        yield return new WaitForSeconds(.6f);
        animNumber = 0;
    }

    void LAtk()
    {
        if (player == null)
            return;

        Instantiate(bossBullet, LAtkPos.position, Quaternion.identity);
        fireDelay -= 1;
    }

    void RAtk()
    {
        if (player == null)
            return;

        Instantiate(bossBullet, RAtkPos.position, Quaternion.identity);
        fireDelay -= 1;
    }

    void OnDead()
    {
        onDead = true;

        if (gameObject.tag != "Untagged")
        {
            // 스코어 증가 코드 작성
            UIManager.instance.Score += score;
            SoundManager.instance.enemyDeadSnd.Play();
        }

        // 죽을 때 태그를 없애서 총알 중복 손실 방지
        gameObject.tag = "Untagged";
    }

    void BossSpawn()
    {
        transform.position = Vector3
                           // MoveTowards(현재위치, 목표위치, 거리차이(속도))
                             .MoveTowards(transform.position, 
                              spawnMovePos.position, Time.deltaTime * speed);

        if (transform.position == spawnMovePos.position)
            isSpawn = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tag.BULLET_TAG))
        {
            if (hp1 > 0)
                hp1 -= playerController.Damage;
            else
                hp2 -= playerController.Damage;
        }
        if (collision.CompareTag(Tag.BOOM_TAG))
        {
            if (hp1 > 0)
                hp1 -= playerController.boomDmg;
            else
                hp2 -= playerController.boomDmg;
        }
        if (hp2 <= 0)
        {
            animator.SetTrigger(Tag.DIE);
            OnDead();
        }
    }
}
