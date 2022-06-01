using UnityEngine;
[CreateAssetMenu(fileName = "Info", menuName = "Enemy")]
public class EnemyInfo : ScriptableObject
{
    public int hp, damage;
    public float speed, runSpeed, dashSpeed, parringAbleTime;
    public float attackDelay, stunTime, attackMinuesDelay, judgeTime = 0.2f;
    public float animTime;
    public float attackPos;
    public float particleScale;
    public float maxAttackSpeed;
    public float maxSpeed;
    public AudioClip[] attack, damaged, die;
    public enum MonsterType { high, ground, underground, goblin, plant, slimebullet, Wizard, Sekleton, Dathnight, MantiCore };
    public MonsterType monsterType;
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
}