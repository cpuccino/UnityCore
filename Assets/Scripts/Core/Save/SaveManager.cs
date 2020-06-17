using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityCore.Utilities.Serialization;
using UnityEngine;

public class SaveManager
{
    public static readonly string SaveBasePath = Path.Combine(Application.persistentDataPath, "saves");
    public static readonly string SaveFileExtension = ".sav";

    public List<string> SaveFiles { get { return GetSaveFiles(); } }

    private List<string> GetSaveFiles()
    {
        if (!Directory.Exists(SaveBasePath))
        {
            Directory.CreateDirectory(SaveBasePath);
        }

        return Directory.GetFiles(SaveBasePath)
            .Where(path => path.EndsWith(SaveFileExtension))
            .Select(path => Path.GetFileNameWithoutExtension(path))
            .ToList();
    }

    public void Save(string name, SaveData data)
    {
        Serialization.Save(SaveBasePath, name, data);
    }

    public SaveData Load(string name)
    {
        if (!SaveFiles.Contains(name)) return null;

        var saveData = (SaveData)Serialization.Load(Path.Combine(SaveBasePath, name));
        return saveData;
    }
}