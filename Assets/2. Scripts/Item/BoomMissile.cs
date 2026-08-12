using UnityEngine;

public class BoomMissile : MonoBehaviour
{
    public float speed;
    float time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 35f;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        MoveBoom();
        DestroyBoom();
    }

    void MoveBoom()
    {
        transform.Translate(Vector3.up * speed *  Time.deltaTime);
    }

    void DestroyBoom()
    {
        time += Time.deltaTime;

        if (time > 3f)
        {
            Destroy(gameObject);
        }
    }
}
