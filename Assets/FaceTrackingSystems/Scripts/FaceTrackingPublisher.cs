using Events;
using SuperMaxim.Messaging;
using UnityEngine;

namespace FaceTrackingSystems.Scripts
{
	public class FaceTrackingPublisher : MonoBehaviour
	{
		private FaceTrackingSystem _faceTrackingSystem;
		private EyePositionChangedEvent _eyePositionChangedEvent;
		private Vector3 _position;

		private void Awake()
		{
			_eyePositionChangedEvent = new EyePositionChangedEvent();
		}

		private void OnEnable()
		{
			Messenger.Default.Subscribe<NewFaceTrackingSystemSelectedEvent>(OnNewFaceTrackingSystemSelected);
		}

		private void OnDisable()
		{
			Messenger.Default.Unsubscribe<NewFaceTrackingSystemSelectedEvent>(OnNewFaceTrackingSystemSelected);
		}

		private void Update()
		{
			if (_faceTrackingSystem == null)
			{
				return;
			}

			PositionChangeCheck();
		}

		private void PositionChangeCheck()
		{
			var newPosition = _faceTrackingSystem.GetFacePositionFromCamera();
			if (newPosition.Equals(_position)) return;
			_position = newPosition;
			_eyePositionChangedEvent.Position = _position;
			Messenger.Default.Publish(_eyePositionChangedEvent);
		}

		private void OnNewFaceTrackingSystemSelected(NewFaceTrackingSystemSelectedEvent newFaceTrackingSystemSelectedEvent)
		{
			_faceTrackingSystem = newFaceTrackingSystemSelectedEvent.FaceTrackingSystem;
		}
	}
}