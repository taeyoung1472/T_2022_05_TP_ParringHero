using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    [SerializeField] private float logRotateSpeed = 360f;
    [SerializeField] private float logSpeed = 1f;
    [SerializeField] private AudioClip logClip;
    bool isDestroyed = false;
    Rigidbody2D rb;
    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Start()
    {
        StartCoroutine(Jump());
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack") && !isDestroyed)
        {
            PoolManager_Test.instance.Pop(PoolType.Sound).GetComponent<AudioPool>().PlayAudio(logClip, 1, Random.Range(0.8f, 1.2f));
            GameManager.Instance.Player.GetComponent<Player>().IsAttackSucces = true;
            isDestroyed = true;
            PushBack();
        }
        if (collision.CompareTag("Boss") && isDestroyed)
        {
            PoolManager_Test.instance.Pop(PoolType.Sound).GetComponent<AudioPool>().PlayAudio(logClip, 1, Random.Range(0.8f, 1.2f));
            collision.gameObject.GetComponentInParent<WoodTank>().Damaged();
            Destroy(gameObject);
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PoolManager_Test.instance.Pop(PoolType.Sound).GetComponent<AudioPool>().PlayAudio(logClip, 1, Random.Range(0.8f, 1.2f));
            Destroy(gameObject);
        }
    }
    IEnumerator Jump()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            PoolManager_Test.instance.Pop(PoolType.Sound).GetComponent<AudioPool>().PlayAudio(logClip, 1, Random.Range(1.8f, 2.2f));
            rb.AddTorque(logRotateSpeed, ForceMode2D.Impulse);
            rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        }
    }
    void PushBack()
    {
        StopAllCoroutines();
        rb.AddForce(Vector2.up * 5 + Vector2.right * 15, ForceMode2D.Impulse);
    }
}
