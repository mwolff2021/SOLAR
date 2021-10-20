using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class navigationQuaternionMouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKey(KeyCode.UpArrow)){
             transform.Translate(transform.forward * Time.deltaTime);
       

            }
        if(Input.GetKey(KeyCode.RightArrow)){
		 transform.Translate(transform.right * Time.deltaTime);

            }
       if(Input.GetKey(KeyCode.LeftArrow)){
		 transform.Translate(-1*transform.right * Time.deltaTime);

            }


       if(Input.GetKey(KeyCode.A)){

             transform.Rotate(transform.up, -15.0f*Time.deltaTime);
    
      }
      if(Input.GetKey(KeyCode.D)){

              transform.Rotate(transform.up, 15.0f*Time.deltaTime);
      }

     if(Input.GetMouseButtonDown(0)){
	
        Vector3 worldPosition = new Vector3(0,0,0);
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, 0);
        if (plane.Raycast(ray, out distance))
        {
           worldPosition = ray.GetPoint(distance);
        }


         Vector3 relativePos = worldPosition - transform.position;
         Quaternion rotation = Quaternion.LookRotation(relativePos , Vector3.up);
         transform.rotation = rotation;
  	}


    }
}
