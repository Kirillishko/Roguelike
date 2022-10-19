// #if UNITY_EDITOR
// using System.Linq;
// using UnityEditor;
// using UnityEngine;
// using UnityEngine.InputSystem;
//
//
// [CustomEditor(typeof(RebindingUI))]
// public class RebindingUIEditor : UnityEditor.Editor
// {
//     private SerializedProperty _inputActionReference;
//     private SerializedProperty _bind;
//     private SerializedProperty _excludeMouse;
//     private SerializedProperty _actionText;
//     private SerializedProperty _rebindButton;
//     private SerializedProperty _rebindText;
//     private SerializedProperty _resetButton;
//
//     private GUIContent _bindingLabel = new GUIContent("Binding");
//     private GUIContent[] _bindingOptions;
//     private string[] _bindingOptionValues;
//     private int _selectedBindingOption;
//
//     protected void OnEnable()
//     {
//         _inputActionReference = serializedObject.FindProperty("_inputActionReference");
//         _bind = serializedObject.FindProperty("_bind");
//         _excludeMouse = serializedObject.FindProperty("_excludeMouse");
//         _actionText = serializedObject.FindProperty("_actionText");
//         _rebindButton = serializedObject.FindProperty("_rebindButton");
//         _rebindText = serializedObject.FindProperty("_rebindText");
//         _resetButton = serializedObject.FindProperty("_resetButton");
//
//         RefreshBindingOptions();
//     }
//
//     public override void OnInspectorGUI()
//     {
//         EditorGUI.BeginChangeCheck();
//         
//         EditorGUILayout.LabelField(_bindingLabel, Styles.boldLabel);
//         using (new EditorGUI.IndentLevelScope())
//         {
//             EditorGUILayout.PropertyField(_inputActionReference);
//
//             var newSelectedBinding = EditorGUILayout.Popup(_bindingLabel, _selectedBindingOption, _bindingOptions);
//             if (newSelectedBinding != _selectedBindingOption)
//             {
//                 var bindingId = _bindingOptionValues[newSelectedBinding];
//                 _bind.stringValue = bindingId;
//                 _selectedBindingOption = newSelectedBinding;
//             }
//         }
//
//         EditorGUILayout.PropertyField(_excludeMouse);
//         EditorGUILayout.PropertyField(_actionText);
//         EditorGUILayout.PropertyField(_rebindButton);
//         EditorGUILayout.PropertyField(_rebindText);
//         EditorGUILayout.PropertyField(_resetButton);
//         
//
//         if (EditorGUI.EndChangeCheck())
//         {
//             serializedObject.ApplyModifiedProperties();
//             RefreshBindingOptions();
//         }
//     }
//
//     protected void RefreshBindingOptions()
//     {
//         var actionReference = (InputActionReference)_inputActionReference.objectReferenceValue;
//         var action = actionReference?.action;
//
//         if (action == null)
//         {
//             _bindingOptions = new GUIContent[0];
//             _bindingOptionValues = new string[0];
//             _selectedBindingOption = -1;
//             return;
//         }
//
//         var bindings = action.bindings;
//         var bindingCount = bindings.Count;
//
//         _bindingOptions = new GUIContent[bindingCount];
//         _bindingOptionValues = new string[bindingCount];
//         _selectedBindingOption = -1;
//
//         var currentBindingId = _bind.stringValue;
//         for (var i = 0; i < bindingCount; ++i)
//         {
//             var binding = bindings[i];
//             var bindingId = binding.id.ToString();
//             var haveBindingGroups = !string.IsNullOrEmpty(binding.groups);
//             
//             var displayOptions =
//                 InputBinding.DisplayStringOptions.DontUseShortDisplayNames | InputBinding.DisplayStringOptions.IgnoreBindingOverrides;
//             if (!haveBindingGroups)
//                 displayOptions |= InputBinding.DisplayStringOptions.DontOmitDevice;
//             
//             var displayString = action.GetBindingDisplayString(i, displayOptions);
//             
//             if (binding.isPartOfComposite)
//                 displayString = $"{ObjectNames.NicifyVariableName(binding.name)}: {displayString}";
//             
//             displayString = displayString.Replace('/', '\\');
//             
//             if (haveBindingGroups)
//             {
//                 var asset = action.actionMap?.asset;
//                 if (asset != null)
//                 {
//                     var controlSchemes = string.Join(", ",
//                         binding.groups.Split(InputBinding.Separator)
//                             .Select(x => asset.controlSchemes.FirstOrDefault(c => c.bindingGroup == x).name));
//
//                     displayString = $"{displayString} ({controlSchemes})";
//                 }
//             }
//
//             _bindingOptions[i] = new GUIContent(displayString);
//             _bindingOptionValues[i] = bindingId;
//
//             if (currentBindingId == bindingId)
//                 _selectedBindingOption = i;
//         }
//     }
//
//     private static class Styles
//     {
//         public static GUIStyle boldLabel = new GUIStyle("MiniBoldLabel");
//     }
// }
//
// #endif
