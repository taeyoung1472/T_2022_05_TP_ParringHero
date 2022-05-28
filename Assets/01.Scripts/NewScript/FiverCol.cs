using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiverCol : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector2 randomFly = new Vector2(Random.Range(5, 10f), Random.Range(3f, 10f));
            collision.gameObject.GetComponent<EnemyBase>().Rb.AddForce(randomFly, ForceMode2D.Impulse);

        }
    }
}
