using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinEffect : MonoBehaviour
{
    [SerializeField] private Vector2 target;//Transform target;
    [SerializeField] private float returnTime;
    [SerializeField] private float speed;
    [SerializeField] private float force;
    [SerializeField] private float returningTime;
    private Rigidbody2D rb;
    float temp;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(Random.Range(-force, force), Random.Range(-force, force)),ForceMode2D.Impulse);
        StartCoroutine(Return());
    }
    IEnumerator Return()
    {
        temp = Time.time;
        returningTime *= Random.Range(0.8f, 1.2f);
        yield return new WaitForSeconds(returnTime);
        rb.velocity = new Vector2(0,0);
        while (true)
        {
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * speed);
            if(temp + returningTime < Time.time)
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        gameObject.SetActive(false);
    }
}
