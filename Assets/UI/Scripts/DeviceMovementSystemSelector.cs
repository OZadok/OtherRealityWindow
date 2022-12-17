using System;
using System.Collections.Generic;
using DeviceMovementSystems.Scripts;
using SuperMaxim.Messaging;
using TMPro;
using UnityEngine;

namespace UI.Scripts
{
    public class DeviceMovementSystemSelector : MonoBehaviour
    {
        [SerializeField] private DeviceMovementSystem[] _deviceMovementSystems;

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
            var selectedDeviceMovementSystem = _deviceMovementSystems[index];
            Messenger.Default.Publish(new NewDeviceMovementSystemSelectedEvent(selectedDeviceMovementSystem));
        }

        private void FillDropDown()
        {
            var optionData = new TMP_Dropdown.OptionData[_deviceMovementSystems.Length];
            for (int i = 0; i < _deviceMovementSystems.Length; i++)
            {
                optionData[i] = new TMP_Dropdown.OptionData(_deviceMovementSystems[i].name);
            }
            _dropdown.options.Clear();
            _dropdown.options.AddRange(optionData);
        }
    }
}
