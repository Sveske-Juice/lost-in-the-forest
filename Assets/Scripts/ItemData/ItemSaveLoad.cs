using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class ItemSaveLoad : MonoBehaviour
{
    private List<ItemScriptableObject> items = new();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveItems()
    {
        items.Clear();
        items.AddRange(Resources.LoadAll<ItemScriptableObject>("Items"));
        ItemsSaves itemsSaves = new ItemsSaves();
        itemsSaves.items = this.items.Select(i => new ItemSave(i.Id, i.Level)).ToArray();

        string json = JsonUtility.ToJson(itemsSaves);
        Debug.Log(json);
        string savePath = Path.Combine(Application.persistentDataPath, "itemData.json");
        StreamWriter t = new StreamWriter(savePath, false);
        t.Write(json);
        t.Close();

    }
}
[Serializable]
public class ItemsSaves
{
    public ItemSave[] items;
}
[Serializable]
public class ItemSave
{
    public string id;
    public int level;

    public ItemSave(string id, int level)
    {
        this.id = id;
        this.level = level;
    }
}