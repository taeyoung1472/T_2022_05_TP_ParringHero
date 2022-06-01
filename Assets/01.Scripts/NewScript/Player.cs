using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour, ISound
{
    public int MaxHp { set { maxHp = value; } get { return maxHp; } }
    public int CurHp { set { curHp = value; } get { return curHp; } }
    public bool IsAttackSucces { set { isAttackSucces = value; } }
    //[SerializeField] private Animator[] petAnim;
    [SerializeField] private Collider2D attackCollider;
    //[SerializeField] private GameObject bloodEfeect;//, soundObject;
    [SerializeField] private int maxHp;
    [SerializeField] private OverPanel deadUI;
    //[SerializeField] private Animator petAnimator;
    [SerializeField] private HeartSystem heartSystem;
    [SerializeField] private AudioClip[] attackAudioClip;
    [SerializeField] private float attackDelay = 0.5f;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private float attackRange;
    //[SerializeField] private float attackCharge;
    //[SerializeField] private Slider chargeSlider;

    [SerializeField] private float atkTime = 0f;// 공격 대기신간

    private bool isInvincibility = false;
    public bool IsInvincibility
    {
        get => isInvincibility;
        set
        {
            isInvincibility = value;
        }
    }
    private int curHp;
    bool isDead;
    bool isAttackSucces;
    int timeTemp;
    int reberseChance;
    float originAttackColPos;
    readonly int AttackHashStr = Animator.StringToHash("Attack");
    readonly int MoveHashStr = Animator.StringToHash("Move");
    public void Start()
    {
        originAttackColPos = attackCollider.transform.position.x;
        //chargeSlider.maxValue = attackCharge;
        playerAnimator.SetBool(MoveHashStr, true);
        reberseChance = GameManager.Instance.currentUser.reberseChance;
        maxHp += GameManager.Instance.currentUser.fixedMaxHP + GameManager.Instance.currentUser.petMaxHp;
        timeTemp = Mathf.RoundToInt(Time.time);
        curHp = maxHp;
        //petAnimator.runtimeAnimatorController = petAnim[GameManager.Instance.currentUser.petIndex].runtimeAnimatorController;
        try
        {
            heartSystem.Set(maxHp);
        }
        catch
        {
            print("Tuto");
        }
        StartCoroutine(CheckAttack());
    }
    public void Update()
    {
        atkTime -= Time.deltaTime;
        CheckInput();
    }
    public void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (atkTime > 0)
            {
                return;
            }
            if (GameManager.Instance.IsTuto)
            {
                if (GameManager.Instance.isAttackTuto)
                {
                    playerAnimator.SetTrigger("Attack");
                    StartCoroutine(Attack());
                }
            }
            else
            {
                playerAnimator.SetTrigger("Attack");
                StartCoroutine(Attack());
            }
            PlaySound(attackAudioClip[GameManager.Instance.currentUser.playerIndex]);
        }
    }
    IEnumerator CheckAttack()
    {
        while (true)
        {
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));
            if (GameManager.Instance.IsTuto)
            {
                if (GameManager.Instance.isAttackTuto)
                {
                    StartCoroutine(Attack());
                }
            }
            else
            {
                StartCoroutine(Attack());
            }
            playerAnimator.SetTrigger(AttackHashStr);
            PlaySound(attackAudioClip[GameManager.Instance.currentUser.playerIndex]);
            yield return new WaitForSeconds(0.2f);
            if (isAttackSucces)
            {
                isAttackSucces = false;
                atkTime = 0f;

            }
            else
            {
                yield return new WaitForSeconds(attackDelay - 0.2f);
            }
        }
    }
    public IEnumerator Attack()
    {
        attackCollider.enabled = true;
        attackCollider.transform.DOMoveX(originAttackColPos + attackRange, 0.25f);
        atkTime = 2f;
        yield return new WaitForSeconds(0.25f);

        attackCollider.enabled = false;
        attackCollider.transform.DOMoveX(originAttackColPos, 0);
    }
    public void GetDamage(int damage)
    {
        if (isInvincibility)
        {
            return;
        }
        curHp -= damage;
        //Instantiate(bloodEfeect, transform.position, Quaternion.identity);
        if (curHp > maxHp)
        {
            curHp = maxHp;
        }
        else if (curHp <= 0 && !isDead)
        {
            Dead();
        }
        heartSystem.Heart(curHp);
    }
    public void Heal(int amount)
    {
        curHp += amount;
        if (curHp >= maxHp)
        {
            curHp = maxHp;
        }
        heartSystem.Heart(curHp);
    }
    public void SetAnim(int index)
    {
        //animator = playerAnim[index];
    }
    public void SetPet(int index)
    {
        //petAnimator = petAnim[index];
    }
    void Dead()
    {
        if (reberseChance > 0)
        {
            reberseChance--;
            curHp = Mathf.RoundToInt(maxHp / 2);
        }
        else
        {
            isDead = true;
            deadUI.gameObject.SetActive(true);
            deadUI.Set((int)Time.time - timeTemp, GameManager.Instance.CoinUI.Coin);
        }
    }

    public void PlaySound(AudioClip clip, float volume = 1)
    {
        PoolManager_Test.instance.Pop(PoolType.Sound).GetComponent<AudioPool>().PlayAudio(clip, volume);
    }
}