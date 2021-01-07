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
        float cameraRotationY = 0f;
        if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            cameraPositionZ = 5f;
            cameraRotationY = 180f;
        }
        Camera.main.transform.position = new Vector3(0f, 7f, cameraPositionZ);
        Camera.main.transform.rotation = Quaternion.Euler(60.0f, cameraRotationY, 0f);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
