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
            collision.gameObject.GetComponent<EnemyBase>().FeverPush();
        }
    }
}
