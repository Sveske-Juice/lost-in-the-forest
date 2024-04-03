using UnityEngine;

public class DontDetroy : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this);;
    }
}
