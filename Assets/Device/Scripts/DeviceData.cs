using UnityEngine;

namespace Device.Scripts
{
	[CreateAssetMenu(fileName = "Device Data", menuName = "DeviceData")]
	public class DeviceData : ScriptableObject
	{
		[field: Tooltip("the physical screen size")]
		[field: SerializeField]
		public Vector2 ScreenDimension { get; private set; }

		[field: Tooltip("The camera position from the center of the screen")]
		[field: SerializeField]
		public Vector2 CameraPosition { get; private set; }

		public void SetScreenDimension(Vector2 screenSize)
		{
			ScreenDimension = screenSize;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="aspectRatio">width divided by height</param>
		/// <param name="diagonalSize">meters</param>
		public void SetScreenDimension(float aspectRatio, float diagonalSize)
		{
			var height = diagonalSize * Mathf.Sqrt(1 / (1 + aspectRatio * aspectRatio));
			var width = height * aspectRatio;
			SetScreenDimension(new Vector2(width, height));
		}

		public void SetCameraPosition(Vector2 position)
		{
			CameraPosition = position;
		}
	}
}