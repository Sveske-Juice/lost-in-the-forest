using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class ItemSaveLoad : MonoBehaviour
{
    private static List<ItemScriptableObject> items = new();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void LoadItemData()
    {
        items.Clear();
        items.AddRange(Resources.LoadAll<ItemScriptableObject>("Items"));

        ItemsSaves itemsInJson = new ItemsSaves();
        string loadPath = Path.Combine(Application.persistentDataPath, "itemData.json");
        StreamReader r = new StreamReader(loadPath);
        string temp = r.ReadToEnd();
        r.Close();
        itemsInJson = JsonUtility.FromJson<ItemsSaves>(temp);
        Debug.Log(itemsInJson);
        
        for (int i = 0; i < items.Count(); i++)
        {
            for (int j = 0; j < itemsInJson.items.Count(); j++)
            {
                if (items[i].Id == itemsInJson.items[j].id)
                {
                    items[i].SetLevel(itemsInJson.items[j].level);
                    break;
                }
            }
        }
    }
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
        itemsSaves.items = items.Select(i => new ItemSave(i.Id, i.Level)).ToArray();

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