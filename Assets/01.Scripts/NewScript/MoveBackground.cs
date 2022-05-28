using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    public float speed;
    public float limitPos;
    public float startPos;
    public float orignspeed;
    private void Start()
    {
        orignspeed = speed;
    }
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        if (transform.position.x <= limitPos)
        {
            transform.position = new Vector3(startPos, transform.position.y, transform.position.z);
        }
    }
}