using UnityEngine;

public class JetpackBasic : MonoBehaviour
{
	private SteamVR_TrackedController _controller;

	private void OnEnable()
	{
		_controller = GetComponent<SteamVR_TrackedController>();
	
	}

	private void OnDisable()
	{
		_controller.TriggerClicked -= HandleTriggerClicked;
	}

	private void HandleTriggerClicked(object sender, ClickedEventArgs e)
	{
		Debug.Log("trigger clicked"); 
	}

}