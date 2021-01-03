using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class PlayerName : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private bool isMyPlayer;

    // Start is called before the first frame update
    void Start()
    {
        Text playerNameComponent = GetComponent<Text>();
        if(isMyPlayer)
        {
            playerNameComponent.text = PhotonNetwork.NickName;
        }
        else if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            playerNameComponent.text = PhotonNetwork.PlayerListOthers[0].NickName;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        if(!isMyPlayer)
        {
            Text playerNameComponent = GetComponent<Text>();
            playerNameComponent.text = newPlayer.NickName;
        }
    }
}
