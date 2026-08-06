using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float x, y;

    public Vector3 limitMax;
    public Vector3 limitMin;

    public Vector3 temp;

    public GameObject prefabBullet;
    float time;
    public float speed;

    private void Start()
    {
        time = 0;
        speed = 10.0f;
    }

    private void Update()
    {
        Move();
        FireBullet();
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
        time += Time.deltaTime;
        
        if (time > 0.3f)
        {
            Instantiate(prefabBullet, transform.position, Quaternion.identity);
            time -= .3f;
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
}
