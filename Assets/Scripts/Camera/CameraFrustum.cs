using Device.Scripts;
using UnityEngine;

namespace Camera
{
	[ExecuteInEditMode]
	public class CameraFrustum : MonoBehaviour
	{
		[SerializeField] private Transform _cameraTransform;
		[SerializeField] private UnityEngine.Camera _camera;
		[SerializeField] private Transform _deviceTransform;
		[SerializeField] private DeviceData _deviceData;

		private void Awake()
		{
			Reset();
		}

		private void Reset()
		{
			if (_camera == null)
			{
				_camera = GetComponent<UnityEngine.Camera>();
				if (_camera == null)
				{
					_camera = UnityEngine.Camera.main;
				}
			}

			if (_cameraTransform == null)
			{
				if (_camera != null) _cameraTransform = _camera.transform;
			}
		}

		private void LateUpdate()
		{
			if (_deviceData == null) return;
			if (_deviceTransform == null) return;
			if (_camera == null) return;
			if (_cameraTransform == null) return;
			SetFrustum();
		}

		private void SetFrustum()
		{
			var deviceOrientation = _deviceTransform.rotation;
			var oppositeDeviceOrientation = deviceOrientation * Quaternion.Euler(Vector3.up * 180);
			_cameraTransform.rotation = oppositeDeviceOrientation;

			var worldToLocalMatrix = _cameraTransform.worldToLocalMatrix;
			var devicePosition = worldToLocalMatrix.MultiplyPoint(_deviceTransform.position);

			var y = _deviceData.ScreenDimension.y;
			var x = _deviceData.ScreenDimension.x;
			var left = devicePosition.x - x / 2f;
			var right = devicePosition.x + x / 2f;
			var top = devicePosition.y + y / 2f;
			var bottom = devicePosition.y - y / 2f;

			var planeNormal = worldToLocalMatrix.MultiplyVector(deviceOrientation * Vector3.forward);
			var devicePlane = new Plane(planeNormal, devicePosition);

			var closestPointOnPlane = devicePlane.ClosestPointOnPlane(Vector3.zero);
			var near = closestPointOnPlane.magnitude;
			var far = _camera.farClipPlane;

			_camera.projectionMatrix = PerspectiveOffCenter(left, right, bottom, top, near, far);
		}

		private static Matrix4x4 PerspectiveOffCenter(float left, float right, float bottom, float top, float near, float far)
		{
			// https://docs.unity3d.com/ScriptReference/Camera-projectionMatrix.html
			// https://answers.unity.com/questions/1359718/what-do-the-values-in-the-matrix4x4-for-cameraproj.html
			var x = 2.0F * near / (right - left); // scale factor
			var y = 2.0F * near / (top - bottom); // scale factor
			var a = (right + left) / (right - left); // linear offset
			var b = (top + bottom) / (top - bottom); // linear offset
			var c = -(far + near) / (far - near); // depth value // scale factor
			var d = -(2.0F * far * near) / (far - near); // depth value // constant offset
			var e = -1.0F;
			var m = new Matrix4x4
			{
				[0, 0] = x, [0, 1] = 0, [0, 2] = a, [0, 3] = 0,
				[1, 0] = 0, [1, 1] = y, [1, 2] = b, [1, 3] = 0,
				[2, 0] = 0, [2, 1] = 0, [2, 2] = c, [2, 3] = d,
				[3, 0] = 0, [3, 1] = 0, [3, 2] = e, [3, 3] = 0
			};
			return m;
		}
	}
}