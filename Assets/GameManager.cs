using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private ItemSaveLoad itemSaveLoad = new ItemSaveLoad();

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);
    }

    private void OnApplicationQuit()
    {
        itemSaveLoad.SaveItems();
    }

}
