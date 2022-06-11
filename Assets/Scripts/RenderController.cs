using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderController : MonoBehaviour
{
    private LineRenderer _renderer;


    [SerializeField]
    private Color _color = Color.black;
    [SerializeField]
    private float _startWidth = 5f;
    [SerializeField]
    private float _endWidth = 5f;

    private BaseFigure _figure = null;

    private void OnEnable()
    {
        _renderer = GetComponent<LineRenderer>();

        SetRendererSettings();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    public void Subscribe(BaseFigure figure, Material material, float[] widthes)
    {
        figure.OnDrawFigure += DrawFigureByPoints;
        //figure.OnChangeColor += ChangeColor;

        if (widthes != null)
        {
            _startWidth = widthes[0];
            _endWidth = widthes[1];
        }
        //придётся запомнить для отписки
        _figure = figure;
        ChangeColor(_figure.GetColor());

        _renderer.material = material;
    }

    private void Unsubscribe()
    {
        if (_figure)
        {
            _figure.OnDrawFigure -= DrawFigureByPoints;
            //_figure.OnChangeColor -= ChangeColor;
        }
    }

    private void OnValidate()
    {
        //при малейшей перестройке Line Renderer всё слетает ,sо
        _renderer.startWidth = _startWidth;
        _renderer.endWidth = _endWidth;

        RedrawFigureWithNewColor(_color);
    }

    private void SetRendererSettings()
    {
        _renderer.startWidth = _startWidth;
        _renderer.endWidth = _endWidth;
        _renderer.positionCount = 0;


        ChangeColor(_color);
    }

    private void DrawFigureByPoints(in List<Vector2> points)
    {
        foreach (var point in points)
        {
            _renderer.positionCount++;
            _renderer.SetPosition(
                _renderer.positionCount - 1,
                (Vector3)point);
        }

        //очень важно, так как иначе будет
        //отрисовывать несколько фигур подряд из-за накопления подписок
        Unsubscribe();
    }


    public void ChangeColor(Color color)
    {
        _color = color;

        _renderer.startColor = _color;
        _renderer.endColor = _color;

        if (_figure)
            _figure.SetColor(color);
    }

    public Color GetColor()
    {
        return _color;
    }

    public float[] GetWidthes()
    {
        return new float[] { _startWidth, _endWidth };
    }

    public void RedrawFigureWithNewColor(in Color color)
    {
        ChangeColor(color);

        Vector3[] positions = new Vector3[_renderer.positionCount];

        _renderer.GetPositions(positions); ;

        ClearFigure();

        List<Vector2> posList = new List<Vector2>(positions.Length);
        //ничего не поделаешь
        foreach(var position in positions)
        {
            posList.Add(position);
        }

        //перерисовка фигуры
        DrawFigureByPoints(posList);
    }

    private void ClearFigure()
    {
        _renderer.positionCount = 0;
    }

}
