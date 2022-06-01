using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float length;
    [SerializeField] private Transform layer1, layer2;
    private float orignspeed;
    private int level = 1;

    private void Start()
    {
        orignspeed = speed;
    }
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        if (transform.position.x < -(length * level))
        {
            if(layer1.position.x > layer2.position.x)
            {
                layer2.position = new Vector3(layer1.position.x + length, 0, 0);
            }
            else
            {
                layer1.position = new Vector3(layer2.position.x + length, 0, 0);
            }
            level++;
        }
    }
    public void SetFever()
    {
        speed = orignspeed * 2.5f;
    }
    public void EndFiver()
    {
        speed = orignspeed;
    }
}