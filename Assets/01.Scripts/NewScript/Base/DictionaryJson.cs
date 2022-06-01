using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class DictionaryJson<Tkey, Tvalue>
{
    [SerializeField] private Tkey key;
    [SerializeField] private Tvalue value;
    public Tkey Key { get { return key; } set { key = value; } }
    public Tvalue Value { get { return value; } set { this.value = value; } }
}
