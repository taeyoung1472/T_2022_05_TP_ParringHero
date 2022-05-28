using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAlpabet : MonoBehaviour
{
    private Fiver _fiver;
    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private Transform target;

    private void Start()
    {
        _fiver = GameObject.Find("Fiver").GetComponent<Fiver>();
    }
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);
    }
}