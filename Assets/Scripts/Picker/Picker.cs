using System.Collections.Generic;
using UnityEngine;

using Picker3D.CollectableSystem;
using Picker3D.Platform.CheckPointSystem;

namespace Picker3D.PickerSystem
{
    public class Picker : MonoBehaviour
    {
        public bool HasStopped { get; set; } = false;
        public bool IsStarted { get; set; } = false;


        private List<Collectable> _collectedCollectables = new List<Collectable>();
        private Vector3 _startPosition;

        private Camera _pickerCamera;
        [SerializeField] private Vector3 cameraOffset;

        private void Start()
        {
            _pickerCamera = Camera.main;
            _startPosition = transform.position;

            CheckPoint.Completed += CheckPoint_Completed;
            GameManager.LevelCompleted += GameManager_LevelCompleted;
            GameManager.LevelFailed += GameManager_LevelFailed;
        }

        private void LateUpdate()
        {
            if (_pickerCamera == null)
                return;

            _pickerCamera.transform.position = new Vector3(_pickerCamera.transform.position.x, transform.position.y + cameraOffset.y,
                transform.position.z + cameraOffset.z);
        }

        private void GameManager_LevelFailed()
        {
            ResetValues();
        }

        private void GameManager_LevelCompleted()
        {
            ResetValues();
        }

        private void CheckPoint_Completed()
        {
            HasStopped = false;
        }

        public void DropTheBalls()
        {
            HasStopped = true;
            foreach (Collectable item in _collectedCollectables)
            {
                item.Push();
            }
        }

        public void AddCollectableToList(Collectable collectable)
        {
            _collectedCollectables.Add(collectable);
        }

        public void RemoveCollectableFromList(Collectable collectable)
        {
            _collectedCollectables.Remove(collectable);
        }

        private void ResetValues()
        {
            transform.position = _startPosition;
            IsStarted = false;
            HasStopped = false;
        }
    }
}

