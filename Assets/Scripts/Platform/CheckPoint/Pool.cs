using UnityEngine;

namespace Picker3D.Platform.CheckPointSystem
{
    public class Pool : MonoBehaviour
    {
        private CheckPoint checkPoint;

        private void Awake()
        {
            checkPoint = GetComponentInParent<CheckPoint>();
        }
        private void OnCollisionEnter(Collision collision)
        {
            checkPoint.HowManyWeHave++;
        }
    }
}

