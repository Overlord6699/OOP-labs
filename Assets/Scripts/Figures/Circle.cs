using System;
using System.Collections.Generic;
using UnityEngine;

public class Circle : BaseFigure
{
    [SerializeField]
    private float _radius = 100;
    [SerializeField]
    protected Vector2 _center = new Vector2(500, 300);
    [SerializeField]
    protected int _segments = 30;

    private void Awake()
    {
        _name = "Circle";
    }

    public void SetRadius(in float radius)
    {
        _radius = radius;
    }

    public void SetSegments(in int segments)
    {
        _segments = segments;
    }

    public override void CreateFigure(params Vector2[] points)
    {      
        _center = points[0];
    
    }

    protected virtual void CalculatePoints()
    {
        float x;
        float y;

        float change = 2 * (float)System.Math.PI / _segments;
        float angle = 0;

        y = Mathf.Sin(angle) * _radius;

        _points.Add(new Vector2(_center.x+_radius, _center.y));

        for (int i = 0; i <= _segments; i++)
        {
            y = Mathf.Sin(angle) * _radius;
            x = Mathf.Cos(angle) * _radius;

            _points.Add(new Vector2(
                    _center.x+x,
                    _center.y+y));


            angle += change;
        }
    }

    protected override void WritePoints()
    {
        //здесь будет пусто для перебивки базовой реализации
        //да, странно
    }

    public override void Draw(in bool writingFigurePoints = true)
    {
        CalculatePoints();

        base.Draw(writingFigurePoints);
    }
}
