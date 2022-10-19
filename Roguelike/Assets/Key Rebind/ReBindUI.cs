using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ReBindUI : MonoBehaviour
{
    [SerializeField] private InputActionReference _inputActionReference; //this is on the SO
    [SerializeField] private bool _excludeMouse = true;
    [SerializeField, Range(0, 10)] private int _selectedBinding;
    [SerializeField] private InputBinding.DisplayStringOptions _displayStringOptions;
    
    [Header("Binding Info - DO NOT EDIT")]
    [SerializeField] private InputBinding _inputBinding;
    private int _bindingIndex;

    private string _actionName;

    [Header("UI Fields")]
    [SerializeField] private Text _actionText;
    [SerializeField] private Button _rebindButton;
    [SerializeField] private Text _rebindText;
    [SerializeField] private Button _resetButton;

    private void OnEnable()
    {
        _rebindButton.onClick.AddListener(() => DoRebind());
        _resetButton.onClick.AddListener(() => ResetBinding());

        if(_inputActionReference != null)
        {
            InputManager.LoadBindingOverride(_actionName);
            GetBindingInfo();
            UpdateUI();
        }

        InputManager.RebindComplete += UpdateUI;
        InputManager.RebindCanceled += UpdateUI;
    }

    private void OnDisable()
    {
        InputManager.RebindComplete -= UpdateUI;
        InputManager.RebindCanceled -= UpdateUI;
    }

    private void OnValidate()
    {
        if (_inputActionReference == null)
            return; 

        GetBindingInfo();
        UpdateUI();
    }

    private void GetBindingInfo()
    {
        if (_inputActionReference.action == null)
            return;

        if (_inputActionReference.action.bindings.Count > _selectedBinding)
        {
            _inputBinding = _inputActionReference.action.bindings[_selectedBinding];
            _bindingIndex = _selectedBinding;
        }
    }

    private void UpdateUI()
    {
        if (_actionText != null)
            _actionText.text = _actionName;

        if (_rebindText != null)
        {
            if (Application.isPlaying)
                _rebindText.text = InputManager.GetBindingName(_actionName, _bindingIndex);
            else
                _rebindText.text = _inputActionReference.action.GetBindingDisplayString(_bindingIndex);
        }
    }

    private void DoRebind()
    {
        InputManager.StartRebind(_actionName, _bindingIndex, _rebindText, _excludeMouse);
    }

    private void ResetBinding()
    {
        InputManager.ResetBinding(_actionName, _bindingIndex);
        UpdateUI();
    }
}
