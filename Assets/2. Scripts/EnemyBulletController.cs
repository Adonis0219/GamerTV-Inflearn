using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    float moveSpeed;
    float rotateSpeed;
    float time;
    Rigidbody2D rb2D;
    GameObject player;

    private void Start()
    {
        moveSpeed   = 10f;
        rotateSpeed = 300f;
        time   = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        rb2D   = GetComponent<Rigidbody2D>();

        FireBullet();
    }

    private void Update()
    {
        RotateBullet();
        DestroyBullet();
    }

    void FireBullet()
    {
        Vector3 dist = player.transform.position - transform.position;
        Vector3 dir = dist.normalized;

        // 1초당 움직이는 거리
        rb2D.linearVelocity = dir * moveSpeed;
    }

    void RotateBullet()
    {
        transform.rotation = Quaternion.Euler(0, 0, time *  rotateSpeed);
    }

    void DestroyBullet()
    {
        time += Time.deltaTime;

        if (time > 5f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
