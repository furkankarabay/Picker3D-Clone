using UnityEngine;

namespace Picker3D.CollectableSystem
{
    public class Collectable : MonoBehaviour
    {
        private Rigidbody _rb;

        void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            if (transform.position.y > 0.35f)
            {
                _rb.MovePosition(new Vector3(transform.position.x, 0.35f, transform.position.z));
            }
        }

        public void Push()
        {
            _rb.AddForce(Vector3.forward * 3.5f,ForceMode.Impulse);
            _rb.AddExplosionForce(2,transform.position,1);
        }

    }
}

