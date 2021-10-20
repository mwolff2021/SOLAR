using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class navigationQuaternionKeys3 : MonoBehaviour
{
    //Camera rCam, lCam, mCam;
    //GameObject cameraHolder;
    //GameObject head, body; 
    Camera camera; 

    public Quaternion rotTmp;
    // Start is called before the first frame update
    void Start()
    {
        /*Input.gyro.enabled = true; 

        GameObject tempM = (GameObject.Find("Camera"));
        camera = (tempM.GetComponent<Camera>() as Camera);
        camera.enabled = true;
        */
    }
    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
    // Update is called once per frame
    void Update()
    {
        Quaternion yaw = Quaternion.AngleAxis(0.0f*Time.deltaTime, transform.up);
        Quaternion pitch = Quaternion.AngleAxis(0.0f*Time.deltaTime, transform.right);
        Quaternion roll = Quaternion.AngleAxis(0.0f*Time.deltaTime, transform.forward);

        if (Input.GetKey(KeyCode.A)){
                yaw = Quaternion.AngleAxis(-15.0f * Time.deltaTime, transform.up);
        }
      if(Input.GetKey(KeyCode.D)){
                yaw = Quaternion.AngleAxis(15.0f * Time.deltaTime, transform.up);
        }
	  if (Input.GetKey(KeyCode.W)){
                pitch = Quaternion.AngleAxis(-15.0f * Time.deltaTime, transform.right);
        }
	  if (Input.GetKey(KeyCode.S)){
                pitch = Quaternion.AngleAxis(15.0f * Time.deltaTime, transform.right);
        }
	  if (Input.GetKey(KeyCode.Q)){
                roll = Quaternion.AngleAxis(-15.0f * Time.deltaTime, transform.forward);
        }
	  
	  if (Input.GetKey(KeyCode.Z)){
                roll = Quaternion.AngleAxis(15.0f * Time.deltaTime, transform.forward);
        }

        //camera.transform.rotation = GyroToUnity(Input.gyro.attitude);
       transform.rotation = roll * pitch * yaw * transform.rotation;
        //head and body move forward, backward, left, right together
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(transform.forward * 100.0f *Time.deltaTime);
            //Debug.Lmmmog("Up");
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(transform.forward * -100.0f * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(transform.right * -100.0f * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(transform.right * 100.0f * Time.deltaTime);
        }


        if (Input.GetKey(KeyCode.N))
        {
            //since this is an underwater environment, upward movement is not constrained
            transform.Translate(transform.up * 100.0f * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.M))
        {
                transform.Translate(transform.up * -100.0f * Time.deltaTime);
        }


    }
}
