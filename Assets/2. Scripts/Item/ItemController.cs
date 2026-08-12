using UnityEngine;

public class ItemController : MonoBehaviour
{
    protected GameObject player;
    protected float speed;
    protected int score;

    private void Start()
    {
        speed = 10f;
        score = 100;
    }

    private void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            ItemGain();
        }
        if (collision.CompareTag("BlockCollider"))
        {
            Destroy(gameObject);
        }
    }

    protected virtual void ItemGain()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
    }
}