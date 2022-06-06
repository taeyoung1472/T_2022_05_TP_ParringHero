using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerWeaponeManager : MonoBehaviour
{
    [SerializeField] PlayerWeapon[] weapons;
    void Start()
    {
        foreach (PlayerWeapon weapone in weapons)
        {
            if (weapone.WeaponType == GameManager.Instance.currentUser.weaponType)
            {
                weapone.Weapon.SetActive(true);
            }
        }
    }
    void Update()
    {
        
    }
    [Serializable]
    class PlayerWeapon
    {
        [SerializeField] private WeaponType weaponType;
        [SerializeField] private GameObject weapon;
        public WeaponType WeaponType { get { return weaponType; } }
        public GameObject Weapon { get { return weapon; } }
    }
}