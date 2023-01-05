using FaceTrackingSystems.Scripts;
using SuperMaxim.Messaging;
using TMPro;
using UnityEngine;

namespace UI.Scripts
{
	public class FaceTrackingSystemSelector : MonoBehaviour
	{
		[SerializeField] private FaceTrackingSystem[] _FaceTrackingSystems;

		[SerializeField] private TMP_Dropdown _dropdown;

		private void Awake()
		{
			FillDropDown();
			_dropdown.onValueChanged.AddListener(OnDropDownValueChanged);
		}

		private void Start()
		{
			OnDropDownValueChanged(_dropdown.value);
		}

		private void OnDropDownValueChanged(int index)
		{
			var selectedFaceTrackingSystem = _FaceTrackingSystems[index];
			Messenger.Default.Publish(new NewFaceTrackingSystemSelectedEvent(selectedFaceTrackingSystem));
		}

		private void FillDropDown()
		{
			var optionData = new TMP_Dropdown.OptionData[_FaceTrackingSystems.Length];
			for (int i = 0; i < _FaceTrackingSystems.Length; i++)
			{
				optionData[i] = new TMP_Dropdown.OptionData(_FaceTrackingSystems[i].name);
			}

			_dropdown.options.Clear();
			_dropdown.options.AddRange(optionData);
		}
	}
}