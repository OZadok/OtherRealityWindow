using UnityEngine;

namespace DeviceMovementSystems.Scripts
{
	public abstract class DeviceMovementSystem
	{ 
		public abstract Vector3 GetPosition();
		public abstract Quaternion SetOrientation();
	}
}
