using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void sceneChanger(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
        GameManager.instance.scene = sceneID;
    }

}
