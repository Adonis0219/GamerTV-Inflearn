using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    float time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 30.0f;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        MoveBullet();
        DestroyBullet();
    }

    void MoveBullet()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    void DestroyBullet()
    {
        time += Time.deltaTime;
        
        if (time > 3.0f)
        {
            Destroy(gameObject);    
        }
    }
}
