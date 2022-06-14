using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tank : MonoBehaviour
{
    #region 구동 관련
    [Header("구동 관련")]
    [SerializeField] private Transform[] wheels;
    [SerializeField] private float wheelSpeed;
    [SerializeField] private float speed;
    [SerializeField] private float stopPos;
    Rigidbody2D rb;
    float hSpeed;
    #endregion

    #region 노예 관련
    [Header("노예 관련")]
    [SerializeField] private GameObject slave;
    [SerializeField] private GameObject slaveMannequin1;
    [SerializeField] private GameObject slaveMannequin2;
    [SerializeField] private Vector3 slaveDropPos;
    [SerializeField] private Vector3 slaveWaitPos;
    [SerializeField] private Vector3 slaveHide;
    Queue<GameObject> slaveQueue = new Queue<GameObject>();
    #endregion

    #region 대포 관련
    [Header("대포 관련")]
    [SerializeField] private Transform cannonFirePos;
    [SerializeField] private GameObject cannonBall;
    [SerializeField] private Vector2 fireForce;
    #endregion

    private void Awake()
    {
        slaveQueue.Enqueue(slaveMannequin1);
        slaveQueue.Enqueue(slaveMannequin2);
        hSpeed = wheelSpeed;
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        RotateWheel();
        Move();
        if (Input.GetKeyDown(KeyCode.P))
        {
            SpawnSlave();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ShoopCannon();
        }
    }
    void RotateWheel()
    {
        foreach (Transform wheel in wheels)
        {
            wheel.Rotate(Vector3.forward * hSpeed * Time.deltaTime);
        }
    }
    void Move()
    {
        if (transform.position.x > stopPos)
        {
            rb.velocity = Vector2.left * speed + Vector2.up * rb.velocity.y;
            hSpeed = wheelSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
            hSpeed = -wheelSpeed;
        }
    }
    public void SpawnSlave()
    {
        GameObject spawnTarget = slaveQueue.Dequeue();
        GameObject waitTarget = slaveQueue.Dequeue();
        Sequence seq = DOTween.Sequence();
        seq.Append(spawnTarget.transform.DOLocalJump(slaveDropPos, 2.5f, 1, 1f));
        seq.Join(waitTarget.transform.DOLocalJump(slaveWaitPos,1f, 1, 1f));
        seq.AppendCallback(() =>
        {
            Instantiate(slave, spawnTarget.transform.position, Quaternion.identity);
            spawnTarget.transform.localPosition = slaveHide;
            slaveQueue.Enqueue(waitTarget);
            slaveQueue.Enqueue(spawnTarget);
        });
    }
    void ShoopCannon()
    {
        GameObject ball = Instantiate(cannonBall, cannonFirePos.position, Quaternion.identity);
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.left * ((ball.transform.position.x - GameManager.Instance.Player.position.x) * fireForce.x) + (Vector2.up * fireForce.y);
        ball.GetComponent<Rigidbody2D>().AddTorque(360);
    }
}
