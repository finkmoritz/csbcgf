using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class Hero : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetValue(string name, int value)
    {
        photonView.RPC("SetValueInternal", RpcTarget.All, name, value);
    }

    [PunRPC]
    private void SetValueInternal(string name, int value)
    {
        GetTextMesh(name).text = "" + value;
    }

    public void SetPlayerName(string name)
    {
        photonView.RPC("SetPlayerNameInternal", RpcTarget.All, name);
    }

    [PunRPC]
    private void SetPlayerNameInternal(string name)
    {
        GetTextMesh("PlayerName").text = name;
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
