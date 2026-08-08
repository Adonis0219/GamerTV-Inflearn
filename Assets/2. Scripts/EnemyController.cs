using UnityEngine;

public class EnemyController : MonoBehaviour
{
    static string BULLET_TAG = "Bullet";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(BULLET_TAG))
            Destroy(gameObject);
    }
}
