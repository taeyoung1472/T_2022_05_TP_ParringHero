using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : EnemyBase
{
    [SerializeField] private GameObject arrow;
    [SerializeField] private Vector2 arrowForce;
    protected override IEnumerator Attack()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Player");
        while (true)
        {
            yield return new WaitUntil(() => isCanAttack);
            yield return new WaitForSeconds(enemyInfo.attackDelay);
            print("АјАн");
            //_playerMat.SetInt("_AttackWaiting", 1);
            anim.Play("Attack");
            yield return new WaitForSeconds(enemyInfo.animTime);
            GameObject obj = Instantiate(arrow,transform.position,Quaternion.identity);
            PlaySound(enemyInfo.attack[Random.Range(0, enemyInfo.attack.Length)]);
            obj.GetComponent<Arrow>().Set(Vector2.left * ((transform.position.x - GameManager.Instance.Player.position.x) * arrowForce.x) + (Vector2.up * arrowForce.y),transform, arrowForce);
            //spriteRenderer.color = Color.blue;
            //isCanDamage = true;
            /*yield return new WaitForSeconds(enemyInfo.parringAbleTime);
            if (isCanAttack)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 10, layerMask);
                if (hit)
                {
                    hit.transform.GetComponent<Player>().GetDamage(enemyInfo.damage);
                }
                anim.Play("Attack");
            }*/
            //isCanDamage = false;
            //spriteRenderer.color = Color.white;
        }
    }
}