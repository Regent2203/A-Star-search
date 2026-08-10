using EasyField.Fields;
using UnityEngine;
using Zenject;

namespace EasyField.Cameras
{
    [RequireComponent(typeof(Camera))]
    public class CameraZoomController : MonoBehaviour
    {
        [Header("Zoom Settings")]
        [Range(0.5f, 2.0f)]
        [SerializeField] private float _zoomSensitivity = 1.5f;
        [SerializeField] private float _minOrthographicSize = 10f;
        [SerializeField] private float _maxOrthographicSize = 40f;

        private Camera _mainCamera;
        private BoxCollider2D _box;


        [Inject]
        public void Construct(Camera mainCamera, IField field)
        {
            _mainCamera = mainCamera;
            _box = field.Box;
        }

        private void Update()
        {
            HandleScrollZoom();
        }

        private void HandleScrollZoom()
        {
            var scrollInput = Input.GetAxisRaw("Mouse ScrollWheel");

            if (Mathf.Approximately(scrollInput, 0f))
            {
                return;
            }

            Vector3 mouseWorldBeforeZoom = _mainCamera.ScreenToWorldPoint(Input.mousePosition);

            float currentZoom = _mainCamera.orthographicSize;
            float targetZoom = currentZoom - (scrollInput * _zoomSensitivity * currentZoom);

            _mainCamera.orthographicSize = Mathf.Clamp(targetZoom, _minOrthographicSize, _maxOrthographicSize);

            Vector3 mouseWorldAfterZoom = _mainCamera.ScreenToWorldPoint(Input.mousePosition);

            transform.position += mouseWorldBeforeZoom - mouseWorldAfterZoom;
            transform.position = transform.position.Clamp(_box.bounds);
        }


        private void Reset()
        {
            _mainCamera = GetComponent<Camera>();
        }
    }
}