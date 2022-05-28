using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpGradeSystem : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private CoinUI coinUI;
    public void SetBuff(Player _player, CoinUI _coinUI)
    {
        coinUI = _coinUI;
        player = _player;
        StartCoroutine(Buff());
    }
    IEnumerator Buff()
    {
        User user = GameManager.Instance.currentUser;
        EnemyBase.damage = user.fixedDamage + user.petDamage + 1;
        coinUI.CoinValue = user.timePerCoin + user.petTimePerCoin + 1;
        print("ภ๛ฟ๋ตส");
        while (true)
        {
            yield return new WaitForSeconds(30);
            player.Heal(user.timePerHealHp + user.petTimePetHeal);
            print("ภ๛ฟ๋ตส");
        }
    }
}