using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiverCol : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("��11��");
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("����");
            collision.gameObject.GetComponent<EnemyBase>().FeverPush();
        }
    }
}
