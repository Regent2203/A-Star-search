using UnityEngine;

namespace ThisProject.Cameras
{
    [RequireComponent(typeof(Camera))]
    public class CameraZoomController : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;

        [Header("Zoom Settings")]
        [Range(0.5f, 2.0f)]
        [SerializeField] private float _zoomSensitivity = 1.5f;
        [SerializeField] private float _minOrthographicSize = 10f;
        [SerializeField] private float _maxOrthographicSize = 40f;


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
        }


        private void Reset()
        {
            _mainCamera = GetComponent<Camera>();
        }
    }
}