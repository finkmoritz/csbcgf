using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using emotitron.Utilities;
using emotitron.Utilities.Networking;

namespace Photon.Pun.Simple
{
	public class SimulateHealth : MonoBehaviour
	{
#if PUN_2_OR_NEWER
		Vitals vitals;
		public VitalNameType vitalType = new VitalNameType(VitalType.Health);
		public KeyCode AddHealthKey = KeyCode.Alpha6;
		public KeyCode RemHealthKey = KeyCode.Alpha7;
		public KeyCode DamagehKey = KeyCode.Alpha8;
		public float amount = 20f;

		// Use this for initialization
		void Start()
		{
			var iVitalsComp = GetComponentInChildren<IVitalsSystem>();


			if (iVitalsComp == null || !(iVitalsComp as SyncObject).PhotonView.IsMine)
				Destroy(this);
			else
				vitals = iVitalsComp.Vitals;
		}

		// Update is called once per frame
		void Update()
		{
			if (Input.GetKeyDown(AddHealthKey))
				vitals.ApplyCharges(vitalType, amount, false, false);

			if (Input.GetKeyDown(RemHealthKey))
				vitals.ApplyCharges(vitalType , - amount, false, true);

			if (Input.GetKeyDown(DamagehKey))
				vitals.ApplyCharges(amount, false, true);

		}
#endif
	}

}



