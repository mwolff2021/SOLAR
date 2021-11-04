using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(SteamVR_Behaviour_Pose))]

public class NavigationBasicThrust : MonoBehaviour
{
    public Rigidbody NaviBase;
    public Vector3 ThrustDirection;
    public float ThrustForce;

    private SteamVR_Behaviour_Pose trackedObj;
    public SteamVR_Action_Boolean Button;

    GameObject attachedObject;
    Vector3 tempVector;

    private void Awake()
    {
        Debug.Log("awake");
        trackedObj = GetComponent<SteamVR_Behaviour_Pose>();
    }

    void FixedUpdate()
    {
        Debug.Log("fixed update");
        bool b = Button.GetState(trackedObj.inputSource);

        // add force
        if (b)
        {
            Debug.Log("pulled trigger ,woof");
            tempVector = Quaternion.Euler(ThrustDirection) * Vector3.forward;
            Debug.Log(tempVector.ToString());
            NaviBase.AddForce(transform.rotation * tempVector * ThrustForce, ForceMode.Acceleration);
            Debug.Log("added force: " + (transform.rotation * tempVector * ThrustForce).ToString());
            NaviBase.maxAngularVelocity = 2f;
        }

        NaviBase.transform.rotation = Quaternion.Slerp(NaviBase.transform.localRotation, Quaternion.Euler(NaviBase.transform.localRotation.x, transform.localRotation.y, NaviBase.transform.localRotation.z), 2.2f * Time.deltaTime);

    }
}