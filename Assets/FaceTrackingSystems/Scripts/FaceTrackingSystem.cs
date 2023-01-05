using UnityEngine;

namespace FaceTrackingSystems.Scripts
{
    public abstract class FaceTrackingSystem : ScriptableObject
    {
        public abstract Vector3 GetFacePositionFromCamera();
    }
}
