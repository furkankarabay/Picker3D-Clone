using UnityEngine;

public class Level : MonoBehaviour
{
    public int numberOfCheckPoint;
    public void DestroyLevel()
    {
        Destroy(gameObject);
    }
}
