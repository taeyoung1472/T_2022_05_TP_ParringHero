using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : PoolingBase
{
    [SerializeField] private AudioClip reboundAudio;
    Rigidbody2D rb;
    float startForce;
    bool isRebound;
    public CamManager camManager;
    public float stopTime;
    public bool stopping;
    public float slowTime;
    public void Set(float force)
    {
        camManager = GameManager.Instance.CamManager;
        rb = GetComponent<Rigidbody2D>();
        startForce = force;
        rb.velocity = new Vector2(-force,0);
    }
    public void Rebound()
    {
        camManager.SetCamShake(0.3f, 2f);
        isRebound = true;
        startForce = startForce * 2.5f;
        rb.velocity = new Vector2(startForce, 0);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && isRebound)
        {
            collision.GetComponent<EnemyBase>().GetDamage();
            InstanceaPool();
        }
        if (collision.CompareTag("Attack"))
        {
            GameManager.Instance.Player.GetComponent<Player>().PlaySound(reboundAudio);
            Rebound();
            GameManager.Instance.Player.GetComponent<Player>().IsAttackSucces = true;
        }
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().GetDamage(1);
            InstanceaPool();
        }
    }
    public void TimeStop()
    {
        if (!stopping)
        {
            stopping = true;
            Time.timeScale = 0.5f;
            StartCoroutine(Stop());
        }
    }
    IEnumerator Stop()
    {
        yield return new WaitForSecondsRealtime(stopTime);
        Time.timeScale = 0.01f;
        yield return new WaitForSecondsRealtime(slowTime);

        Time.timeScale = 1;
        stopping = false;
    }
}