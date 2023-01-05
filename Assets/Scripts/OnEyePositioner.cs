using Device.Scripts;
using Events;
using SuperMaxim.Messaging;
using UnityEngine;

public class OnEyePositioner : MonoBehaviour
{
	[SerializeField] private DeviceData _deviceData;
	private Vector3 _devicePosition;
	private Quaternion _deviceOrientation;
	private Vector3 _facePosition;

	private void OnEnable()
	{
		Messenger.Default.Subscribe<EyePositionChangedEvent>(OnEyePositionChange);
		Messenger.Default.Subscribe<DevicePositionChangedEvent>(OnDevicePositionChange);
		Messenger.Default.Subscribe<DeviceOrientationChangedEvent>(OnDeviceOrientationChange);
	}

	private void OnDeviceOrientationChange(DeviceOrientationChangedEvent deviceOrientationChangedEvent)
	{
		_deviceOrientation = deviceOrientationChangedEvent.Orientation;
	}

	private void OnDevicePositionChange(DevicePositionChangedEvent devicePositionChangedEvent)
	{
		_devicePosition = devicePositionChangedEvent.Position;
	}

	private void OnDisable()
	{
		Messenger.Default.Unsubscribe<EyePositionChangedEvent>(OnEyePositionChange);
		Messenger.Default.Unsubscribe<DevicePositionChangedEvent>(OnDevicePositionChange);
		Messenger.Default.Unsubscribe<DeviceOrientationChangedEvent>(OnDeviceOrientationChange);
	}

	private void OnEyePositionChange(EyePositionChangedEvent eyePositionChangedEvent)
	{
		_facePosition = eyePositionChangedEvent.Position;
	}

	private void LateUpdate()
	{
		SetPosition();
	}

	private void SetPosition()
	{
		transform.position = _devicePosition + _deviceData.CameraOffSetPosition + _deviceOrientation * _deviceData.CameraOffsetOrientation * _facePosition;
	}
}