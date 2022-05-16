using System;
using UnityEngine;


namespace Picker3D.PickerSystem.Controllers
{
    public class PickerMovementController : MonoBehaviour
    {
        private float _speedForward = 1f;
        private float _speedHorizontal = 1f;
        private float _distanceToScreen;
        private float _directionMouseX;

        private Vector3 _mousePos;

        private Camera _camera;
        private Rigidbody _rb;

        private Picker _picker;

        private void Awake()
        {
            _picker = GetComponent<Picker>();
            _rb = GetComponent<Rigidbody>();
            _camera = Camera.main;
        }

        private void Update()
        {
            
            _directionMouseX = 0;

            if (Input.GetMouseButton(0))
            {
                MouseControl();
            }
            if (Input.GetMouseButtonUp(0))
            {
                _directionMouseX = 0;
            }

        }

        private void FixedUpdate()
        {

            if (!GameManager.Instance.IsGameStarted)
                return;

            if (_picker.HasStopped)
                return;

            Movement();

        }

        private void MouseControl()
        {
            var position = Input.mousePosition;

            _distanceToScreen = _camera.WorldToScreenPoint(gameObject.transform.position).z;
            _mousePos = _camera.ScreenToWorldPoint(new Vector3(position.x, position.y, _distanceToScreen));

            if (Math.Abs(_mousePos.x - transform.position.x) > .025f)
            {
                _directionMouseX = 1;
                _directionMouseX = _mousePos.x > transform.position.x ? _directionMouseX : -_directionMouseX; 
            }
        }

        private void Movement()
        {
            float zAxis = (_speedForward * Time.fixedDeltaTime);
            float xAxis = (_directionMouseX * _speedHorizontal * Time.fixedDeltaTime);

            Vector3 newPos = transform.position + new Vector3(xAxis, 0, zAxis);
            newPos.x = (Mathf.Clamp(newPos.x, -0.38f, 0.38f));
            _rb.MovePosition(newPos);

        }
    }


   
    
}

