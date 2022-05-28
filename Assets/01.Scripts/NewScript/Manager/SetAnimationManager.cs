using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimationManager : MonoBehaviour
{
    [SerializeField] private Animator[] animator;
    [SerializeField] private Animator anim;
    public ProductType productType;
    public void Start()
    {
        Set();
    }
    public void Set()
    {
        int temp = 0;
        switch (productType)
        {
            case ProductType.Character:
                temp = GameManager.Instance.currentUser.playerIndex;
                break;
            case ProductType.Pet:
                temp = GameManager.Instance.currentUser.petIndex;
                break;
        }
        anim.runtimeAnimatorController = animator[temp].runtimeAnimatorController;
    }
}