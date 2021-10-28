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
   
    //public GameObject solar; 
    public System.Random rnd = new System.Random(); 

    private int[,] colors = { { 0, 226, 255 }, { 0, 230, 255 }, { 0, 234, 255 }, { 0, 239, 255 }, { 0, 243, 255 }, { 0, 247, 255 }, { 0, 251, 255 }, { 0, 255, 255 }, { 0, 255, 245 }, { 0, 255, 234 }, { 0, 255, 224 }, { 0, 255, 213 }, { 0, 255, 203 }, { 0, 255, 192 }, { 0, 255, 181 }, { 0, 255, 169 }, { 0, 255, 158 }, { 0, 255, 146 }, { 0, 255, 135 }, { 0, 255, 123 }, { 0, 255, 110 }, { 0, 255, 97 }, { 0, 255, 84 }, { 0, 255, 70 }, { 0, 255, 56 }, { 0, 255, 40 }, { 0, 255, 23 }, { 0, 255, 0 }, { 9, 255, 0 }, { 15, 255, 0 }, { 21, 255, 0 }, { 26, 255, 0 }, { 31, 255, 0 }, { 36, 255, 0 }, { 40, 255, 0 }, { 45, 255, 0 }, { 49, 255, 0 }, { 54, 255, 0 } };
    private int colorIterator = 0;
    private ParticleSystem particles;
    private ParticleSystem.Particle[] ps;
    int numPoints = 406893;
    //int numPoints = 40000; 
    // Use this for initialization
    void Start()
    {
        particles = gameObject.GetComponent<ParticleSystem>();

        ps = new ParticleSystem.Particle[numPoints]; 

        pointList = CSVReader.Read(inputfile);
       
       
        //Instantiate(PointPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        for (var i = 0; i < numPoints; i++)
        {
            Debug.Log("i: " + i.ToString());
            // Get value in poinList at ith "row", in "column" Name
            float x = System.Convert.ToSingle(pointList[i]["x"]);
            float y = System.Convert.ToSingle(pointList[i]["y"]);
            float z = System.Convert.ToSingle(pointList[i]["z"]);
            float opacity = System.Convert.ToSingle(pointList[i]["binned"]);
            colorIterator = rnd.Next(0, 38);
            Color particleColor = new Color((float)colors[colorIterator, 0] / 255,
                (float)colors[colorIterator, 1] / 255, 
                (float)colors[colorIterator, 2] / 255, 
                (float)opacity);
            ps[i].startColor = particleColor;
            ps[i].position = new Vector3(x, y, z);
            ps[i].size = 20.0f; 

        }

        particles.SetParticles(ps, numPoints); 
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