using UnityEngine;

public class Destroy : MonoBehaviour
{
    public void After(float seconds)
    {
        Destroy(gameObject, seconds);
    }

    public void DestroyRootAfter(float seconds)
    {
        Destroy(transform.root.gameObject, seconds);
    }

    public void DestroyNow()
    {
        Destroy(gameObject);
    }

    public void DestroyRootNow()
    {
        Destroy(transform.root.gameObject);
    }
}
