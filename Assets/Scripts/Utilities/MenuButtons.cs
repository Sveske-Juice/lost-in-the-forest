using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    private ItemSaveLoad itemSaveLoad = new ItemSaveLoad();
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
