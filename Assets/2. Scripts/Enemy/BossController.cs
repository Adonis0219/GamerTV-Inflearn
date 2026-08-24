using UnityEngine;

public class BossController : MonoBehaviour
{
    //
    GameObject player;
    PlayerController playerController;
    // 체력바
    public float hp1; // 초록색
    public float hp2; // 빨간색
    public float MaxHp1; // 빨간색
    public float MaxHp2; // 빨간색
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

    private void Awake()
    {
        hp1 = 150.0f;
        hp2 = 150.0f;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        spawnMovePos = GameObject.Find("BossPos").GetComponent<Transform>();

        animator = GetComponent<Animator>();

        onDead = false;
        isSpawn = true;

        score = 1000;

        speed = 10;
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
            isSpawn = true;
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
