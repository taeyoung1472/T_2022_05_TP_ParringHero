using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public bool isRead = false;
    public int code;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isRead) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            isRead = true;
            DialogManager.ShowDialog(code);
        }
    }
}
