using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Diffculty")]
public class DifficultySO : ScriptableObject
{
    public string name;
    public float animAtkSpeed;
    public float animMoveSpeed;
}
