﻿using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class Card3D : MonoBehaviourPun
{
    public Vector3? targetPosition;
    public Quaternion? targetRotation;

    public const float Speed = 0.05f;
    public const float RotationSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(targetPosition != null)
        {
            Vector3 diff = (Vector3)(targetPosition - transform.position);
            if(diff.magnitude < Speed)
            {
                transform.position = (Vector3)targetPosition;
                targetPosition = null;
            } else
            {
                transform.position += Speed * diff.normalized;
            }
        }

        if(targetRotation != null)
        {
            float diff = Quaternion.Angle((Quaternion)targetRotation, transform.rotation);
            if(diff < RotationSpeed)
            {
                transform.rotation = (Quaternion)targetRotation;
                targetRotation = null;
            } else
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, (Quaternion)targetRotation, RotationSpeed);
            }
        }
    }

    public void SetValue(string name, int value)
    {
        photonView.RPC("SetValueInternal", RpcTarget.All, name, value);
    }

    [PunRPC]
    private void SetValueInternal(string name, int value)
    {
        TextMeshPro textMesh = GetTextMesh(name);
        textMesh.text = "" + value;
    }

    private TextMeshPro GetTextMesh(string name)
    {
        foreach (Transform child in transform)
        {
            if (child.name == name)
            {
                return child.gameObject.GetComponent<TextMeshPro>();
            }
        }
        return null;
    }
}