using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(SteamVR_Behaviour_Pose))]

public class NavigationBasicThrust : MonoBehaviour
{
    public Rigidbody NaviBase;
    public Vector3 ThrustDirection;
    public float ThrustForce;
    public bool ShowTrustMockup = true;
    public GameObject ThrustMockup;

    private SteamVR_Behaviour_Pose trackedObj;
    public SteamVR_Action_Boolean Button;

    GameObject attachedObject;
    Vector3 tempVector;

    private void Awake()
    {
        trackedObj = GetComponent<SteamVR_Behaviour_Pose>();
    }

    void FixedUpdate()
    {
        bool b = Button.GetState(trackedObj.inputSource);

        // add force
        if (b)
        {
            tempVector = Quaternion.Euler(ThrustDirection) * Vector3.forward;
            NaviBase.AddForce(transform.rotation * tempVector * ThrustForce);
            NaviBase.maxAngularVelocity = 2f;
        }

        NaviBase.transform.rotation = Quaternion.Slerp(NaviBase.transform.localRotation, Quaternion.Euler(NaviBase.transform.localRotation.x, transform.localRotation.y, NaviBase.transform.localRotation.z), 2.2f * Time.deltaTime);

        // show trust mockup
        if (ShowTrustMockup && ThrustMockup != null)
        {
            if (attachedObject == null && b)
            {
                attachedObject = (GameObject)GameObject.Instantiate(ThrustMockup, Vector3.zero, Quaternion.identity);
                attachedObject.transform.SetParent(this.transform, false);
                attachedObject.transform.Rotate(ThrustDirection);
            }
            else if (attachedObject != null && b)
            {
                Destroy(attachedObject);
            }
        }
    }
}