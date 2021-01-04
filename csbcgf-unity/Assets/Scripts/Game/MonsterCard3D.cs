using System;
using csbcgf;
using TMPro;
using UnityEngine;

public class MonsterCard3D : MonsterCard
{
    public GameObject gameObject;

    public MonsterCard3D(int mana, int attack, int life)
        : base(mana, attack, life)
    {
    }

    public void SetMana(int mana) {
        ManaValue = mana;
        TextMeshPro textMesh = gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>();
        textMesh.text = "" + mana;
    }

    public void SetAttack(int attack)
    {
        ManaValue = attack;
        TextMeshPro textMesh = gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshPro>();
        textMesh.text = "" + attack;
    }

    public void SetLife(int life)
    {
        ManaValue = life;
        TextMeshPro textMesh = gameObject.transform.GetChild(3).gameObject.GetComponent<TextMeshPro>();
        textMesh.text = "" + life;
    }
}
