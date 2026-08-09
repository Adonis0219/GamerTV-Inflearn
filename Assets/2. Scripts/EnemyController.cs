using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    static string BULLET_TAG = "Bullet";
    static string STATE = "State";

    Animator animator;
    [SerializeField] AnimationClip deadClip;

    bool onDead;
    float time;

    private void Start()
    {
        animator = GetComponent<Animator>();
        onDead   = false;
        time     = 0.0f;
    }

    private void Update()
    {
        if (onDead)
        {
            time += Time.deltaTime;
        }
        if (time > 0.6f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(BULLET_TAG))
        {
            animator.SetInteger(STATE, 1);
            OnDead();
        }
    }

    void OnDead()
    {
        onDead = true;
    }

    #region 코루틴
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag(BULLET_TAG))
    //    {
    //        StartCoroutine(OnDead());
    //    }
    //}

    //IEnumerator OnDead()
    //{
    //    animator.SetInteger(STATE, 1);

    //    yield return new WaitForSeconds(deadClip.length);
    //    Destroy(gameObject);
    //}
    #endregion
}
