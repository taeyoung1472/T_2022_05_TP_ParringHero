using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WoodTank : MonoBehaviour
{
    #region 참조
    [Header("참조")]
    [SerializeField] private BossDataSO bossData;
    [SerializeField] private CamManager camManager;
    [SerializeField] private SpawnManager spawnManager;
    #endregion

    #region 구동 관련
    [Header("구동 관련")]
    [SerializeField] private Transform[] wheels;
    [SerializeField] private float wheelSpeed;
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

    #region 통나무 관련
    [SerializeField] private GameObject log;
    [SerializeField] private Transform logSpawnPos;
    #endregion

    #region 공격 관련
    bool isCanAttack = false;
    #endregion

    #region
    [Header("효과 관련")]
    [SerializeField] private Transform hpBar;
    int hp;
    #endregion

    #region
    [Header("사운드")]
    [SerializeField] private AudioClip cannonShoot;
    [SerializeField] private AudioClip slaveSpawn;
    [SerializeField] private AudioClip logDrop;
    [SerializeField] private AudioClip hit;
    [SerializeField] private AudioClip dead;
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
    }
    private void Start()
    {
        hp = bossData.hp;
        StartCoroutine(BossCycle());
    }
    private void OnDestroy()
    {
        spawnManager.IsBossAlive = false;
    }
    public void Damaged()
    {
        hp--;
        hpBar.transform.localScale = new Vector3((float)hp / (float)bossData.hp, 1, 1);
        if(hp <= 0)
        {
            PlayAudio(dead, Random.Range(0.8f, 1.2f));
            Destroy(gameObject);
        }
        else 
        {
            PlayAudio(hit, Random.Range(0.8f, 1.2f));
        }
    }
    IEnumerator BossCycle()
    {
        yield return new WaitUntil(() => isCanAttack);
        camManager.ZoomInOut();
        yield return new WaitForSeconds(3f);
        while (true)
        {
            yield return new WaitUntil(() => isCanAttack);
            switch (Random.Range(0, 3))
            {
                case 0:
                    SpawnSlave();
                    break;
                case 1:
                    for (int i = 0; i < 5; i++)
                    {
                        if(Random.Range(0, 2) == 0)
                        {
                            ShoopCannon();
                        }
                        yield return new WaitForSeconds(0.25f);
                    }
                    break;
                case 2:
                    SpawnLog();
                    break;
            }
            yield return new WaitForSeconds(bossData.defaultAttackDelay);
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
        if (transform.position.x > bossData.stopPos)
        {
            rb.velocity = Vector2.left * bossData.defaultSpeed + Vector2.up * rb.velocity.y;
            hSpeed = wheelSpeed;
            isCanAttack = false;
        }
        else
        {
            rb.velocity = Vector2.zero;
            hSpeed = -wheelSpeed;
            isCanAttack = true;
        }
    }
    void SpawnSlave()
    {
        PlayAudio(slaveSpawn, Random.Range(1.5f, 1.9f));
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
        PoolManager_Test.instance.Pop(PoolType.Sound).GetComponent<AudioPool>().PlayAudio(cannonShoot, Random.Range(0.8f, 1.2f));
        GameObject ball = Instantiate(cannonBall, cannonFirePos.position, Quaternion.identity);
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.left * ((ball.transform.position.x - GameManager.Instance.Player.position.x) * fireForce.x) + (Vector2.up * fireForce.y);
        ball.GetComponent<Rigidbody2D>().AddTorque(360);
    }
    void SpawnLog()
    {
        PlayAudio(logDrop, Random.Range(0.8f, 1.2f));
        Instantiate(log, logSpawnPos.position, Quaternion.identity);
    }
    void PlayAudio(AudioClip clip, float pitch = 1, float volume = 1)
    {
        PoolManager_Test.instance.Pop(PoolType.Sound).GetComponent<AudioPool>().PlayAudio(clip, pitch, volume);
    }
}
