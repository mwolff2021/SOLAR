using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 


[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{

	public float acceleration = 50; // how fast you accelerate
	public float accSprintMultiplier = 4; // how much faster you go when "sprinting"
	public float lookSensitivity = 1; // mouse look sensitivity
	public float dampingCoefficient = 5; // how quickly you break to a halt after you stop your input
	public bool focusOnEnable = true; // whether or not to focus and lock cursor immediately on enable
  
    bool whichData = false; 

	Vector3 velocity; // current velocity

    static bool Focused
	{
		get => Cursor.lockState == CursorLockMode.Locked;
		set
		{
			Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.None;
			Cursor.visible = value == false;
		}
	}

	void OnEnable()
	{
		if (focusOnEnable) Focused = true;
	}

	void OnDisable() => Focused = false;

  

    public void ChangeScript()
    {
        Debug.Log("change script called"); 
       if (whichData)
        {
            Debug.Log("setting emissivity plotter to off"); 
            GameObject.Find("Particle System").GetComponent<EmissivityPlotter>().enabled = false;
            GameObject.Find("Particle System").GetComponent<DataPlotter>().enabled = true;
            whichData = !whichData; 
        }
        else {
            Debug.Log("setting emissivity plotter to on");
            GameObject.Find("Particle System").GetComponent<DataPlotter>().enabled = false;
            GameObject.Find("Particle System").GetComponent<EmissivityPlotter>().enabled = true;
            whichData = !whichData;
        }
 

    }

    void Update()
	{
		// Input
		if (Focused)
			UpdateInput();
		else if (Input.GetMouseButtonDown(0))
			Focused = true;
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("f key pressed");
            ChangeScript(); 
        }

		// Physics
		velocity = Vector3.Lerp(velocity, Vector3.zero, dampingCoefficient * Time.deltaTime);
		transform.position += velocity * Time.deltaTime;
	}

	void UpdateInput()
	{
		// Position
		velocity += GetAccelerationVector() * Time.deltaTime;

		// Rotation
		Vector2 mouseDelta = lookSensitivity * new Vector2(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));
		Quaternion rotation = transform.rotation;
		Quaternion horiz = Quaternion.AngleAxis(mouseDelta.x, Vector3.up);
		Quaternion vert = Quaternion.AngleAxis(mouseDelta.y, Vector3.right);
		transform.rotation = horiz * rotation * vert;

		// Leave cursor lock
		if (Input.GetKeyDown(KeyCode.Escape))
			Focused = false;
	}

	Vector3 GetAccelerationVector()
	{
		Vector3 moveInput = default;

		void AddMovement(KeyCode key, Vector3 dir)
		{
			if (Input.GetKey(key))
				moveInput += dir;
		}

		AddMovement(KeyCode.W, Vector3.forward);
		AddMovement(KeyCode.S, Vector3.back);
		AddMovement(KeyCode.D, Vector3.right);
		AddMovement(KeyCode.A, Vector3.left);
		AddMovement(KeyCode.Q, Vector3.up);
		AddMovement(KeyCode.E, Vector3.down);
		Vector3 direction = transform.TransformVector(moveInput.normalized);

		if (Input.GetKey(KeyCode.LeftShift))
			return direction * (acceleration * accSprintMultiplier); // "sprinting"
		return direction * acceleration; // "walking"
	}
}
