using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject Cube0;

    public GameObject Cube1;

    public GameObject Cube2;

    public GameObject Cube3;
    // Start is called before the first frame update
    void Start()
    {
        Cube0 = GameObject.Find("Cube0");
        Cube1 = GameObject.Find("Cube1");
        Cube2 = GameObject.Find("Cube2");
        Cube3 = GameObject.Find("Cube3");

        Cube0.GetComponent<Renderer>().material.SetColor("_Color", new Color((float)1.0, (float)1.0, (float)1.0, (float)0.1));
        Cube1.GetComponent<Renderer>().material.SetColor("_Color", new Color((float)1.0, (float)1.0, (float)1.0, (float)0.01));
        Cube2.GetComponent<Renderer>().material.SetColor("_Color", new Color((float)1.0, (float)1.0, (float)1.0, (float)0.001));
        Cube3.GetComponent<Renderer>().material.SetColor("_Color", new Color((float)1.0, (float)1.0, (float)1.0, (float)0.0001));
    }

}