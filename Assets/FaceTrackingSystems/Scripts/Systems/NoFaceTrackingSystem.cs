using UnityEngine;

namespace FaceTrackingSystems.Scripts.Systems
{
    [CreateAssetMenu(menuName = "Face Tracking Systems/No Face Tracking System", fileName = "No Face Tracking")]
    public class NoFaceTrackingSystem : FaceTrackingSystem
    {
        private static readonly Vector3 Position = new Vector3(0,0,0.5f);

        public override Vector3 GetFacePositionFromCamera()
        {
            return Position;
        }
    }
}
