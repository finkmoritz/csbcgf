
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if PUN_2_OR_NEWER
using Photon.Pun;
#endif

namespace Photon.Pun.Example
{
    public class TakeOwnership : MonoBehaviour
    {

        public KeyCode keycode = KeyCode.C;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(keycode))
                TransferOwner();
        }

        public void TransferOwner()
        {
#if PUN_2_OR_NEWER
            GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer.ActorNumber);
#endif
        }
    }

}
