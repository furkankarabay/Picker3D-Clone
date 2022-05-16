using UnityEngine;

using Picker3D.CollectableSystem;
using Picker3D.Platform.CheckPointSystem;
namespace Picker3D.PickerSystem.Controllers
{
   public class PickerCollisionController : MonoBehaviour
    {
        private Picker _picker;

        private void Awake()
        {
            _picker = GetComponent<Picker>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("DropAreaCheckPoint"))
            {
                other.enabled = false;
                other.GetComponentInParent<CheckPoint>().isActivated = true;

                _picker.DropTheBalls();
            }

            if (other.CompareTag("Collectable"))
            {
                Collectable collectable = other.GetComponent<Collectable>();
                _picker.AddCollectableToList(collectable);
            }

            if (other.CompareTag("FinishLine"))
            {
                _picker.HasStopped = true;

                GameManager.Instance.ChangeGameState(GameManager.StateOfGame.Win);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Collectable"))
            {
                Collectable collectable = other.GetComponent<Collectable>();
                _picker.RemoveCollectableFromList(collectable);
            }
        }
    }
}

