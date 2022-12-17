namespace DeviceMovementSystems.Scripts
{
	public class NewDeviceMovementSystemSelectedEvent
	{
		public DeviceMovementSystem DeviceMovementSystem;

		public NewDeviceMovementSystemSelectedEvent(DeviceMovementSystem deviceMovementSystem)
		{
			DeviceMovementSystem = deviceMovementSystem;
		}
	}
}