using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : EnemyBase
{
    [SerializeField] private GameObject magicBall;
    [SerializeField] private float magicBallSpeed;
    protected override IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitUntil(() => isCanAttack);
            yield return new WaitForSeconds(enemyInfo.attackDelay);
            //_playerMat.SetInt("_AttackWaiting", 1);
                  
            print("АјАн");
            anim.Play("Attack");
            yield return new WaitForSeconds(enemyInfo.animTime);
            GameObject obj = Instantiate(magicBall,transform.position,Quaternion.identity);
            obj.GetComponent<MagicBall>().Set(magicBallSpeed);
            PlaySound(enemyInfo.attack[Random.Range(0, enemyInfo.attack.Length)]);
        }
    }
}