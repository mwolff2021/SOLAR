/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class DataPlotter : MonoBehaviour
{

    // Name of the input file, no extension
    public string inputfile = "emission_data_scaled_8";

    // List for holding data from CSV reader
    private List<Dictionary<string, object>> pointList, pointList2;
    // Indices for columns to be assigned
    public int columnX = 0;
    public int columnY = 1;
    public int columnZ = 2;
    public int columnEmit = 3;   
    public string opacityColumn = "binned";
   
    //public GameObject solar; 
    public System.Random rnd = new System.Random(); 

    private int[,] colors = { { 0, 226, 255 }, { 0, 230, 255 }, { 0, 234, 255 }, { 0, 239, 255 }, { 0, 243, 255 }, { 0, 247, 255 }, { 0, 251, 255 }, { 0, 255, 255 }, { 0, 255, 245 }, { 0, 255, 234 }, { 0, 255, 224 }, { 0, 255, 213 }, { 0, 255, 203 }, { 0, 255, 192 }, { 0, 255, 181 }, { 0, 255, 169 }, { 0, 255, 158 }, { 0, 255, 146 }, { 0, 255, 135 }, { 0, 255, 123 }, { 0, 255, 110 }, { 0, 255, 97 }, { 0, 255, 84 }, { 0, 255, 70 }, { 0, 255, 56 }, { 0, 255, 40 }, { 0, 255, 23 }, { 0, 255, 0 }, { 9, 255, 0 }, { 15, 255, 0 }, { 21, 255, 0 }, { 26, 255, 0 }, { 31, 255, 0 }, { 36, 255, 0 }, { 40, 255, 0 }, { 45, 255, 0 }, { 49, 255, 0 }, { 54, 255, 0 } };
    private int colorIterator = 0;
    private ParticleSystem particles;
    private ParticleSystem.Particle[] ps;
    ParticleSystem.Particle[] ps2; 
    private Color[] particleColors;
    private Color[] particleColors2;
    int numPoints = 406893;
    //int numPoints = 40000;
    //int numPoints = 13180;
    int updateCounter = 0;
    int updateMax = 1200; 

    // Use this for initialization
    void Start()
    {
        particles = gameObject.GetComponent<ParticleSystem>();

        //particles.GetParticles(ps);
        ps = new ParticleSystem.Particle[numPoints];
        //ps2 = new ParticleSystem.Particle[numPoints];


        var col = particles.colorOverLifetime;
        col.enabled = true;

        particleColors = new Color[numPoints];
        particleColors2 = new Color[numPoints];

        pointList = CSVReader.Read(inputfile);
        //pointList2 = CSVReader.Read(inputfile2);

        //float maxValue = FindMaxValue("");
        //Debug.Log(maxValue);
       
        //Instantiate(PointPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        for (var i = 0; i < numPoints; i++)
        {
            //Debug.Log("i: " + i.ToString());
            // Get value in poinList at ith "row", in "column" Name
            float x = System.Convert.ToSingle(pointList[i]["x"]);
            float y = System.Convert.ToSingle(pointList[i]["y"]);
            float z = System.Convert.ToSingle(pointList[i]["z"]);
            float opacity = System.Convert.ToSingle(pointList[i][opacityColumn]);
            //float opacity = (System.Convert.ToSingle(pointList[i][opacityColumn])/maxValue);
            //Debug.Log("opacity" + opacity);
            colorIterator = rnd.Next(0, 38);
            Color particleColor = new Color((float)colors[colorIterator, 0] / 255,
                (float)colors[colorIterator, 1] / 255, 
                (float)colors[colorIterator, 2] / 255, 
                (float)opacity);
            ps[i].startColor = particleColor;
            particleColors[i] = particleColor; 
            ps[i].position = new Vector3(x, y, z);
            ps[i].size = 20.0f;

            //ps[i].remainingLifetime = 20;
            //visually recvealing new parts of the data 
            //basically just trying to show time varying data
        }

          
            //ps[i].remainingLifetime = 20;
            //visually recvealing new parts of the data 
            //basically just trying to show time varying data
        //}


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
}*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class DataPlotter : MonoBehaviour
{

    public string inputfile = "temp";
    public string inputfile2 = "temp_28";

    // List for holding data from CSV reader
    private List<Dictionary<string, object>> pointList, pointList2;
    // Indices for columns to be assigned
    public int columnX = 0;
    public int columnY = 1;
    public int columnZ = 2;
    public int columnEmit = 3;
    public string opacityColumn = "a";// binned";//"a";
  

    //public GameObject solar; 
    public System.Random rnd = new System.Random();

    private int[,] colors = { { 0, 226, 255 }, { 0, 230, 255 }, { 0, 234, 255 }, { 0, 239, 255 }, { 0, 243, 255 }, { 0, 247, 255 }, { 0, 251, 255 }, { 0, 255, 255 }, { 0, 255, 245 }, { 0, 255, 234 }, { 0, 255, 224 }, { 0, 255, 213 }, { 0, 255, 203 }, { 0, 255, 192 }, { 0, 255, 181 }, { 0, 255, 169 }, { 0, 255, 158 }, { 0, 255, 146 }, { 0, 255, 135 }, { 0, 255, 123 }, { 0, 255, 110 }, { 0, 255, 97 }, { 0, 255, 84 }, { 0, 255, 70 }, { 0, 255, 56 }, { 0, 255, 40 }, { 0, 255, 23 }, { 0, 255, 0 }, { 9, 255, 0 }, { 15, 255, 0 }, { 21, 255, 0 }, { 26, 255, 0 }, { 31, 255, 0 }, { 36, 255, 0 }, { 40, 255, 0 }, { 45, 255, 0 }, { 49, 255, 0 }, { 54, 255, 0 } };
    private int colorIterator = 0;
    private ParticleSystem particles;
    private ParticleSystem.Particle[] ps;
    ParticleSystem.Particle[] ps2;
    private Color[] particleColors;
    private Color[] particleColors2;
    //int numPoints = 406893;
    //int numPoints = 40000;
    int numPoints = 13180;
    int updateCounter = 0;
    int updateMax = 1200;
    int current = 1; 

    void Update()
    {
        /*Debug.Log(updateCounter);
        if (updateCounter % 60 == 0) {
            particles = gameObject.GetComponent<ParticleSystem>();
            particles.GetParticles(ps);
        for (int i = 0; i < numPoints; i++)
        {
            ps[i].startColor = Color.Lerp(Color.clear, particleColors[i], particleColors[i].a);
        }
             
            particles.SetParticles(ps, numPoints);
        }
        updateCounter++;*/
        
        if (updateCounter % 10 == 0)
        {
            if (current == 1)
            {
                Debug.Log("switching now");
                particles.SetParticles(ps2, numPoints);
                current = 2; 
            }
            else if (current == 2)
            {
                Debug.Log("switching now");
                particles.SetParticles(ps, numPoints);
                current = 1;
            }

           
        }
        updateCounter++;

    }

    // Use this for initialization
    void Start()
    {
        particles = gameObject.GetComponent<ParticleSystem>();

        //particles.GetParticles(ps);
        ps = new ParticleSystem.Particle[numPoints];
        ps2 = new ParticleSystem.Particle[numPoints];


        var col = particles.colorOverLifetime;
        col.enabled = true;

        particleColors = new Color[numPoints];
        particleColors2 = new Color[numPoints];

        pointList = CSVReader.Read(inputfile);
        pointList2 = CSVReader.Read(inputfile2);

        //float maxValue = FindMaxValue("");
        //Debug.Log(maxValue);

        //Instantiate(PointPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        for (var i = 0; i < numPoints; i++)
        {
            //Debug.Log("i: " + i.ToString());
            // Get value in poinList at ith "row", in "column" Name
            float x = System.Convert.ToSingle(pointList[i]["x"]);
            float y = System.Convert.ToSingle(pointList[i]["y"]);
            float z = System.Convert.ToSingle(pointList[i]["z"]);
            float opacity = System.Convert.ToSingle(pointList[i][opacityColumn]);
            //float opacity = (System.Convert.ToSingle(pointList[i][opacityColumn])/maxValue);
            //Debug.Log("opacity" + opacity);
            colorIterator = rnd.Next(0, 38);
            Color particleColor = new Color((float)colors[colorIterator, 0] / 255,
                (float)colors[colorIterator, 1] / 255,
                (float)colors[colorIterator, 2] / 255,
                (float)opacity);
            ps[i].startColor = particleColor;
            particleColors[i] = particleColor;
            ps[i].position = new Vector3(x, y, z);
            ps[i].size = 20.0f;
        }

        for (var i = 0; i < numPoints; i++)
        {
            //Debug.Log("i: " + i.ToString());
            // Get value in poinList at ith "row", in "column" Name
            float x = System.Convert.ToSingle(pointList2[i]["x"]);
            float y = System.Convert.ToSingle(pointList2[i]["y"]);
            float z = System.Convert.ToSingle(pointList2[i]["z"]);
            float opacity = (System.Convert.ToSingle(pointList2[i][opacityColumn]));
            //Debug.Log("opacity" + opacity);
            colorIterator = rnd.Next(0, 38);
            Color particleColor = new Color((float) 255 / 255,
                (float)colors[colorIterator, 1] / 255,
                (float)colors[colorIterator, 2] / 255,
                (float)opacity);
            ps2[i].startColor = particleColor;
            particleColors2[i] = particleColor;
            ps2[i].position = new Vector3(x, y, z);
            ps2[i].size = 20.0f;
        

        /*Gradient grad = new Gradient(); 
        grad.SetKeys(new GradientColorKey[] { new GradientColorKey(particleColor, 0.0f), new GradientColorKey(particleColor, 1.0f)}, new GradientAlphaKey[] { new GradientAlphaKey(0.0f, 0.0f), new GradientAlphaKey(opacity, 1.0f) });
        ps[i].color = grad; */
        //ps[i].remainingLifetime = 20;
        //visually recvealing new parts of the data 
        //basically just trying to show time varying data
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