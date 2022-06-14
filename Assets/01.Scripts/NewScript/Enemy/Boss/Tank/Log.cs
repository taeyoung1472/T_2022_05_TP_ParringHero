using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    [SerializeField] private float logRotateSpeed = 360f;
    [SerializeField] private float logSpeed = 1f;
    Rigidbody2D rb;
    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Start()
    {
        StartCoroutine(Jump());
    }
    public void Update()
    {
        transform.Rotate(Vector3.forward * logRotateSpeed * Time.deltaTime);
    }
    IEnumerator Jump()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.75f, 1.25f));
            rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            rb.AddForce(Vector2.left * logSpeed, ForceMode2D.Impulse);
        }
    }
}
