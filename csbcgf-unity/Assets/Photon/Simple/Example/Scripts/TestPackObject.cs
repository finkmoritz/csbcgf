using UnityEngine;
using Photon.Pun.Simple;
using System.Collections.Generic;

using Photon.Pun;
using Photon.Compression;

namespace emotitron
{
    public enum TestEnum { None, Some, SomeMore, All }

    ///// Structs only work if unsafe is enabled
    //[PackObject]
    //public struct TestStruct2
    //{

    //	[PackRangedInt(-20, 100, applyCallback = "HealthCallback", snapshotCallback = "HealthSnapshot")]
    //	public int myHealth;


    //	[PackRangedInt(0, 3)]
    //	public TestEnum myTestEnum;

    //	[Pack]
    //	public float floaterBloater;

    //	[Pack]
    //	public float floaterBloater2;

    //	public void HealthSnapshot(int snap, int targ)
    //	{

    //	}

    //	public void HealthCallback(int newvalue, int oldvalue)
    //	{
    //		// Put callback stuff here
    //	}
    //}


    [PackObject(defaultInclusion = DefaultPackInclusion.Explicit)]
	public class TestPackObject : NetComponent
		, IOnPreSimulate
        , IOnInterpolate
		//, IOnPostSimulate
	{

		[SyncHalfFloat(
			snapshotCallback = "SnapshotHook",
			applyCallback = "RotationHook", 
			setValueTiming = SetValueTiming.BeforeCallback, 
			interpolate = true, 
			keyRate = KeyRate.Every
			)]
		public float rotation;

		//[Pack]
		//public Vector3 v5;
  //      public Vector2 v2;

  //      [Pack]
  //      public PhotonView pv2;
        //private PhotonView pv3;
        //[Pack]
        //public List<int> bytelist = new List<int>(3) { 11, 22, 33 };

        [SyncRangedInt(-1, 2)]
		public int intoroboto;

		//[Pack]
		//public TestStruct2 teststruct;


		public void RotationHook(float newrot, float oldrot)
		{
            //Debug.Log("Hook  " + NetMaster.PreviousFrameId + ": " + oldrot + " ---  " + NetMaster.CurrentFrameId + ": " + newrot);
            //if (!PhotonView.IsMine)
            {
                transform.localEulerAngles = new Vector3(0, rotation, 0);

            }
        }

		public void SnapshotHook(float snap, float targ)
		{
			//Debug.Log("Snap " +  NetMaster.PreviousFrameId+ ": "+ snap + " ---  " +  NetMaster.CurrentFrameId + ": " + targ);
		}

		public void OnPreSimulate(int frameId, int subFrameId)
		{
            // Rotate when isMine
			if (photonView.IsMine)
			{
				rotation = (Mathf.Sin(Time.time) + .5f) * 120f; // (rotation + 5f);
				//int revs = (int)(rotation / 360);
				//rotation -= (revs * 360);
				transform.localEulerAngles = new Vector3(0, rotation, 0);
			}
		}

        public 
		//public void OnPostSimulate(int frameId, int subFrameId, bool isNetTick)
		//{
  //          Debug.Log("Post: " + rotation + " " + IsMine);

  //          if (!PhotonView.IsMine)
  //          {
  //              transform.localEulerAngles = new Vector3(0, rotation, 0);

  //          }
  //      }
		// Update is called once per frame
		void FixedUpdate()
		{
			
		}

		private void Update()
		{
            
        }

        public bool OnInterpolate(int snapFrameId, int targFrameId, float t)
        {
            //Debug.Log(photonView.ViewID + " Update " + IsMine + " " + rotation);
            if (!PhotonView.IsMine)
            {
                transform.localEulerAngles = new Vector3(0, rotation, 0);
            }
            return true;
        }
    }

}