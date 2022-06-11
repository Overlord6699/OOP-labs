using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseFigure: MonoBehaviour
{
    private Vector2 point;

    protected string _name;

    protected Color _color = Color.black;

    protected List<Vector2> _points = new List<Vector2>(10);

    public delegate void DrawFigure(in List<Vector2> points);
    public event DrawFigure OnDrawFigure;

    //public delegate void ChangeColor(Color color);
    //public event ChangeColor OnChangeColor;

    private void Awake()
    {
        _name = "Point";
    }

    public  string GetName()
    {
        return _name;
    }

    public void SetColor(Color color)
    {
        _color = color;

        //OnChangeColor?.Invoke(_color);
    }

    public Color GetColor()
    {
        return _color;
    }

    public virtual void CreateFigure(params Vector2[] points)
    {
        point = points[0];
    }

    protected virtual void WritePoints()
    {
        _points.Add(point);
    }

    public virtual void Draw(in bool writingFigurePoints = true)
    {
        if(writingFigurePoints)
            WritePoints();

        OnDrawFigure?.Invoke(_points);

        ClearPoints();
    }

    public void ClearPoints()
    {
        _points.Clear();
    }

    
    public List<Vector2> GetPoints()
    {
        return _points;
    }

    //тут будет передаваться конкретное кол-во точек с загрузки
    public virtual void SetPoints(List<Vector2> points)
    {
        _points = points;
    }
}
