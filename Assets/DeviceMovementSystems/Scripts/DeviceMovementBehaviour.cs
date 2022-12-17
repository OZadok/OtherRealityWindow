using System;
using Events;
using SuperMaxim.Messaging;
using UnityEngine;

namespace DeviceMovementSystems.Scripts
{
    public class DeviceMovementBehaviour : MonoBehaviour
    {
        private Vector3 _initialPosition;
        private Quaternion _initialOrientation;

        private bool _isPositionSet;
        private bool _isOrientationSet;

        private void Awake()
        {
            _initialPosition = transform.position;
            _initialOrientation = transform.rotation;
        }

        private void OnEnable()
        {
            Messenger.Default.Subscribe<DevicePositionChangedEvent>(OnDevicePositionChanged);
            Messenger.Default.Subscribe<DeviceOrientationChangedEvent>(OnDeviceOrientationChanged);
        }

        private void OnDisable()
        {
            Messenger.Default.Unsubscribe<DevicePositionChangedEvent>(OnDevicePositionChanged);
            Messenger.Default.Unsubscribe<DeviceOrientationChangedEvent>(OnDeviceOrientationChanged);
        }

        private void OnDevicePositionChanged(DevicePositionChangedEvent devicePositionChangedEvent)
        {
            // if (!_isPositionSet)
            // {
            //     _initialPosition = 
            // }
            transform.position = _initialPosition + devicePositionChangedEvent.Position;
        }

        private void OnDeviceOrientationChanged(DeviceOrientationChangedEvent deviceOrientationChangedEvent)
        {
            transform.rotation = _initialOrientation * deviceOrientationChangedEvent.Orientation;
        }
    }
}
