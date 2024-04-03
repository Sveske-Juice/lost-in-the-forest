using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnTrigger : MonoBehaviour
{
    public string SceneName = "CHange me you fucking idiot";

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("Player")) // This is a player
        {
            // Change scene
            Debug.Log("Change scene to " + SceneName);

            SceneManager.LoadScene(SceneName);
        }
    }
}
