using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/Enemy/Boss")]
public class BossDataSO : ScriptableObject
{
    public string bossName = "NewBoss";
    public int hp = 5;
    public float defaultAttackDelay = 2.5f;
    public float defaultSpeed = 5f;
    public float stopPos = 5f;
}
