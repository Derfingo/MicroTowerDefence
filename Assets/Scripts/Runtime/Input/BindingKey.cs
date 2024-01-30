using UnityEngine.InputSystem;
using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;
using UnityEngine.Events;
using static UnityEngine.InputSystem.InputActionRebindingExtensions;

public class BindingKey : MonoBehaviour
{
    [Tooltip("Reference to action that is to be rebound from the UI.")]
    [SerializeField] private InputActionReference _inputActionMap;
    [SerializeField] private string _bindingId;
    [SerializeField] private InputBinding.DisplayStringOptions _displayStringOptions;

    [Tooltip("Text label that will receive the name of the action. Optional. Set to None to have the "
        + "rebind UI not show a label for the action.")]
    [SerializeField] private TMP_Text _rebindNameText;

    [Tooltip("Text label that will receive the current, formatted binding string.")]
    [SerializeField] private TMP_Text _bindingButtonText;

    [Tooltip("Optional UI that will be shown while a rebind is in progress.")]
    [SerializeField] private GameObject _rebindingOverlayView;

    [Tooltip("Optional text label that will be updated with prompt for user input.")]
    [SerializeField] private TMP_Text _rebindingOverlayText;

    [Tooltip("Event that is triggered when the way the binding is display should be updated. This allows displaying "
        + "bindings in custom ways, e.g. using images instead of text.")]
    [SerializeField] private UpdateBindingUIEvent m_UpdateBindingUIEvent;

    [Tooltip("Event that is triggered when an interactive rebind is being initiated. This can be used, for example, "
        + "to implement custom UI behavior while a rebind is in progress. It can also be used to further "
        + "customize the rebind.")]
    [SerializeField] private InteractiveRebindEvent m_RebindStartEvent;

    [Tooltip("Event that is triggered when an interactive rebind is complete or has been aborted.")]
    [SerializeField] private InteractiveRebindEvent m_RebindStopEvent;

    private RebindingOperation m_RebindOperation;

    private static List<BindingKey> s_RebindActionUIs;

    /// <summary>
    /// Reference to the action that is to be rebound.
    /// </summary>
    public InputActionReference ActionReference
    {
        get => _inputActionMap;
        set
        {
            _inputActionMap = value;
            UpdateActionLabel();
            UpdateBindingDisplay();
        }
    }

    /// <summary>
    /// ID (in string form) of the binding that is to be rebound on the action.
    /// </summary>
    /// <seealso cref="InputBinding.id"/>
    public string BindingId
    {
        get => _bindingId;
        set
        {
            _bindingId = value;
            UpdateBindingDisplay();
        }
    }

    public InputBinding.DisplayStringOptions DisplayStringOptions
    {
        get => _displayStringOptions;
        set
        {
            _displayStringOptions = value;
            UpdateBindingDisplay();
        }
    }

    /// <summary>
    /// Text component that receives the name of the action. Optional.
    /// </summary>
    public TMP_Text RebindNameText
    {
        get => _rebindNameText;
        set
        {
            _rebindNameText = value;
            UpdateActionLabel();
        }
    }

    /// <summary>
    /// Text component that receives the display string of the binding. Can be <c>null</c> in which
    /// case the component entirely relies on <see cref="updateBindingUIEvent"/>.
    /// </summary>
    public TMP_Text BindingButtonText
    {
        get => _bindingButtonText;
        set
        {
            _bindingButtonText = value;
            UpdateBindingDisplay();
        }
    }

    /// <summary>
    /// Optional text component that receives a text prompt when waiting for a control to be actuated.
    /// </summary>
    /// <seealso cref="startRebindEvent"/>
    /// <seealso cref="rebindOverlay"/>
    public TMP_Text RebindingOverlayText
    {
        get => _rebindingOverlayText;
        set => _rebindingOverlayText = value;
    }

    /// <summary>
    /// Optional UI that is activated when an interactive rebind is started and deactivated when the rebind
    /// is finished. This is normally used to display an overlay over the current UI while the system is
    /// waiting for a control to be actuated.
    /// </summary>
    /// <remarks>
    /// If neither <see cref="RebindingOverlayText"/> nor <c>rebindOverlay</c> is set, the component will temporarily
    /// replaced the <see cref="BindingButtonText"/> (if not <c>null</c>) with <c>"Waiting..."</c>.
    /// </remarks>
    /// <seealso cref="startRebindEvent"/>
    /// <seealso cref="RebindingOverlayText"/>
    public GameObject rebindOverlay
    {
        get => _rebindingOverlayView;
        set => _rebindingOverlayView = value;
    }

    /// <summary>
    /// Event that is triggered every time the UI updates to reflect the current binding.
    /// This can be used to tie custom visualizations to bindings.
    /// </summary>
    public UpdateBindingUIEvent updateBindingUIEvent
    {
        get
        {
            if (m_UpdateBindingUIEvent == null)
                m_UpdateBindingUIEvent = new UpdateBindingUIEvent();
            return m_UpdateBindingUIEvent;
        }
    }

    /// <summary>
    /// Event that is triggered when an interactive rebind is started on the action.
    /// </summary>
    public InteractiveRebindEvent startRebindEvent
    {
        get
        {
            if (m_RebindStartEvent == null)
                m_RebindStartEvent = new InteractiveRebindEvent();
            return m_RebindStartEvent;
        }
    }

    /// <summary>
    /// Event that is triggered when an interactive rebind has been completed or canceled.
    /// </summary>
    public InteractiveRebindEvent stopRebindEvent
    {
        get
        {
            if (m_RebindStopEvent == null)
                m_RebindStopEvent = new InteractiveRebindEvent();
            return m_RebindStopEvent;
        }
    }

    /// <summary>
    /// When an interactive rebind is in progress, this is the rebind operation controller.
    /// Otherwise, it is <c>null</c>.
    /// </summary>
    public RebindingOperation ongoingRebind => m_RebindOperation;

    /// <summary>
    /// Return the action and binding index for the binding that is targeted by the component
    /// according to
    /// </summary>
    /// <param name="action"></param>
    /// <param name="bindingIndex"></param>
    /// <returns></returns>
    public bool ResolveActionAndBinding(out InputAction action, out int bindingIndex)
    {
        bindingIndex = -1;

        action = _inputActionMap?.action;
        if (action == null)
            return false;

        if (string.IsNullOrEmpty(_bindingId))
            return false;

        // Look up binding index.
        var bindingId = new Guid(_bindingId);
        bindingIndex = action.bindings.IndexOf(x => x.id == bindingId);
        if (bindingIndex == -1)
        {
            Debug.LogError($"Cannot find binding with ID '{bindingId}' on '{action}'", this);
            return false;
        }

        return true;
    }

    /// <summary>
    /// Trigger a refresh of the currently displayed binding.
    /// </summary>
    public void UpdateBindingDisplay()
    {
        var displayString = string.Empty;
        var deviceLayoutName = default(string);
        var controlPath = default(string);

        // Get display string from action.
        var action = _inputActionMap?.action;
        if (action != null)
        {
            var bindingIndex = action.bindings.IndexOf(x => x.id.ToString() == _bindingId);
            if (bindingIndex != -1)
                displayString = action.GetBindingDisplayString(bindingIndex, out deviceLayoutName, out controlPath, DisplayStringOptions);
        }

        // Set on label (if any).
        if (_bindingButtonText != null)
            _bindingButtonText.text = displayString;

        // Give listeners a chance to configure UI in response.
        m_UpdateBindingUIEvent?.Invoke(this, displayString, deviceLayoutName, controlPath);
    }

    /// <summary>
    /// Remove currently applied binding overrides.
    /// </summary>
    public void ResetToDefault()
    {
        if (!ResolveActionAndBinding(out var action, out var bindingIndex))
            return;

        if (action.bindings[bindingIndex].isComposite)
        {
            // It's a composite. Remove overrides from part bindings.
            for (var i = bindingIndex + 1; i < action.bindings.Count && action.bindings[i].isPartOfComposite; ++i)
                action.RemoveBindingOverride(i);
        }
        else
        {
            action.RemoveBindingOverride(bindingIndex);
        }
        UpdateBindingDisplay();
    }

    /// <summary>
    /// Initiate an interactive rebind that lets the player actuate a control to choose a new binding
    /// for the action.
    /// </summary>
    public void StartInteractiveRebind()
    {
        if (!ResolveActionAndBinding(out var action, out var bindingIndex))
            return;

        // If the binding is a composite, we need to rebind each part in turn.
        if (action.bindings[bindingIndex].isComposite)
        {
            var firstPartIndex = bindingIndex + 1;
            if (firstPartIndex < action.bindings.Count && action.bindings[firstPartIndex].isPartOfComposite)
                PerformInteractiveRebind(action, firstPartIndex, allCompositeParts: true);
        }
        else
        {
            PerformInteractiveRebind(action, bindingIndex);
        }
    }

    private void PerformInteractiveRebind(InputAction action, int bindingIndex, bool allCompositeParts = false)
    {
        m_RebindOperation?.Cancel(); // Will null out m_RebindOperation.

        void CleanUp()
        {
            m_RebindOperation?.Dispose();
            m_RebindOperation = null;
        }

        // Configure the rebind.
        m_RebindOperation = action.PerformInteractiveRebinding(bindingIndex)
            .OnCancel(
                operation =>
                {
                    m_RebindStopEvent?.Invoke(this, operation);
                    _rebindingOverlayView?.SetActive(false);
                    UpdateBindingDisplay();
                    CleanUp();
                })
            .OnComplete(
                operation =>
                {
                    _rebindingOverlayView?.SetActive(false);
                    m_RebindStopEvent?.Invoke(this, operation);
                    UpdateBindingDisplay();
                    CleanUp();

                    // If there's more composite parts we should bind, initiate a rebind
                    // for the next part.
                    if (allCompositeParts)
                    {
                        var nextBindingIndex = bindingIndex + 1;
                        if (nextBindingIndex < action.bindings.Count && action.bindings[nextBindingIndex].isPartOfComposite)
                            PerformInteractiveRebind(action, nextBindingIndex, true);
                    }
                });

        // If it's a part binding, show the name of the part in the UI.
        var partName = default(string);
        if (action.bindings[bindingIndex].isPartOfComposite)
            partName = $"Binding '{action.bindings[bindingIndex].name}'. ";

        // Bring up rebind overlay, if we have one.
        _rebindingOverlayView?.SetActive(true);
        if (_rebindingOverlayText != null)
        {
            var text = !string.IsNullOrEmpty(m_RebindOperation.expectedControlType)
                ? $"{partName}Waiting for {m_RebindOperation.expectedControlType} input..."
                : $"{partName}Waiting for input...";
            _rebindingOverlayText.text = text;
        }

        // If we have no rebind overlay and no callback but we have a binding text label,
        // temporarily set the binding text label to "<Waiting>".
        if (_rebindingOverlayView == null && _rebindingOverlayText == null && m_RebindStartEvent == null && _bindingButtonText != null)
            _bindingButtonText.text = "<Waiting...>";

        // Give listeners a chance to act on the rebind starting.
        m_RebindStartEvent?.Invoke(this, m_RebindOperation);

        m_RebindOperation.Start();
    }

    protected void OnEnable()
    {
        if (s_RebindActionUIs == null)
            s_RebindActionUIs = new List<BindingKey>();
        s_RebindActionUIs.Add(this);
        if (s_RebindActionUIs.Count == 1)
            InputSystem.onActionChange += OnActionChange;
    }

    protected void OnDisable()
    {
        m_RebindOperation?.Dispose();
        m_RebindOperation = null;

        s_RebindActionUIs.Remove(this);
        if (s_RebindActionUIs.Count == 0)
        {
            s_RebindActionUIs = null;
            InputSystem.onActionChange -= OnActionChange;
        }
    }

    // When the action system re-resolves bindings, we want to update our UI in response. While this will
    // also trigger from changes we made ourselves, it ensures that we react to changes made elsewhere. If
    // the user changes keyboard layout, for example, we will get a BoundControlsChanged notification and
    // will update our UI to reflect the current keyboard layout.
    private static void OnActionChange(object obj, InputActionChange change)
    {
        if (change != InputActionChange.BoundControlsChanged)
            return;

        var action = obj as InputAction;
        var actionMap = action?.actionMap ?? obj as InputActionMap;
        var actionAsset = actionMap?.asset ?? obj as InputActionAsset;

        for (var i = 0; i < s_RebindActionUIs.Count; ++i)
        {
            var component = s_RebindActionUIs[i];
            var referencedAction = component.ActionReference?.action;
            if (referencedAction == null)
                continue;

            if (referencedAction == action ||
                referencedAction.actionMap == actionMap ||
                referencedAction.actionMap?.asset == actionAsset)
                component.UpdateBindingDisplay();
        }
    }

    

    // We want the label for the action name to update in edit mode, too, so
    // we kick that off from here.
#if UNITY_EDITOR
    protected void OnValidate()
    {
        UpdateActionLabel();
        UpdateBindingDisplay();
    }

#endif

    private void UpdateActionLabel()
    {
        if (_rebindNameText != null)
        {
            var action = _inputActionMap?.action;
            _rebindNameText.text = action != null ? action.name : string.Empty;
        }
    }

    [Serializable]
    public class UpdateBindingUIEvent : UnityEvent<BindingKey, string, string, string>
    {
    }

    [Serializable]
    public class InteractiveRebindEvent : UnityEvent<BindingKey, RebindingOperation>
    {
    }
}