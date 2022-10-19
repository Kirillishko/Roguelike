// using System;
// using System.Collections.Generic;
// using UnityEngine.Events;
// using UnityEngine.UI;
// using UnityEngine;
// using UnityEngine.InputSystem;
//
// public class RebindingUI : MonoBehaviour
// {
//     [SerializeField] private InputActionReference _inputActionReference; //this is on the SO
//     [SerializeField] private string _bind;
//     [SerializeField] private bool _excludeMouse = true;
//     [Range(0, 10)] [SerializeField] private int _selectedBinding;
//     [SerializeField] private InputBinding.DisplayStringOptions _displayStringOptions;
//
//     [Header("Binding Info - DO NOT EDIT")] [SerializeField]
//     private InputBinding inputBinding;
//
//     private int bindingIndex;
//     private string actionName;
//
//     [Header("UI Fields")] [SerializeField] private Text _actionText;
//     [SerializeField] private Button _rebindButton;
//     [SerializeField] private Text _rebindText;
//     [SerializeField] private Button _resetButton;
//
//     private void OnEnable()
//     {
//         _rebindButton.onClick.AddListener(() => DoRebind());
//         _resetButton.onClick.AddListener(() => ResetBinding());
//
//         if (_inputActionReference != null)
//         {
//             InputManager.LoadBindingOverride(actionName);
//             GetBindingInfo();
//             UpdateUI();
//         }
//
//         InputManager.RebindComplete += UpdateUI;
//         InputManager.RebindCanceled += UpdateUI;
//     }
//
//     private void OnDisable()
//     {
//         InputManager.RebindComplete -= UpdateUI;
//         InputManager.RebindCanceled -= UpdateUI;
//     }
//
//     private void OnValidate()
//     {
//         if (_inputActionReference == null)
//             return;
//
//         GetBindingInfo();
//         UpdateUI();
//     }
//
//     private void GetBindingInfo()
//     {
//         if (_inputActionReference.action != null)
//             actionName = _inputActionReference.action.name;
//
//         if (_inputActionReference.action.bindings.Count > _selectedBinding)
//         {
//             inputBinding = _inputActionReference.action.bindings[_selectedBinding];
//             bindingIndex = _selectedBinding;
//         }
//     }
//
//     private void UpdateUI()
//     {
//         if (_actionText != null)
//             _actionText.text = actionName;
//
//         if (_rebindText != null)
//         {
//             if (Application.isPlaying)
//             {
//                 _rebindText.text = InputManager.GetBindingName(actionName, bindingIndex);
//             }
//             else
//                 _rebindText.text = _inputActionReference.action.GetBindingDisplayString(bindingIndex);
//         }
//     }
//
//     private void DoRebind()
//     {
//         InputManager.StartRebind(actionName, bindingIndex, _rebindText, _excludeMouse);
//     }
//
//     private void ResetBinding()
//     {
//         InputManager.ResetBinding(actionName, bindingIndex);
//         UpdateUI();
//     }
// }
//
