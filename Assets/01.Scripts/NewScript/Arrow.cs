using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : PoolingBase , ISound
{
    [SerializeField] private AudioClip reboundAudio;
    bool isRebound;
    Transform startTrans;
    Rigidbody2D rb;
    Vector2 originForce;
    SpriteRenderer spriteRenderer;
    public CamManager camManager;
    public float stopTime;
    public bool stopping;
    public float slowTime;
    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        camManager = GameManager.Instance.CamManager;
    }
    public void Update()
    {
        float angle;
        if (!isRebound)
        {
            angle = Mathf.Atan2(transform.position.y - GameManager.Instance.Player.position.y, transform.position.x - GameManager.Instance.Player.position.x) * Mathf.Rad2Deg;
        }
        else
        {
            angle = Mathf.Atan2(transform.position.y - startTrans.position.y, transform.position.x - startTrans.position.x) * Mathf.Rad2Deg;
        }
        if (rb.velocity.y > 0)
        {
            if (Mathf.Abs(rb.velocity.y) <= 0.1f)
            {
                angle = 0;
            }
            angle = -angle;
        }
        transform.localEulerAngles = new Vector3(0, 0, angle);
    }
    public void PlaySound(AudioClip clip, float volume = 1)
    {
        throw new System.NotImplementedException();
    }
    public void Set(Vector2 force, Transform transform, Vector2 orgForce)
    {
        originForce = orgForce;
        startTrans = transform;
        SetVelocity(force);
    }
    public void Rebound()
    {
        camManager.SetCamShake(0.3f, 2f);
        rb.velocity = Vector2.zero;
        SetVelocity(Vector2.left * ((transform.position.x - startTrans.position.x) * originForce.x) + (Vector2.up * originForce.y * 0.8f));
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack"))
        {
            if (!isRebound)
            {
                transform.localEulerAngles = new Vector3(0, 180, transform.localEulerAngles.z);
                isRebound = true;
                GameManager.Instance.Player.GetComponent<Player>().PlaySound(reboundAudio);
                Rebound();
                GameManager.Instance.Player.GetComponent<Player>().IsAttackSucces = true;
            }
        }
        if (collision.CompareTag("Enemy"))
        {
            if (isRebound)
            {
                InstanceaPool();
                collision.GetComponent<EnemyBase>().GetDamage();
            }
        }
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().GetDamage(1);
            InstanceaPool();
        }
    }
    public void SetVelocity(Vector2 force)
    {
        rb.velocity = force;
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
