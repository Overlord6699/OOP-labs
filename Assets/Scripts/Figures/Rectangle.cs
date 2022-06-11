using System;
using System.Collections.Generic;
using UnityEngine;

public class Rectangle : BaseFigure
{
    [SerializeField]
    protected Vector2 leftPoint = new Vector2(400, 400);
    [SerializeField]
    protected Vector2 rightPoint = new Vector2(800, 200);

    private void Awake()
    {
        _name = "Rectangle";
    }

    public override void CreateFigure(params Vector2[] points)
    {
        leftPoint = points[0];
        rightPoint = points[1];
    }

    protected virtual void CalculatePoints()
    {
        _points.Add(new Vector2(rightPoint.x, _points[0].y));
        _points.Add(rightPoint);
        _points.Add(new Vector2(_points[0].x, rightPoint.y));
    }

    protected override void WritePoints()
    {
        _points.Add(leftPoint);

        CalculatePoints();

        _points.Add(leftPoint);
    }

    public override void Draw(in bool writingFigurePoints = true)
    {
        base.Draw(writingFigurePoints);
    }

}
