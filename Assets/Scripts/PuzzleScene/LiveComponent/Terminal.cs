using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManagement;

namespace Components
{
    public class Terminal : MonoBehaviour
    {
        private class TerminalController : Controller
        {
            Terminal terminal;

            Vector3 mouseDownStartPosition;

            private static Terminal currentlyHovered;
            private bool mouseDown = false;

            private void Awake()
            {
                terminal = gameObject.GetComponent<Terminal>();
            }

            private void OnMouseDown()
            {
                Terminal.mouseDown?.Invoke(terminal);
                mouseDownStartPosition = Utils.GetMouseWorldPosition();
                mouseDown = true;
                terminal.GetComponent<SpriteRenderer>().color = Color.red;
            }

            private void OnMouseUp()
            {
                Terminal.mouseUp?.Invoke(terminal);
                var currPos = Utils.GetMouseWorldPosition();
                if (currPos == mouseDownStartPosition) terminal.Disconnect();
                else
                {
                    if (currentlyHovered != null) terminal.Connect(currentlyHovered);
                    else terminal.Disconnect();
                }

                mouseDown = false;
                terminal.GetComponent<SpriteRenderer>().color = terminal.connection ? Color.green : Color.white;
            }

            private void OnMouseEnter()
            {
                Terminal.mouseEntered?.Invoke(terminal);
                currentlyHovered = terminal;
                terminal.GetComponent<SpriteRenderer>().color = Color.gray;
            }

            private void OnMouseExit()
            {
                Terminal.mouseExited?.Invoke(terminal);
                if (!mouseDown) terminal.GetComponent<SpriteRenderer>().color = terminal.connection ? Color.green : Color.white;
                currentlyHovered = null;
            }

        }


        public Terminal connection { get; private set; }

        static public Action<Terminal> mouseEntered;
        static public Action<Terminal> mouseExited;
        static public Action<Terminal> mouseDown;
        static public Action<Terminal> mouseUp;
        static public Action<Terminal> destroyed;
        static public Action<Terminal, Terminal> connected;
        static public Action<Terminal, Terminal> disconnected;

        private TerminalController controller;

        private void Awake()
        {
            controller = gameObject.AddComponent<TerminalController>();
        }

        public void Disconnect(bool silent = false)
        {
            if (connection != null)
            {
                var t = connection;
                connection = null;
                if (!silent)
                {
                    Terminal.disconnected?.Invoke(this, t);
                    if (t.connection == this) t.Disconnect(true);
                }
            }
        }

        public void Connect(Terminal terminal, bool silent = false)
        {
            if (terminal != this)
            {
                if (terminal.transform.parent == transform.parent) Debug.Log("Warning: connecting terminals on the same component!");
                if (connection != null) Disconnect();
                connection = terminal;
                if (!silent)
                {
                    Terminal.connected?.Invoke(this, terminal);
                    terminal.Connect(this, true);
                }
            }
            else
            {
                Debug.Log("Error: cannot connect a termnial to its self.");
            }
        }

        private void OnEnable()
        {
            OnDisable();
            InteractionMode.changed += HandleInteractionModeChange;
        }

        private void OnDisable()
        {
            InteractionMode.changed -= HandleInteractionModeChange;
        }

        private void Enable()
        {
            controller.enabled = true;
        }

        private void Disable()
        {
            controller.enabled = false;
        }

        private void HandleInteractionModeChange(InteractionModes mode)
        {
            if (InteractionMode.Current == InteractionModes.Wire) Enable();
            else Disable();
        }

        private void OnDestroy() { destroyed?.Invoke(this); }
    }
}