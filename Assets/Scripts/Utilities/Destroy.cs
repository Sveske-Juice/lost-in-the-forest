using UnityEngine;

public class Destroy : MonoBehaviour
{
    public void After(float seconds)
    {
        Destroy(gameObject, seconds);
    }

    public void DestroyNow()
    {
        Destroy(gameObject);
    }
}
