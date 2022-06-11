using System;
using System.Collections.Generic;
using UnityEngine;

public class Graphics : MonoBehaviour
{
    public delegate void SaveObjects(in List<FigureData> figures);
    public event SaveObjects OnSaveFigures;

    //public event Action OnLoadFigures;

    [SerializeField]
    private Material _material;

    [SerializeField]
    private FigureStorage storage;

    //[SerializeField]
    //private FigureSerializer serializer;

    [SerializeField]
    private List<GameObject> activeFigures = new List<GameObject>(10);

    //��� ������� ���������� ����������� �������
    [SerializeField]
    private PluginCollector pluginCollector;

    private List<IPlugin> plugins = new List<IPlugin>(10);

    private void Start()
    {
        //serializer.OnFiguresDeserialized += LoadFigures;

        pluginCollector.OnPluginAdded += ProcessNewPlugin;

        //ProcessNewPlugin(FindObjectOfType<ColorPlugin>());
    }

    private void CreateFigureOnScene(BaseFigure figure, float[] widthes = null)
    {
        GameObject obj = new GameObject(figure.GetName());

        obj.AddComponent<LineRenderer>();

        obj.AddComponent<RenderController>();
        obj.GetComponent<RenderController>().Subscribe(figure, _material, widthes);

        activeFigures.Add(obj);
        //OnActFiguresNumChanged?.Invoke(activeFigures);

        foreach(IPlugin plugin in plugins)
            plugin.ChangeFigureNumber(activeFigures);
    }

    public void DrawNeededFigure(string name)
    {
        BaseFigure figure = storage.FindFigure(name);

        if (figure)
        {
            CreateFigureOnScene(figure);

            figure.Draw();
            figure.ClearPoints();
        }
        else
        {
            Debug.Log("���������� ���������� ������ " + name);
        }
    }
    private void RefreshFigure(FigureData figureData)
    {
        BaseFigure figure = storage.FindFigure(figureData.Name);

        if (figure)
        {
            CreateFigureOnScene(figure, figureData.Widthes);

            //������ �� ����� ������� ���� �����
            figure.Draw(false);
            figure.ClearPoints();
        }
        else
        {
            Debug.Log("���������� ���������� ������ " + figureData.Name);
        }
    }

    public void ClearCanvas()
    {
        for (int i = activeFigures.Count - 1; i >= 0; i--)
        {
            Destroy(activeFigures[i]);
        }

        activeFigures.Clear();

        //����
        //OnActFiguresNumChanged?.Invoke(activeFigures);
        //�����
        foreach (IPlugin plugin in plugins)
            plugin.ChangeFigureNumber(activeFigures);
    }

    public void SaveFigures()
    {
        List<FigureData> figuresData = new List<FigureData>(activeFigures.Count);

        /* ������ �� storage
        foreach(BaseFigure figure in storage.figures)
        {
            FigureData figureData = new FigureData(
                figure.GetName(),
                figure.GetColor(),
                figure.GetPoints());

            figuresData.Add(figureData);
        }
        */

        //������ �� �����
        foreach (GameObject sceneFigure in activeFigures)
        {
            if (sceneFigure)
            {
                LineRenderer render = sceneFigure.GetComponent<LineRenderer>();

                //Vector2 �� ��������������, ���))
                Vector3[] points = new Vector3[render.positionCount];
                render.GetPositions(points);
                //���������
                List<Vector2> points2D = new List<Vector2>(points.Length);
                for (int i = 0; i < points.Length; i++)
                {
                    points2D.Add(new Vector2(points[i].x, points[i].y));
                }

                RenderController rendController = sceneFigure.GetComponent<RenderController>();

                

                FigureData figureData = new FigureData(
                    sceneFigure.name,
                    render.startColor,
                    points2D, rendController.GetWidthes());

                figuresData.Add(figureData);
            }
        }


        OnSaveFigures?.Invoke(figuresData);
    }

    //����� ��������������
    public void LoadFigures(List<FigureData> figuresData)
    {
        foreach (FigureData figureData in figuresData)
        {
            BaseFigure figure = storage.FindFigure(figureData.Name);

            if (figure)
            {
                figure.SetPoints(figureData.GetPoints());
                //����� ���� ���� �������� � ��� �������
                figure.SetColor(figureData.Color);

                RefreshFigure(figureData);

                Debug.Log("������ " + figureData.Name + " ���� ������� ���������");
            }

        }
    }

    #region InfoForPlugin

    /*
    private void AddListener(PluginBase.ChangeActiveFiguresNumber callback)
    {
        OnActFiguresNumChanged += callback;
    }
    //loh azazaz
    private void RemoveListener(PluginBase.ChangeActiveFiguresNumber callback)
    {
        OnActFiguresNumChanged += callback;
    }
    */

    private void ProcessNewPlugin(IPlugin plugin)
    {
        //����� �������, ��� ������ ����� �������� ������ �������� �����
        //AddListener(plugin.ConnectByEvent());
        plugins.Add(plugin);

        plugin.ChangeFigureNumber(activeFigures);

        //plugin.OnChangeFigureColor += ChangeFigureColor;
    }

    /*
    private void ChangeFigureColor(GameObject figure, Color color)
    {
        figure.GetComponent<RenderController>().ChangeColor(color);
    }
    */
    #endregion
}
