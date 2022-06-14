using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private AudioClip explosionClip;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Explosion();
    }

    public void Explosion()
    {
        Destroy(Instantiate(explosionEffect, transform.position, Quaternion.identity), 5f);
        PoolManager_Test.instance.Pop(PoolType.Sound).GetComponent<AudioPool>().PlayAudio(explosionClip);
        Destroy(gameObject);
    }
}
