using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class PluginCollector: MonoBehaviour
{
    public delegate void AddPlugin(IPlugin plugin);
    public event AddPlugin OnPluginAdded;

    //путь к папке с плагинами
    private readonly string pluginPath = System.IO.Path.Combine(
                                                    Directory.GetCurrentDirectory(),
                                                    "Assets\\Scripts\\Plugins");

    private List<IPlugin> plugins = new List<IPlugin>();

    private void Start()
    {
        RefreshPlugins();
    }

    private void RefreshPlugins()
    {
        plugins.Clear();

        DirectoryInfo pluginDirectory = new DirectoryInfo(pluginPath);
        if (!pluginDirectory.Exists)
            pluginDirectory.Create();

        //берем из директории все файлы с расширением .dll      
        var pluginFiles = Directory.GetFiles(pluginPath, "*.dll");
        foreach (var file in pluginFiles)
        {
            //загружаем сборку
            Assembly asm = Assembly.LoadFrom(file);
            //ищем типы, имплементирующие наш интерфейс IPlugin,
            //чтобы не захватить лишнего
            var types = asm.GetTypes().
                            Where(t => t.GetInterfaces().
                            Where(i => i.FullName == typeof(IPlugin).FullName).Any());

            //заполн€ем экземпл€рами полученных типов коллекцию плагинов
            foreach (var type in types)
            {
                var plugin = asm.CreateInstance(type.FullName) as IPlugin;      
                plugins.Add(plugin);

                var realType = asm.CreateInstance(type.FullName);

                GameObject obj = new GameObject(type.FullName);
                obj.AddComponent(realType.GetType());

                OnPluginAdded?.Invoke((IPlugin)obj.GetComponent(realType.GetType()));               

                Debug.Log("Ѕыл добавлен плагин: " + type.FullName);
            }
        }
    }
}
