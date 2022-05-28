using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiverCol : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("날11가");
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("날라가");
            Vector2 randomFly = new Vector2(Random.Range(5, 10f), Random.Range(3f, 10f));
            collision.gameObject.GetComponent<EnemyBase>().Rb.AddForce(randomFly, ForceMode2D.Impulse);

        }
    }
}
