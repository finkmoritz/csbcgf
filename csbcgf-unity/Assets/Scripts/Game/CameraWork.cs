using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class CameraWork : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {
        float cameraPositionZ = -5f;
        if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            cameraPositionZ = 5f;
        }
        Camera.main.transform.position = new Vector3(0f, 5f, cameraPositionZ);
        Camera.main.transform.LookAt(new Vector3());
    }

    // Update is called once per frame
    void Update()
    {
    }
}
