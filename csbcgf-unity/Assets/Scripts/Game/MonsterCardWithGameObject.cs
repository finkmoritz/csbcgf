using System;
using csbcgf;
using TMPro;
using UnityEngine;

public class MonsterCardWithGameObject : MonsterCard
{
    public GameObject gameObject;

    public MonsterCardWithGameObject(int mana, int attack, int life)
        : base(mana, attack, life)
    {
    }
}
