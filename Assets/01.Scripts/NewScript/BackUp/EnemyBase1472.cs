using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase1472 : MonoBehaviour
{
    #region 스크립트에이블로 뺼거
    /*public float attackDelay; //대쉬는 멈추는 시간과 에너미 어택을 가지고 있거나 안가지고있다
    [Header("공격하기전에 애니메실행초 전")]
    public float attackMinuesDelay = 0.2f;
    [Range(0, 2f)]
    public float EnemyEyeSpeed = 1f;    
    public enum State
    {
        Move,
        Attack,
        Hit,
        Dead,
        Stun,
        Skill,
        Dash,
        Run,
        Idle
    }
    public float judgeTime = 0.2f;
    public float stunTime = 0.5f; //RecoverTime*/
    #endregion
    #region 대체될거
    /*protected EnemyMove move;

    protected EnemyFOV fov;
    protected EnemyAttack attack;
    EnemyEyeAnim animEYE;*/
    #endregion
    #region Legarcy Code
    /*
    protected bool isDash = false;
    protected bool isRun = false;
    protected Vector2 destination;
    protected bool moveSet = false;
    private readonly int hashDetact = Animator.StringToHash("Detect");
    private readonly int hashEyeSpeed = Animator.StringToHash("EyeSpeed");
    protected float lastAttackTime = 0;
    public bool isAttack = false;
    public bool AttackTimeReset = true;
    public bool eyeAnimation = true;
    public EnemyInfo.State currentState = EnemyInfo.State.Move;
    private WaitForSeconds ws;*/
    #endregion
    [SerializeField] protected EnemyInfo enemyInfo;
    [SerializeField] protected Animator anim;
    [SerializeField] protected GameObject coinEffect;
    [SerializeField] protected GameObject hitParticle;
    [SerializeField] protected Vector3 pos;
    protected SpriteRenderer spriteRenderer;
    protected Collider2D collider2D;
    protected float currentSpeed;
    protected int currentHp;
    protected Rigidbody2D rb;

    bool isCanMove = true;
    bool isDead = false;
    bool isCanAttack = false;
    public bool isCanDamage = false;
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentSpeed = enemyInfo.speed;
        currentHp = enemyInfo.hp;
        StartCoroutine(Attack());
    }
    public void Update()
    {
        if (transform.position.x >= enemyInfo.attackPos)
        {
            isCanAttack = false;
        }
        else
        {
            isCanAttack = true;
        }
        Move();
    }
    virtual protected void OnTriggerStay2D(Collider2D other)
    {
        print("트리거 작동");
    }
    virtual protected void OnTriggerEnter2D(Collider2D other)
    {
        print("트리거 작동");
        if (other.CompareTag("Attack"))
        {
            Damaged(1);
        }
        if (other.CompareTag("Ground") && isDead)
        {
            //1안
            spriteRenderer.flipY = true;
            spriteRenderer.color = Color.gray;
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(new Vector2(1, 5), ForceMode2D.Impulse);
            collider2D.enabled = false;
        }
    }
    IEnumerator Dash()
    {
        currentSpeed = enemyInfo.dashSpeed;
        yield return new WaitForSeconds(1f);
        currentSpeed = enemyInfo.speed;
    }
    IEnumerator Attack()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Player");
        while (true)
        {
            yield return new WaitUntil(() => isCanAttack);
            yield return new WaitForSeconds(enemyInfo.attackDelay);
            spriteRenderer.color = Color.blue;
            isCanDamage = true;
            yield return new WaitForSeconds(enemyInfo.parringAbleTime);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 10,layerMask);
            if (hit)
            {
                hit.transform.GetComponent<Player>().GetDamage(enemyInfo.damage);
            }
            if (isCanAttack)
            {
                anim.Play("Attack");
            }
            spriteRenderer.color = Color.white;
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
            rb.velocity = new Vector2(0, rb.velocity.y);
            isCanMove = true;
            StartCoroutine(Dash());
        }
    }
    virtual protected void Damaged(int damage)
    {
        //if (isCanDamage)
        //{
            currentHp -= damage;
            Instantiate(hitParticle, pos, Quaternion.identity);
            PushedBack();
            if (currentHp <= 0)
            {
                Instantiate(coinEffect);
                Dead();
            }
        //}
    }
    virtual protected void PushedBack()
    {
        isCanMove = false;
        rb.AddForce(new Vector2(currentSpeed * 10f, 2.5f), ForceMode2D.Impulse);
    }
    public void Dead()
    {
        //1안
        isDead = true;
        collider2D.isTrigger = true;
        //2안
        /*rb.velocity = new Vector2(0, 0);
        rb.AddForce(new Vector2(15, 10), ForceMode2D.Impulse);
        collider2D.enabled = false;
        groundCheckCol.enabled = false;*/
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
    /*if (isAttack) //투르 펄스로 공격 AI로 구현
{
    if (AttackTimeReset)
    {
        //트루되고 시간이 초기화 되가지고 다시 안되는구나
        AttackTimeReset = false;
        lastAttackTime = Time.time;

    }

    if (lastAttackTime + enemyInfo.attackDelay - enemyInfo.attackMinuesDelay <= Time.time && eyeAnimation)
    {
        eyeAnimation = false;
        SetEyeSpeed(enemyInfo.enemyEyeSpeed);
        SetEyeAnime();
    }

    if (lastAttackTime + enemyInfo.attackDelay <= Time.time) //딜레이가 끝나 다시 공격가능하다면
    {
        lastAttackTime = Time.time;
        isAttack = false;
        eyeAnimation = true;
        Attack();
    }

}*/
    /*public void Attack() { 

    } //다 공격방법이 다르니 추상클래스로
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
            yield return ws; //요고만큼 기다렸다가 다음 체크하게됨 그라운드무브가 그사이에 동작하면서
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
        if (currentState == EnemyInfo.State.Hit || currentState == EnemyInfo.State.Dead) //히트후 리커버 //이게 Attack되면 무브로 바로전환을 못하니까 여기서 currentState == State.Attack할 필요가 없다.
        {
            return;
        }

        //bool isTrace = IsTracePlayer();

        //bool isAttack = IsAttackPossible();


        if (false) //바꿀거
        {

            currentState = EnemyInfo.State.Dash; //쫒아가고
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
        
        currentState = EnemyInfo.State.Move; //다시 왼쪽으로 이동
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