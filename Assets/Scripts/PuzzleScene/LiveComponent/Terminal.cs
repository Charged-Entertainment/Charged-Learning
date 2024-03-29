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

            private void Awake()
            {
                terminal = gameObject.GetComponent<Terminal>();
            }

            private void OnMouseDown()
            {
                Terminal.mouseDown?.Invoke(terminal);
                mouseDownStartPosition = Utils.GetMouseWorldPosition();
            }

            private void OnMouseUp()
            {
                Terminal.mouseUp?.Invoke(terminal);
                var currPos = Utils.GetMouseWorldPosition();
                if (currPos == mouseDownStartPosition) terminal.Disconnect();
                else
                {
                    if (currentlyHovered != null) terminal.Connect(currentlyHovered);
                }
            }

            private void OnMouseEnter()
            {
                Terminal.mouseEntered?.Invoke(terminal);
                currentlyHovered = terminal;
            }

            private void OnMouseExit()
            {
                Terminal.mouseExited?.Invoke(terminal);
                currentlyHovered = null;
            }

        }
        public HashSet<Terminal> connectedTerminals { get; private set; }
        public CircuitComponent parent;
        static public Action<Terminal> mouseEntered;
        static public Action<Terminal> mouseExited;
        static public Action<Terminal> mouseDown;
        static public Action<Terminal> mouseUp;
        static public Action<Terminal> destroyed;
        static public Action<Terminal, Terminal> connected;
        static public Action<Terminal, Terminal> disconnected;

        static public bool Enabled { get; private set; } = true;

        static public void SetEnabled(bool enabled) { Enabled = enabled; }
        static public void Disable() { Enabled = false; }
        static public void Enable() { Enabled = true; }

        private Dictionary<Terminal, float> connectionCandidates;
        private static float minTimeToConnectCollidedTerminal = 0.3f;

        private TerminalController controller;

        private void Awake()
        {
            connectedTerminals = new HashSet<Terminal>();
            controller = gameObject.AddComponent<TerminalController>();
            connectionCandidates = new Dictionary<Terminal, float>();
            parent = GetComponentInParent<CircuitComponent>();
        }

        ///<summary> 
        /// If otherTerminal is null, will disconnect all terminals connected to this.
        ///</summary>
        public void Disconnect(Terminal otherTerminal = null, bool silent = false)
        {
            // disconnect only one terminal
            if (otherTerminal != null)
            {
                if (connectedTerminals.Contains(otherTerminal))
                {
                    var t = otherTerminal;
                    connectedTerminals.Remove(otherTerminal);
                    if (!silent)
                    {
                        Terminal.disconnected?.Invoke(this, t);
                        if (otherTerminal.connectedTerminals.Contains(this)) t.Disconnect(this, true);
                    }
                }
            }
            else
            {
                List<Terminal> ts = new List<Terminal>();
                foreach (var t in connectedTerminals) ts.Add(t);
                foreach (var t in ts) Disconnect(t);
            }
        }

        public void Connect(Terminal terminal, bool silent = false)
        {
            if (terminal != this)
            {
                if (connectedTerminals.Contains(terminal)) { Debug.Log("Already connected."); return; }
                if (terminal.transform.parent == transform.parent) Debug.Log("Warning: connecting terminals on the same component!");
                connectedTerminals.Add(terminal);
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

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (Enabled)
            {
                var t = other.gameObject.GetComponent<Terminal>();
                if (t != null && !connectedTerminals.Contains(t)) connectionCandidates.Add(t, 0f);
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (Enabled)
            {
                var t = other.gameObject.GetComponent<Terminal>();
                if (t != null && connectionCandidates.ContainsKey(t))
                {
                    connectionCandidates[t] += Time.fixedDeltaTime;
                    if (connectionCandidates[t] >= minTimeToConnectCollidedTerminal)
                    {
                        Connect(t);
                        connectionCandidates.Remove(t);
                    }
                }
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (Enabled)
            {
                var t = other.gameObject.GetComponent<Terminal>();
                if (t != null && connectionCandidates.ContainsKey(t)) connectionCandidates.Remove(t);
            }
        }

        private void OnDestroy() { destroyed?.Invoke(this); }
    }
}