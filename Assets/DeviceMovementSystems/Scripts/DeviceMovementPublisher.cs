using System;
using Events;
using SuperMaxim.Messaging;
using UnityEngine;

namespace DeviceMovementSystems.Scripts
{
	public class DeviceMovementPublisher : MonoBehaviour
	{
		private DeviceMovementSystem _deviceMovementSystem;

		private DevicePositionChangedEvent _devicePositionChangedEvent;
		private Vector3 _position;

		private DeviceOrientationChangedEvent _deviceOrientationChangedEvent;
		private Quaternion _orientation;

		private void Awake()
		{
			_devicePositionChangedEvent = new DevicePositionChangedEvent();
			_deviceOrientationChangedEvent = new DeviceOrientationChangedEvent();
		}

		private void OnEnable()
		{
			Messenger.Default.Subscribe<NewDeviceMovementSystemSelectedEvent>(OnNewDeviceMovementSystemSelected);
		}

		private void OnDisable()
		{
			Messenger.Default.Unsubscribe<NewDeviceMovementSystemSelectedEvent>(OnNewDeviceMovementSystemSelected);
		}

		private void Update()
		{
			if (_deviceMovementSystem == null)
			{
				return;
			}

			PositionChangeCheck();
			OrientationChangeCheck();
		}

		private void PositionChangeCheck()
		{
			var newPosition = _deviceMovementSystem.GetPosition();
			if (newPosition.Equals(_position)) return;
			_position = newPosition;
			_devicePositionChangedEvent.Position = _position;
			Messenger.Default.Publish(_devicePositionChangedEvent);
		}

		private void OrientationChangeCheck()
		{
			var newOrientation = _deviceMovementSystem.GetOrientation();
			if (newOrientation.Equals(_orientation)) return;
			_orientation = newOrientation;
			_deviceOrientationChangedEvent.Orientation = _orientation;
			Messenger.Default.Publish(_deviceOrientationChangedEvent);
		}
		
		private void OnNewDeviceMovementSystemSelected(NewDeviceMovementSystemSelectedEvent newDeviceMovementSystemSelectedEvent)
		{
			_deviceMovementSystem?.End();
			_deviceMovementSystem = newDeviceMovementSystemSelectedEvent.DeviceMovementSystem;
			_deviceMovementSystem.Start();
		}
	}
}