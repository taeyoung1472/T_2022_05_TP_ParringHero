using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteo : MonoBehaviour
{
    [SerializeField] private Vector2 randSpeed;
    [SerializeField] private AudioClip[] clip;
    Transform spriteRendere;
    Rigidbody2D rb;
    float rotSpeed;
    private void Awake()
    {
        spriteRendere = transform.GetComponentInChildren<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        rotSpeed = Random.Range(randSpeed.x, randSpeed.y);
        rb.velocity = 
            Vector2.right * Random.Range(randSpeed.x, randSpeed.y) +
            Vector2.down * Random.Range(randSpeed.x, randSpeed.y);
    }
    private void Update()
    {
        spriteRendere.Rotate(Vector3.forward * rotSpeed * 20 * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponentInParent<EnemyBase>().GetDamage();
            PlayClip();
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            PlayClip();
            Destroy(gameObject);
        }
    }
    void PlayClip()
    {
        PoolManager_Test.instance.Pop(PoolType.Sound).GetComponent<AudioPool>().PlayAudio(clip[Random.Range(0, clip.Length)], 0.25f, 2f + Random.Range(0f, 1f));
    }
}
