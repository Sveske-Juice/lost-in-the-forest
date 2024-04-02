using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[RequireComponent(typeof(ItemSaveLoad))]
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private ItemSaveLoad itemSaveLoad;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        itemSaveLoad = GetComponent<ItemSaveLoad>();
        DontDestroyOnLoad(this);
    }

    private void OnApplicationQuit()
    {
        itemSaveLoad.SaveItems();
    }

}
