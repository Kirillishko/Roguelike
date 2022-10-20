using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ReBindUI : MonoBehaviour
{
    [SerializeField] private InputActionReference _inputActionReference;
    [SerializeField] private bool _excludeMouse = true;
    [SerializeField, Range(0, 10)] private int _selectedBinding;
    [SerializeField] private InputBinding.DisplayStringOptions _displayStringOptions;
    
    [Header("Binding Info - DO NOT EDIT")]
    [SerializeField] private InputBinding _inputBinding;

    [Header("UI Fields")]
    // [SerializeField] private Text _actionText;
    [SerializeField] private Button _rebindButton;
    // [SerializeField] private Text _rebindText;
    // [SerializeField] private Button _resetButton;
    [SerializeField] private HorizontalLayoutGroup _horizontalLayoutGroup;
    [SerializeField] private Image _keyView;

    private InputManager _input;
    private int _bindingIndex;
    private string _actionName;
    private int _uiSizeX;
    private string _keyName;

    private void Awake()
    {
        _actionName = _inputBinding.action;

        _uiSizeX = (int)(_horizontalLayoutGroup.spacing + _keyView.rectTransform.rect.width);
    }

    private void OnEnable()
    {
        if (_input == null)
            _input = InputManager.Instance;

        _rebindButton.onClick.AddListener(() => DoRebind());
        //_resetButton.onClick.AddListener(() => ResetBinding());

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
        // if (_actionText != null)
        //     _actionText.text = _actionName;
        //
        // if (_rebindText != null)
        // {
        //     if (Application.isPlaying)
        //         _rebindText.text = _input.GetBindingName(_actionName, _bindingIndex);
        //     else
        //         _rebindText.text = _inputActionReference.action.GetBindingDisplayString(_bindingIndex);
        // }

        if (_keyView != null && _input != null)
        {
            _keyName = _input.GetBindingName(_actionName, _bindingIndex);

            var currentKeySize = Mathf.CeilToInt(_keyView.sprite.bounds.size.x * 100);
            
            var newKey = _input.GetButtonImage(_keyName);
            var newKeySize = Mathf.CeilToInt(newKey.bounds.size.x * 100);

            if (currentKeySize != newKeySize)
            {
                var ratio = (float)currentKeySize / newKeySize;
                
                var rectTransform = _keyView.rectTransform;
                var width = rectTransform.rect.width / ratio;
                var height = rectTransform.rect.height;

                rectTransform.sizeDelta = new Vector2(width, height);
                _horizontalLayoutGroup.spacing = _uiSizeX - rectTransform.rect.width;
            }
            
            _keyView.sprite = newKey;
        }
    }

    private void DoRebind()
    {
        _input.StartRebind(_actionName, _bindingIndex, /*_rebindText, */_excludeMouse);
    }

    private void ResetBinding()
    {
        _input.ResetBinding(_actionName, _bindingIndex);
        UpdateUI();
    }
}
