using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[Serializable]

public class FigureData
{
    private string _name;

    public string Name { get {return _name;} }

    public Color Color { get { return new Color(_color.R, _color.G, _color.B, _color.A); } }
    private ColorRGBA _color;

    private List<float> _pointsX = new List<float>(10);
    private List<float> _pointsY = new List<float>(10);

    public float[] Widthes { get { return new float[2] { startWidth, endWidth }; } }

    private float startWidth;
    private float endWidth;


    public FigureData(in string name, in List<Vector2> points)
    {
        _name = name;

        foreach(Vector2 point in points)
        {
            _pointsX.Add(point.x);
            _pointsY.Add(point.y);
        }
        
    }

    public FigureData(in string name, in Color color, in List<Vector2> points, in float[] width)
    {
        _name = name;
        _color = new ColorRGBA(color);
        startWidth = width[0];
        endWidth = width[1];

        foreach (Vector2 point in points)
        {
            _pointsX.Add(point.x);
            _pointsY.Add(point.y);
        }
    }

    public List<Vector2> GetPoints()
    {
        List<Vector2> points = new List<Vector2>(_pointsX.Count);

        for(int i = 0; i < _pointsX.Count; i++)
        {
            points.Add(new Vector2(_pointsX[i], _pointsY[i]));
        }

        return points;
    }
}

[Serializable]
internal struct ColorRGBA
{
    internal float R;
    internal float G;
    internal float B;
    internal float A;

    public ColorRGBA(in Color color)
    {
        R = color.r;
        G = color.g;
        B = color.b;
        A = color.a;
    }
}