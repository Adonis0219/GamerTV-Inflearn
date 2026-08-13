using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    float height;
    float speed;
    BoxCollider2D col;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        height = col.size.y;
        speed = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (transform.position.y <= -height)
            RePos();
    }

    void Move()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    void RePos()
    {
        Vector3 offset = new Vector3(0, height * 2, 0);
        transform.position = transform.position + offset;
    }
}
