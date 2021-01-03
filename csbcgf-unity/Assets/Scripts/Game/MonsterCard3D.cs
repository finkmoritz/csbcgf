using System;
using csbcgf;
using UnityEngine;

public class MonsterCard3D : MonsterCard
{
    public GameObject gameObject;

    public MonsterCard3D(int mana, int attack, int life)
        : base(mana, attack, life)
    {
    }
}
