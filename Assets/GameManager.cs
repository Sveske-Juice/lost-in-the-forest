using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player;


    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }
}
