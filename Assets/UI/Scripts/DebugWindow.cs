using System;
using System.Linq;
using DeviceMovementSystems.Scripts;
using Events;
using SuperMaxim.Messaging;
using TMPro;
using UnityEngine;

namespace UI.Scripts
{
    public class DebugWindow : MonoBehaviour
    {
        [SerializeField] private TMP_Text _positionText;
        [SerializeField] private TMP_Text _orientationText;

        [SerializeField] private Transform _xAxis;
        [SerializeField] private Transform _yAxis;
        [SerializeField] private Transform _zAxis;
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
            // _positionText.text = devicePositionChangedEvent.Position.ToString();
            // _positionText.text = $"{Input.acceleration}, {Input.gyro.gravity}, {Input.gyro.userAcceleration}";
        }

        private void OnDeviceOrientationChanged(DeviceOrientationChangedEvent deviceOrientationChangedEvent)
        {
            // _orientationText.text = deviceOrientationChangedEvent.Orientation.eulerAngles.ToString();
            // _positionText.text = $"{Input.acceleration}, {Input.gyro.gravity}, {Input.gyro.userAcceleration}";
        }

        private void Update()
        {
            var acceleration = Input.acceleration - Input.gyro.gravity;
            var gyroUserAcceleration = Input.gyro.userAcceleration;
            // _positionText.text = $"{acceleration}, {gyroUserAcceleration}";
            //
            // _orientationText.text = $"{acceleration - gyroUserAcceleration}";

            // var accelerationFromEvents = Input.accelerationEvents.Aggregate(Vector3.zero, (current, accelerationEvent) => current + accelerationEvent.acceleration - Input.gyro.gravity);
            // _positionText.text = $"{acceleration - accelerationFromEvents}";
            //
            // _orientationText.text = $"{accelerationFromEvents - gyroUserAcceleration}";
            Input.gyro.enabled = true;
            Input.gyro.updateInterval = 0.002f;
            _positionText.text = $"{Input.gyro.updateInterval}";
            
            
            _orientationText.text = $"x: {gyroUserAcceleration.x}\ny: {gyroUserAcceleration.y}\nz: {gyroUserAcceleration.z}";

            var xAxis = _xAxis.localScale;
            xAxis.x = gyroUserAcceleration.x;
            _xAxis.localScale = xAxis;
            
            var yAxis = _yAxis.localScale;
            yAxis.x = gyroUserAcceleration.y;
            _yAxis.localScale = yAxis;
            
            var zAxis = _zAxis.localScale;
            zAxis.x = gyroUserAcceleration.z;
            _zAxis.localScale = zAxis;
        }
    }
}
