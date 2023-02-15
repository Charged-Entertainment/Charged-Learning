using System;
using UnityEngine;
using GameManagement;

public partial class EditorBehaviour
{
    private class EditorBehaviourControllersManager : Singleton<Controller>
    {
        private static DragController dragController;
        private static TransformationController transformationController;

        new void Awake()
        {
            base.Awake();
            dragController = gameObject.AddComponent<DragController>();
            transformationController = gameObject.AddComponent<TransformationController>();
        }
        private void OnEnable()
        {
            OnDisable();
            GameMode.changed += HandleGameModeChange;
            InteractionMode.changed += HandleInteractionModeChange;
        }

        private void OnDisable()
        {
            GameMode.changed -= HandleGameModeChange;
            InteractionMode.changed -= HandleInteractionModeChange;
        }

        private void HandleGameModeChange(GameModes mode)
        {
            HandleGameModeOrInteractionModeChange();
        }

        private void HandleInteractionModeChange(InteractionModes mode)
        {
            HandleGameModeOrInteractionModeChange();
        }

        private void HandleGameModeOrInteractionModeChange()
        {
            if (GameMode.Current != GameModes.Edit)
            {
                dragController.enabled = false;
                transformationController.enabled = false;
                return;
            }

            if (InteractionMode.Current == InteractionModes.Normal)
            {
                dragController.enabled = true;
                transformationController.enabled = true;
            }
            else if (InteractionMode.Current == InteractionModes.Pan)
            {
                dragController.enabled = false;
                transformationController.enabled = true;
            }
            else if (InteractionMode.Current == InteractionModes.Wire)
            {
                dragController.enabled = false;
                transformationController.enabled = false;
            }
            else
            {
                Debug.Log("Error: unknown InteractionMode: " + InteractionMode.Current);
            }
        }
    }
}