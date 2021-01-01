using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using emotitron.Utilities.Networking;

#if PUN_2_OR_NEWER
using Photon.Pun;
#endif

public class LowFrameRate :
#if PUN_2_OR_NEWER

	MonoBehaviourPunCallbacks
{

	public enum SlowWhat { Both, Server, Client }
	public SlowWhat slowWhat = SlowWhat.Server;
	public int targetFrameRate = 10;


	public override void OnJoinedRoom()
	{
		base.OnJoinedRoom();


		bool slowthis = slowWhat == SlowWhat.Both ||
			(PhotonNetwork.IsMasterClient && slowWhat == SlowWhat.Server) ||
			(!PhotonNetwork.IsMasterClient && slowWhat == SlowWhat.Client);

		if (slowthis)
		{
			Application.targetFrameRate = targetFrameRate;
			QualitySettings.vSyncCount = 0;
		}
		else
		{
			Application.targetFrameRate = 100;
		}


	}

#else
	MonoBehaviour {
#endif
}
