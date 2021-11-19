using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EmissivityPlotter: MonoBehaviour
{

    // Indices for columns to be assigned
    public int columnX = 0;
    public int columnY = 1;
    public int columnZ = 2;
    public int columnEmit = 3;
    public string opacityColumn = "a";
    public System.Random rnd = new System.Random();
    private int[,] colors = { { 0, 226, 255 }, { 0, 230, 255 }, { 0, 234, 255 }, { 0, 239, 255 }, { 0, 243, 255 }, { 0, 247, 255 }, { 0, 251, 255 }, { 0, 255, 255 }, { 0, 255, 245 }, { 0, 255, 234 }, { 0, 255, 224 }, { 0, 255, 213 }, { 0, 255, 203 }, { 0, 255, 192 }, { 0, 255, 181 }, { 0, 255, 169 }, { 0, 255, 158 }, { 0, 255, 146 }, { 0, 255, 135 }, { 0, 255, 123 }, { 0, 255, 110 }, { 0, 255, 97 }, { 0, 255, 84 }, { 0, 255, 70 }, { 0, 255, 56 }, { 0, 255, 40 }, { 0, 255, 23 }, { 0, 255, 0 }, { 9, 255, 0 }, { 15, 255, 0 }, { 21, 255, 0 }, { 26, 255, 0 }, { 31, 255, 0 }, { 36, 255, 0 }, { 40, 255, 0 }, { 45, 255, 0 }, { 49, 255, 0 }, { 54, 255, 0 } };
    public List<ParticleSystem.Particle[]> emissivity;
    private ParticleSystem particleSystem; 
    private int numPoints = 13180;
    private int updateCounter = 0;
    private List<string> filenames; 


    void Update()
    {

        switch (updateCounter)
        {
            case 100:
                particleSystem.SetParticles(emissivity[1]);
                break;
            case 200:
                particleSystem.SetParticles(emissivity[2]);
                break;
            case 220:
                particleSystem.SetParticles(emissivity[3]);
                break;
            case 240:
                particleSystem.SetParticles(emissivity[4]);
                break;
            case 260:
                particleSystem.SetParticles(emissivity[5]);
                break;
            case 270:
                particleSystem.SetParticles(emissivity[6]);
                break;
            case 275:
                particleSystem.SetParticles(emissivity[7]);
                break;
            case 280:
                particleSystem.SetParticles(emissivity[8]);
                break;
            case 290:
                particleSystem.SetParticles(emissivity[9]);
                break;
            case 300:
                particleSystem.SetParticles(emissivity[10]);
                break;
            case 400:
                particleSystem.SetParticles(emissivity[11]);
                break;
            case 460:
                updateCounter = 0;
                Debug.Log("resetting update counter"); 
                break; 

            default:
                break; 
        }

         updateCounter++;
    }

    private ParticleSystem.Particle[] generateParticlesFromFile(string filename)
    {
        Debug.Log("calling generateParticlesFromFile"); 
        List<Dictionary<string, object>> pointList = generatePointList(filename);
        float maxValue = FindMaxValue("a", pointList);
        Color[] colors = generateColorList(pointList, maxValue);
        ParticleSystem.Particle[] particles = generateParticles(pointList, colors);
        return particles;
    }

    private Color[] generateColorList(List<Dictionary<string, object>> pointList, float maxValue)
    {
        Debug.Log("calling generateColorList"); 
        Color[] particleColors = new Color[numPoints];
        int colorIterator = 0;

        for (var i = 0; i < numPoints; i++)
        {
            colorIterator = rnd.Next(0, 38);
            float opacity = (100000.0f * System.Convert.ToSingle(pointList[i][opacityColumn]) / maxValue);
            Debug.Log("opacity: " + opacity); 
            Color particleColor = new Color((float)colors[colorIterator, 0] / 255,
                            (float)colors[colorIterator, 1] / 255,
                            (float)colors[colorIterator, 2] / 255,
                            (float)opacity);
            particleColors[i] = particleColor; 
        }
        return particleColors;
    }


    private ParticleSystem.Particle[] generateParticles(List<Dictionary<string, object>> pointList, Color[] particleColors)
    {
        Debug.Log("calling generateParticles"); 
        ParticleSystem.Particle[] ps = new ParticleSystem.Particle[numPoints];
        for (var i = 0; i < numPoints; i++)
        {
            float x = System.Convert.ToSingle(pointList[i]["x"]);
            float y = System.Convert.ToSingle(pointList[i]["y"]);
            float z = System.Convert.ToSingle(pointList[i]["z"]);
            ps[i].startColor = particleColors[i];
            Debug.Log("x: " + x + "\n y:" + y + "\n z:" + z + "\n color:" + particleColors[i]); 
            ps[i].position = new Vector3(x, y, z);
            ps[i].size = 20.0f;
        }
        return ps;
    }

    private List<Dictionary<string, object>> generatePointList(string inputFile)
    {
        Debug.Log("calling generatePointList"); 
        List<Dictionary<string, object>> pointList = new List<Dictionary<string, object>>();
        pointList = CSVReader.Read(inputFile);
        Debug.Log("point list: " + pointList);
        Debug.Log("point list 0: " + pointList[0].ToString());
        return pointList;
    }

    void Start()
    {
        filenames = new List<string> { "emiss_2700", "emiss_2800", "emiss_2900", "emiss_2920", 
            "emiss_2940", "emiss_2960", "emiss_2970", "emiss_2975",
            "emiss_2980", "emiss_2990", "emiss_3000", "emiss_3100"};
        emissivity = new List<ParticleSystem.Particle[]>(); 
        foreach (var name in filenames){
            ParticleSystem.Particle[] currentParticles = generateParticlesFromFile(name);
            Debug.Log("generated particles: " + name);
            emissivity.Add(currentParticles); 
        }
        particleSystem = gameObject.GetComponent<ParticleSystem>();
        Debug.Log("setting particle system");
        Debug.Log("emissivity 0: " + emissivity[0]);
        Debug.Log("emissivity 0 0: " + emissivity[0][0]);
        particleSystem.SetParticles(emissivity[0], numPoints);
    }

    private float FindMaxValue(string columnName, List<Dictionary<string, object>> pointList)
    {
        float maxValue = Convert.ToSingle(pointList[0][columnName]);
        for (var i = 0; i < pointList.Count; i++)
        {
            if (maxValue < Convert.ToSingle(pointList[i][columnName]))
                maxValue = Convert.ToSingle(pointList[i][columnName]);
        }
        return maxValue;
    }
}

