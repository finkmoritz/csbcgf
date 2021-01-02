using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class PlayerName : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {
        Text playerNameComponent = GetComponent<Text>();
        playerNameComponent.text = PhotonNetwork.NickName;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
