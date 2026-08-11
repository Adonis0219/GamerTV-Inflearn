using UnityEngine;

public class ItemController : MonoBehaviour
{
    protected GameObject player;
    protected float speed;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        speed = 10f;
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

    protected virtual void ItemGain() { }
}