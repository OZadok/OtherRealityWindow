using UnityEngine;

namespace DeviceMovementSystems.Scripts.Systems
{
    [CreateAssetMenu(fileName = "NoDeviceMovement", menuName = "Device Movement Systems/NoDeviceMovement")]
    public class NoDeviceMovement : DeviceMovementSystem
    {
        public override Vector3 GetPosition()
        {
            return Vector3.zero;
        }

        public override Quaternion GetOrientation()
        {
            return Quaternion.identity;
        }
    }
}
