using UnityEngine;

namespace DeviceMovementSystems.Scripts
{
	public abstract class DeviceMovementSystem : ScriptableObject
	{
		public virtual void Start()
		{
			
		}

		public virtual void End()
		{
			
		}
		
		public abstract Vector3 GetPosition();
		public abstract Quaternion GetOrientation();
	}
}
