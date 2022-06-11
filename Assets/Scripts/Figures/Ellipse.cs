using System;
using System.Collections.Generic;
using UnityEngine;

public class Ellipse : Circle
{
    [SerializeField][Header("Вертикальная полуось")]
    private float _a = 80;
    [SerializeField][Header("Горизонтальная полуось")]
    private float _b = 50;
    [SerializeField]
    private float theta = 15;

    private void Awake()
    {
        _name = "Ellipse";
    }

    public void SetAngle(in float angle)
    {
        theta = angle;
    }

    public void SetLength(in float a, in float b)
    {
        _a = a;
        _b = b;
    }

    protected override void CalculatePoints()
    {
        Quaternion q = Quaternion.AngleAxis(theta, Vector2.up);

        for (int i = 0; i <= _segments; i++)
        {
            float angle = (float)i / (float)_segments * 2.0f
                * Mathf.PI;

            Vector2 pos = new Vector2(_a * Mathf.Cos(angle),
                _b * Mathf.Sin(angle));

            Vector2 resPos = q * pos;

            _points.Add(resPos + _center);
        }
    }

    public override void CreateFigure(params Vector2[] points)
    {
        base.CreateFigure();
    }


    public override void Draw(in bool writingFigurePoints = true)
    {
        base.Draw(writingFigurePoints);
    }

}
