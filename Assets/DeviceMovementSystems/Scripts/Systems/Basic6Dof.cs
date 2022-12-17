using System.Linq;
using UnityEngine;

namespace DeviceMovementSystems.Scripts.Systems
{
	[CreateAssetMenu(fileName = "Basic6Dof", menuName = "Device Movement Systems/Basic6Dof")]
	public class Basic6Dof : DeviceMovementSystem
	{
		private Vector3 _position = Vector3.zero;
		private readonly Quaternion _correctionQuaternion = Quaternion.Euler(90f, 0f, 0f);
		private Vector3 _velocity = Vector3.zero;

		[SerializeField] private bool _toCalculatePosition = true;

		public override void Start()
		{
			base.Start();
			if (!Input.gyro.enabled)
			{
				Input.gyro.enabled = true;
			}
		}

		public override void End()
		{
			base.End();
			Input.gyro.enabled = false;
		}

		public override Vector3 GetPosition()
		{
			if (!_toCalculatePosition)
			{
				return Vector3.zero;
			}
			var deltaTime = Time.unscaledDeltaTime;
			if (!Input.gyro.enabled)
			{
				Input.gyro.enabled = true;
			}
			var acceleration = Input.gyro.userAcceleration;
			_velocity += acceleration * deltaTime;
			_position += _velocity * deltaTime;
			return _position;
		}

		public override Quaternion GetOrientation()
		{
			if (!Input.gyro.enabled)
			{
				Input.gyro.enabled = true;
			}

			var gyroOrientation = _correctionQuaternion  * GyroToUnity(Input.gyro.attitude);
			return gyroOrientation;
		}
		
		private static Quaternion GyroToUnity(Quaternion q)
		{
			return new Quaternion(q.x, q.y, -q.z, -q.w);
		}
	}
}
