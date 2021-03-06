using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Dat: MonoBehaviour
{

    // Indices for columns to be assigned
    public int columnX = 0;
    public int columnY = 1;
    public int columnZ = 2;
    public int columnEmit = 3;
    public string opacityColumn = "a";
    public System.Random rnd = new System.Random();
    private int[,] colors = { { 0, 226, 255 }, { 0, 230, 255 }, { 0, 234, 255 }, { 0, 239, 255 }, { 0, 243, 255 }, { 0, 247, 255 }, { 0, 251, 255 }, { 0, 255, 255 }, { 0, 255, 245 }, { 0, 255, 234 }, { 0, 255, 224 }, { 0, 255, 213 }, { 0, 255, 203 }, { 0, 255, 192 }, { 0, 255, 181 }, { 0, 255, 169 }, { 0, 255, 158 }, { 0, 255, 146 }, { 0, 255, 135 }, { 0, 255, 123 }, { 0, 255, 110 }, { 0, 255, 97 }, { 0, 255, 84 }, { 0, 255, 70 }, { 0, 255, 56 }, { 0, 255, 40 }, { 0, 255, 23 }, { 0, 255, 0 }, { 9, 255, 0 }, { 15, 255, 0 }, { 21, 255, 0 }, { 26, 255, 0 }, { 31, 255, 0 }, { 36, 255, 0 }, { 40, 255, 0 }, { 45, 255, 0 }, { 49, 255, 0 }, { 54, 255, 0 } };
    private ParticleSystem particles;
    private int numPoints = 4000;
    private int updateCounter = 0;


    void Update()
    {

        /* if (updateCounter == 600)
         {

             Debug.Log("switching to second frame");
             ParticleSystem.Particle[] ps2 = generateParticlesFromFile("temp_28");
             particles.SetParticles(ps2, numPoints);
         }
         else if (updateCounter == 1200)
         {
         }

         updateCounter++;*/
    }

    private ParticleSystem.Particle[] generateParticlesFromFile(string filename)
    {
        List<Dictionary<string, object>> pointList = generatePointList(filename);
        float maxValue = FindMaxValue("a", pointList);
        Color[] colors = generateColorList(pointList, maxValue);
        ParticleSystem.Particle[] particles = generateParticles(pointList, colors);
        return particles;
    }

    private Color[] generateColorList(List<Dictionary<string, object>> pointList, float maxValue)
    {
        Color[] particleColors = new Color[numPoints];
        int colorIterator = 0;

        for (var i = 0; i < numPoints; i++)
        {
            colorIterator = rnd.Next(0, 38);
            float opacity = (System.Convert.ToSingle(pointList[i][opacityColumn]) / maxValue);
            Color particleColor = new Color((float)colors[colorIterator, 0] / 255,
                            (float)colors[colorIterator, 1] / 255,
                            (float)colors[colorIterator, 2] / 255,
                            (float)opacity);
        }
        return particleColors;
    }


    private ParticleSystem.Particle[] generateParticles(List<Dictionary<string, object>> pointList, Color[] particleColors)
    {
        ParticleSystem.Particle[] ps = new ParticleSystem.Particle[numPoints];
        for (var i = 0; i < numPoints; i++)
        {
            float x = System.Convert.ToSingle(pointList[i]["x"]);
            float y = System.Convert.ToSingle(pointList[i]["y"]);
            float z = System.Convert.ToSingle(pointList[i]["z"]);
            ps[i].startColor = particleColors[i];
            ps[i].position = new Vector3(x, y, z);
            ps[i].size = 20.0f;
        }
        return ps;
    }

    private List<Dictionary<string, object>> generatePointList(string inputFile)
    {
        List<Dictionary<string, object>> pointList = new List<Dictionary<string, object>>();
        pointList = CSVReader.Read(inputFile);
        return pointList;
    }

    void Start()
    {
        particles = gameObject.GetComponent<ParticleSystem>();
        ParticleSystem.Particle[] startParticles = generateParticlesFromFile("temp");
        particles.SetParticles(startParticles, numPoints);
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

