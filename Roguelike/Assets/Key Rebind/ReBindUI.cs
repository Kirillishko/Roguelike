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

    [Header("UI Fields")]
    [SerializeField] private Text _actionText;
    [SerializeField] private Button _rebindButton;
    [SerializeField] private Text _rebindText;
    [SerializeField] private Button _resetButton;

    private InputManager _input;
    private int _bindingIndex;
    private string _actionName;
    
    

    private void OnEnable()
    {
        if (_input == null)
            _input = InputManager.Instance;

        _rebindButton.onClick.AddListener(() => DoRebind());
        _resetButton.onClick.AddListener(() => ResetBinding());

        if(_inputActionReference != null)
        {
            _input.LoadBindingOverride(_actionName);
            GetBindingInfo();
            UpdateUI();
        }

        _input.RebindComplete += UpdateUI;
        _input.RebindCanceled += UpdateUI;
    }

    private void OnDisable()
    {
        _input.RebindComplete -= UpdateUI;
        _input.RebindCanceled -= UpdateUI;
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
                _rebindText.text = _input.GetBindingName(_actionName, _bindingIndex);
            else
                _rebindText.text = _inputActionReference.action.GetBindingDisplayString(_bindingIndex);
        }
    }

    private void DoRebind()
    {
        _input.StartRebind(_actionName, _bindingIndex, _rebindText, _excludeMouse);
    }

    private void ResetBinding()
    {
        _input.ResetBinding(_actionName, _bindingIndex);
        UpdateUI();
    }
}
