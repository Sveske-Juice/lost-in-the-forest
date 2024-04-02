using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ItemSaveLoad))]
public class MenuButtons : MonoBehaviour
{
    private ItemSaveLoad itemSaveLoad;

    private void OnEnable()
    {
        itemSaveLoad = GetComponent<ItemSaveLoad>();
    }

    public void sceneChanger(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    public void closeGame()
    {
        itemSaveLoad.SaveItems();
        Application.Quit();
    }

}
