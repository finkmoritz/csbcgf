using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Slot : MonoBehaviourPun
{
    public int playerIndex;
    public int slotIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    public void Init(int playerIndex, int slotIndex)
    {
        this.playerIndex = playerIndex;
        this.slotIndex = slotIndex;
    }
}
