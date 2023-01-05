namespace FaceTrackingSystems.Scripts
{
	public class NewFaceTrackingSystemSelectedEvent
	{
		public FaceTrackingSystem FaceTrackingSystem;

		public NewFaceTrackingSystemSelectedEvent(FaceTrackingSystem faceTrackingSystem)
		{
			FaceTrackingSystem = faceTrackingSystem;
		}
	}
}