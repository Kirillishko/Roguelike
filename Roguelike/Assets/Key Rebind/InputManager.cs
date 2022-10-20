using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.U2D;

public class InputManager : MonoBehaviour
{
    [SerializeField] private SpriteAtlas _spriteAtlas;
    
    public static InputManager Instance { get; private set; }

    public event Action RebindComplete;
    public event Action RebindCanceled;
    public event Action<InputAction, int> RebindStarted;
    
    public InputActions InputActions { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
                //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        InputActions = new InputActions();
        InputActions.Player.Enable();
    }

    public void StartRebind(string actionName, int bindingIndex, /*Text statusText,*/ bool excludeMouse)
    {
        var action = InputActions.asset.FindAction(actionName);
        
        if (action == null || action.bindings.Count <= bindingIndex)
        {
            Debug.Log("Couldn't find action or binding");
            return;
        }

        if (action.bindings[bindingIndex].isComposite)
        {
            var firstPartIndex = bindingIndex + 1;
            
            if (firstPartIndex < action.bindings.Count && action.bindings[firstPartIndex].isComposite)
                DoRebind(action, bindingIndex, /*statusText,*/ true, excludeMouse);
        }
        else
        {
            DoRebind(action, bindingIndex, /*statusText,*/ false, excludeMouse);
        }
    }

    private void DoRebind(InputAction actionToRebind, int bindingIndex, /*Text statusText,*/ bool allCompositeParts, bool excludeMouse)
    {
        if (actionToRebind == null || bindingIndex < 0)
            return;

        //statusText.text = $"Press a {actionToRebind.expectedControlType}";

        actionToRebind.Disable();

        var rebind = actionToRebind.PerformInteractiveRebinding(bindingIndex);

        rebind.OnComplete(operation =>
        {
            actionToRebind.Enable();
            operation.Dispose();

            if(allCompositeParts)
            {
                var nextBindingIndex = bindingIndex + 1;
                if (nextBindingIndex < actionToRebind.bindings.Count && actionToRebind.bindings[nextBindingIndex].isComposite)
                    DoRebind(actionToRebind, nextBindingIndex, /*statusText, */true, excludeMouse);
            }

            SaveBindingOverride(actionToRebind);
            RebindComplete?.Invoke();
        });

        rebind.OnCancel(operation =>
        {
            actionToRebind.Enable();
            operation.Dispose();

            RebindCanceled?.Invoke();
        });

        rebind.WithCancelingThrough("<Keyboard>/escape");

        if (excludeMouse)
            rebind.WithControlsExcluding("Mouse");

        RebindStarted?.Invoke(actionToRebind, bindingIndex);
        rebind.Start();
    }

    public string GetBindingName(string actionName, int bindingIndex)
    {
        var action = InputActions.asset.FindAction(actionName);
        return action.bindings[0].overridePath;
        //return action.GetBindingDisplayString(bindingIndex);
    }

    private void SaveBindingOverride(InputAction action)
    {
        for (int i = 0; i < action.bindings.Count; i++)
        {
            PlayerPrefs.SetString(action.actionMap + action.name + i, action.bindings[i].overridePath);
        }
    }

    public void LoadBindingOverride(string actionName)
    {
        InputActions ??= new InputActions();
        
        var action = InputActions.asset.FindAction(actionName);

        for (int i = 0; i < action.bindings.Count; i++)
        {
            if (!string.IsNullOrEmpty(PlayerPrefs.GetString(action.actionMap + action.name + i)))
                action.ApplyBindingOverride(i, PlayerPrefs.GetString(action.actionMap + action.name + i));
        }
    }

    public void ResetBinding(string actionName, int bindingIndex)
    {
        var action = InputActions.asset.FindAction(actionName);

        if(action == null || action.bindings.Count <= bindingIndex)
        {
            Debug.Log("Could not find action or binding");
            return;
        }

        if (action.bindings[bindingIndex].isComposite)
        {
            for (int i = bindingIndex; i < action.bindings.Count && action.bindings[i].isComposite; i++)
                action.RemoveBindingOverride(i);
        }
        else
        {
            action.RemoveBindingOverride(bindingIndex);
        }

        SaveBindingOverride(action);
    }

    public Sprite GetButtonImage(string key)
    {
        var startIndex = key.IndexOf("/", StringComparison.Ordinal) + 1;
        var keyName = key[startIndex..];
        
        return _spriteAtlas.GetSprite(keyName);
    }

}
