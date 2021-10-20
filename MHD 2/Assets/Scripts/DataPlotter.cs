using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataPlotter : MonoBehaviour
{

    // Name of the input file, no extension
    public string inputfile = "emission_data_scaled_8";


    // List for holding data from CSV reader
    private List<Dictionary<string, object>> pointList;
    // Indices for columns to be assigned
    public int columnX = 0;
    public int columnY = 1;
    public int columnZ = 2;
    public int columnEmit = 3;
    public GameObject PointPrefab;
    public GameObject PointHolder;
    public System.Random rnd = new System.Random(); 

    private int[,] colors = { { 0, 226, 255 }, { 0, 230, 255 }, { 0, 234, 255 }, { 0, 239, 255 }, { 0, 243, 255 }, { 0, 247, 255 }, { 0, 251, 255 }, { 0, 255, 255 }, { 0, 255, 245 }, { 0, 255, 234 }, { 0, 255, 224 }, { 0, 255, 213 }, { 0, 255, 203 }, { 0, 255, 192 }, { 0, 255, 181 }, { 0, 255, 169 }, { 0, 255, 158 }, { 0, 255, 146 }, { 0, 255, 135 }, { 0, 255, 123 }, { 0, 255, 110 }, { 0, 255, 97 }, { 0, 255, 84 }, { 0, 255, 70 }, { 0, 255, 56 }, { 0, 255, 40 }, { 0, 255, 23 }, { 0, 255, 0 }, { 9, 255, 0 }, { 15, 255, 0 }, { 21, 255, 0 }, { 26, 255, 0 }, { 31, 255, 0 }, { 36, 255, 0 }, { 40, 255, 0 }, { 45, 255, 0 }, { 49, 255, 0 }, { 54, 255, 0 } };
    private int colorIterator = 0;
    //private List<Color> colorsList = new List<Color>(); 

    // Use this for initialization
    void Start()
    {
        /*for (int colorIterator  = 0; colorIterator < 39; colorIterator++){
            colorsList.Add(new Color(colors[colorIterator, 0] / 255, colors[colorIterator, 1] / 255, colors[colorIterator, 2] / 255));
        }*/

        //Debug.Log(colorsList);
        // Set pointlist to results of function Reader with argument inputfile
        //Debug.Log("file name is" + inputfile);
        pointList = CSVReader.Read(inputfile);
        PointHolder = GameObject.Find("PointHolder");

        //Log to console
        //Debug.Log(pointList);
        //List<string> columnList = new List<string>(pointList[1].Keys);

        // Print number of keys (using .count)
        //Debug.Log("There are " + columnList.Count + " columns in CSV");

        //foreach (string key in columnList)
        //Debug.Log("Column name is " + key);
        //Debug.Log("started");
        // float maximum = FindMaxValue("a");
        //float maximumSqrt = (float)Math.Sqrt(maximum); 
        //Debug.Log(maximum);
        //float numPoints = 412690;
        //float numPoints = 4000;
        float numPoints = 406893;
        Instantiate(PointPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        for (var i = 0; i < numPoints; i++)
        {
            // Get value in poinList at ith "row", in "column" Name
            float x = System.Convert.ToSingle(pointList[i]["x"]);
            float y = System.Convert.ToSingle(pointList[i]["y"]);
            float z = System.Convert.ToSingle(pointList[i]["z"]);
            float opacity = System.Convert.ToSingle(pointList[i]["binned"]);
            //Debug.Log("opacity" + opacity.ToString());

            
            //instantiate the prefab with coordinates defined above
            

            // Instantiate as gameobject variable so that it can be manipulated within loop
            GameObject dataPoint = Instantiate(
                    PointPrefab,
                    new Vector3((float)(y/.64), (float)(x/1.92), (float)(z/1.92)),
                    Quaternion.identity);

            // Make child of PointHolder object, to keep points within container in hierarchy
            dataPoint.transform.parent = PointHolder.transform;
            //Debug.Log("opacity" + (a / maximumSqrt).ToString());
            //Debug.Log(i);
            //Debug.Log(a);
            /*  string dataPointName =
                   pointList[i]["x"] + " "
                   + pointList[i]["y"] + " "
                   + pointList[i]["z"];

              // Assigns name to the prefab
              dataPoint.transform.name = dataPointName;*/
            colorIterator = rnd.Next(0, 38);
            //colorIterator = 0;
            dataPoint.GetComponent<Renderer>().material.SetColor("_Color", new Color((float)colors[colorIterator, 0] / 255, (float)colors[colorIterator, 1] / 255, (float)colors[colorIterator, 2] / 255, (float)opacity));
            //dataPoint.GetComponent<Renderer>().material.SetColor("_Color", new Color((float)0.1, (float)0.2, (float)0.3));
            //dataPoint.GetComponent<Renderer>().material.SetColor("_Color", new Color((float)(1-1.0*opacity), (float)(1-1.0 * opacity), (float)(1-1.0 * opacity), opacity));
            //colorIterator = (colorIterator + 1)%38; 
            //Debug.Log(dataPoint.GetComponent<Renderer>().material.color); 
            /* // Assigns original values to dataPointName
         */

        }

    }

    private float FindMaxValue(string columnName)
    {
        //Debug.Log("in max value method"); 
        //set initial value to first value
        float maxValue = Convert.ToSingle(pointList[0][columnName]);

        //Loop through Dictionary, overwrite existing maxValue if new value is larger
        for (var i = 0; i < pointList.Count; i++)
        {
            if (maxValue < Convert.ToSingle(pointList[i][columnName]))
                maxValue = Convert.ToSingle(pointList[i][columnName]);
        }

        //Spit out the max value
        return maxValue;
    }
}