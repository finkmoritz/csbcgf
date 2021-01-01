using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun.Simple;
using UnityEngine.UI;

public class TickMover : MonoBehaviour, IOnPostSimulate
{
	private Vector3 rotationPerTick;
	private TextMesh tickText;

	// Use this for initialization
	void Awake ()
	{
        NetMasterCallbacks.RegisterCallbackInterfaces(this, true);
		rotationPerTick = new Vector3(0, 0, 360f * (Time.fixedDeltaTime * TickEngineSettings.sendEveryXTick));

		tickText = GetComponentInChildren<TextMesh>();

		if (!tickText)
		tickText = GetComponentInParent<TextMesh>();

		if (tickText)
			tickText.text = "";
	}

	private void OnDestroy()
	{
        NetMasterCallbacks.RegisterCallbackInterfaces(this, false, true);
	}

	public void OnPostSimulate(int frameId, int subFrameId, bool isNetTick)
	{
		if (!isNetTick)
			return;

		transform.eulerAngles -= rotationPerTick;

		if (tickText)
			tickText.text = frameId.ToString();
	}
}
