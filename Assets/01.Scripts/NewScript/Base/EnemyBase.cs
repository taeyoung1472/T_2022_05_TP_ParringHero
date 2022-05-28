using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : PoolAbleObject//PoolingBase , ISound
{
    [SerializeField] protected EnemyInfo enemyInfo;
    //[SerializeField] protected GameObject coinEffect;
    [SerializeField] protected GameObject hitParticle;
    //[SerializeField] protected GameObject enemyHitParticle;
    [SerializeField] protected GameObject deadParticle;
    //[SerializeField] protected GameObject soundObject;
    [SerializeField] protected Transform rayTrans;
    [SerializeField] protected Collider2D col;
    protected float currentSpeed;
    protected int currentHp;
    protected Rigidbody2D rb;
    public Rigidbody2D Rb { get => rb; }

    [SerializeField] protected Animator anim;
    [Header("���ִϸ��̼� �ӵ�")]
    [Range(0, 2)]
    [SerializeField]  private float eyeAnimSpeed = 1f;
    protected bool isCanMove = true;
    protected bool isDead = false;
    protected bool isDeadEffect = false;
    protected bool isCanAttack = false;
    protected bool isCanDamage = false;

    public float stopTime;
    public bool stopping;
    public float slowTime;
    private CamManager camManager;

    public static int damage = 1;
    public static int deadCount;
    protected readonly int attackHashStr = Animator.StringToHash("Attack");
    protected readonly int moveHashStr = Animator.StringToHash("Move");
    protected readonly int deadHasStr = Animator.StringToHash("Die");

    private Fiver _fiver;

    [SerializeField] private AudioClip readyDamaged;

    #region �������̽� ������
    public override void Init_Pop()
    {

    }

    public override void Init_Push()
    {

    }
    #endregion
    public void Start()
    {
        camManager = GameManager.Instance.CamManager;
        _fiver = GameObject.Find("Fiver").GetComponent<Fiver>();
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = enemyInfo.speed;
        currentHp = enemyInfo.hp;
        anim.SetBool(moveHashStr, true);
        StartCoroutine(Attack());
    }
    public void Update()
    {
        if (transform.position.x >= GameManager.Instance.Player.position.x + enemyInfo.attackPos)
        {
            isCanAttack = false;
            Move();
        }
        else
        {
            isCanAttack = true;
        }
    }
    virtual public void GetDamage()
    {
        StartCoroutine(Damaged(1));
    }
    virtual protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Attack"))
        {
            if (isCanDamage)
            {
                //print("����");
                TimeStop();
                Instantiate(hitParticle, new Vector3(transform.position.x, transform.position.y + 0.5f), Quaternion.identity);
                StartCoroutine(Damaged(1));
                camManager.SetCamShake(0.3f, 2f);
                GameManager.Instance.Player.GetComponent<Player>().IsAttackSucces = true;
            }
        }
        if (other.CompareTag("OutLine"))
        {
            PoolManager_Test.instance.Push(poolType, gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && isDead && !isDeadEffect)
        {
            anim.Play(deadHasStr);
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(new Vector2(1, 5), ForceMode2D.Impulse);
            GameObject obj = Instantiate(deadParticle, transform.position, Quaternion.identity);
            obj.transform.localScale = new Vector3(enemyInfo.particleScale, enemyInfo.particleScale, enemyInfo.particleScale);
            isDeadEffect = true;
            PlaySound(enemyInfo.die[Random.Range(0, enemyInfo.die.Length)]);
            col.enabled = false;
        }
    }
    IEnumerator Dash()
    {
        currentSpeed = enemyInfo.dashSpeed;
        yield return new WaitForSeconds(1f);
        currentSpeed = enemyInfo.speed;
    }
    protected virtual IEnumerator Attack()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Player");
        while (true)
        {
            yield return new WaitUntil(() => isCanAttack);
            print("���õ�����" );
            yield return new WaitForSeconds(enemyInfo.attackDelay);
            isCanDamage = true;
            anim.SetTrigger(attackHashStr);
            print("�и����̺�");
            PoolManager_Test.instance.Pop(PoolType.Sound).GetComponent<AudioPool>().PlayAudio(readyDamaged);
            yield return new WaitForSeconds(enemyInfo.parringAbleTime);

            print("����!");
            if (isCanAttack && !isDead)
            {
                RaycastHit2D hit = Physics2D.Raycast(rayTrans.position, Vector2.left, 15, layerMask);
                if (hit)
                {
                    hit.transform.GetComponent<Player>().GetDamage(enemyInfo.damage);
                }
                PlaySound(enemyInfo.attack[Random.Range(0, enemyInfo.attack.Length)]);
            }
            isCanDamage = false;
        }
    }
    virtual protected void Move()
    {
        if (isCanMove)
        {
            rb.velocity = new Vector2(-currentSpeed, rb.velocity.y);
        }
        else if (Mathf.Abs(rb.velocity.x) < 0.1f && !isDead)
        {
            isCanMove = true;
            StartCoroutine(Dash());
        }
    }
    virtual protected IEnumerator Damaged(int damage)
    {
        PlaySound(enemyInfo.damaged[Random.Range(0, enemyInfo.damaged.Length)]);
        currentHp -= damage;
        isCanAttack = false;
        yield return new WaitForSeconds(0.1f);
        PushedBack();
        if (currentHp <= 0)
        {
            //Instantiate(coinEffect, transform.position, Quaternion.identity);
            Dead();
        }
        yield return new WaitForSeconds(1f);
        isCanAttack = true;
    }

    public void TimeStop()
    {
        if (!stopping)
        {
            stopping = true;
            Time.timeScale = 0.5f;
            StartCoroutine(Stop());
        }
    }
    IEnumerator Stop()
    {
        yield return new WaitForSecondsRealtime(stopTime);
        Time.timeScale = 0.01f;
        yield return new WaitForSecondsRealtime(slowTime);

        Time.timeScale = 1;
        stopping = false;
    }
    public void Dead()
    {
        isDead = true;

        deadCount++;
        if (GameManager.Instance.IsTuto)
        {
            DialogManager.ShowDialog(1, GameManager.Instance.SpawnEnd);
        }
        SpawnAlpabet();
        Destroy(gameObject, 5f);
    }
    virtual protected void PushedBack()
    {
        isCanAttack = false;
        isCanMove = false;
        rb.AddForce(new Vector2(10f, 2.5f), ForceMode2D.Impulse);
    }
    public void PlaySound(AudioClip clip, float volume = 1)
    {
        PoolManager_Test.instance.Pop(PoolType.Sound).GetComponent<AudioPool>().PlayAudio(clip, volume);
    }
    virtual protected void SpawnAlpabet()
    {
        int persent = Random.Range(0, 100) + 1;
        if (persent <= 100)
        {
            _fiver.SpawnAlpa();
        }
    }
    #region Legacy Code
    //StartCoroutine(CheckNextAction());
    /*isDash = false;
isRun = false;

spriteRenderer = GetComponent<SpriteRenderer>();
rigid = GetComponent<Rigidbody2D>();
anim = GetComponent<Animator>();
currentSpeed = enemyInfo.speed;
currentState = EnemyInfo.State.Move;
ws = new WaitForSeconds(enemyInfo.judgeTime);*/
    /*if (isAttack) //���� �޽��� ���� AI�� ����
{
    if (AttackTimeReset)
    {
        //Ʈ��ǰ� �ð��� �ʱ�ȭ �ǰ����� �ٽ� �ȵǴ±���
        AttackTimeReset = false;
        lastAttackTime = Time.time;

    }

    if (lastAttackTime + enemyInfo.attackDelay - enemyInfo.attackMinuesDelay <= Time.time && eyeAnimation)
    {
        eyeAnimation = false;
        SetEyeSpeed(enemyInfo.enemyEyeSpeed);
        SetEyeAnime();
    }

    if (lastAttackTime + enemyInfo.attackDelay <= Time.time) //�����̰� ���� �ٽ� ���ݰ����ϴٸ�
    {
        lastAttackTime = Time.time;
        isAttack = false;
        eyeAnimation = true;
        Attack();
    }

}*/
    /*public void Attack() { 

    } //�� ���ݹ���� �ٸ��� �߻�Ŭ������
    public void eyeAnimationBool()
    {
        //eyeAnimation = true;
    }
    public virtual void Stop()
    {
        //rigid.velocity = Vector2.zero;
        //moveSet = false;
        //isDash = false;
    }
    public virtual void SetDash(Vector2 target)
    {
        destination = target;
        currentSpeed = enemyInfo.dashSpeed;
        moveSet = false;
        isDash = true;
    }
    public virtual void SetRun()
    {
        currentSpeed = enemyInfo.runSpeed;
        moveSet = true;
        isRun = true;
    }
    public virtual void SetMove()
    {
        moveSet = true;
        isDash = false;
        currentSpeed = enemyInfo.speed;
    }
    IEnumerator CheckNextAction()
    {
        while (true)
        {
            CheckState();
            Action();
            yield return ws; //���ŭ ��ٷȴٰ� ���� üũ�ϰԵ� �׶��幫�갡 �׻��̿� �����ϸ鼭
        }
        yield return null;
    }

    public void SetEyeAnime()
    {
        //anim.SetTrigger(hashDetact);
    }

    public void SetEyeSpeed(float value)
    {
        //anim.SetFloat(hashEyeSpeed, value);
    }

    protected virtual void CheckState()
    {
        if (currentState == EnemyInfo.State.Hit || currentState == EnemyInfo.State.Dead) //��Ʈ�� ��Ŀ�� //�̰� Attack�Ǹ� ����� �ٷ���ȯ�� ���ϴϱ� ���⼭ currentState == State.Attack�� �ʿ䰡 ����.
        {
            return;
        }

        //bool isTrace = IsTracePlayer();

        //bool isAttack = IsAttackPossible();


        if (false) //�ٲܰ�
        {

            currentState = EnemyInfo.State.Dash; //�i�ư���
        }
        else
        {
            currentState = EnemyInfo.State.Move;
        }
    }

    protected virtual void Action()
    {
        switch (currentState)
        {
            case EnemyInfo.State.Move:
                isAttack = false;
                SetMove();
                break;

            case EnemyInfo.State.Attack:
                Stop();
                isAttack = true;
                break;

            case EnemyInfo.State.Hit:
                isAttack = false;
                StartCoroutine(Recover(enemyInfo.stunTime));
                break;

            case EnemyInfo.State.Dash:
                isAttack = false;
                SetDash(PlayerPositionManager.Player.position);
                break;

            case EnemyInfo.State.Run:
                isAttack = false;
                SetRun();
                break;

            case EnemyInfo.State.Dead:
                break;
        }
    }

    public virtual void SetHit()
    {
        Stop();
        currentState = EnemyInfo.State.Hit;

        StopCoroutine(Recover(enemyInfo.stunTime));
        StartCoroutine(Recover(enemyInfo.stunTime));
    }

    protected virtual IEnumerator Recover(float time)
    {
        yield return new WaitForSeconds(time);
        AttackTimeReset = true;
        eyeAnimation = true;
        
        currentState = EnemyInfo.State.Move; //�ٽ� �������� �̵�
        yield return null;
    }

    public void SetDead()
    {
        currentState = EnemyInfo.State.Dead;
        isAttack = false;
        Stop();
    }*/
    #endregion
}