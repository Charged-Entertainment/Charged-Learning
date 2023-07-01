using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogs;
using Entry = Dialogs.DialogEntry;
using System;
using UnityEngine.UIElements;
abstract public class Tutorial : MonoBehaviour
{
    protected List<Entry> entries;
    virtual protected void Awake()
    {
        entries = new List<Entry>();
    }

    /// <summary>
    /// Subscribe to an action with a handler.
    /// If your handler returns true, it'll call your 'clean' handler.
    /// Otherwise, it'll continue its subscription.
    /// </summary>
    virtual protected Action<T> Handle<T>(ref Action<T> action, Func<T, bool> handle, Action<Action<T>> clean)
    {
        Action<T> _handler = null;
        _handler = c => { if (handle(c)) clean(_handler); };
        action += _handler;
        return _handler;
    }

    virtual protected Action Handle(ref Action action, Func<bool> handle, Action<Action> clean)
    {
        Action _handler = null;
        _handler = () => { if (handle()) clean(_handler); };
        action += _handler;
        return _handler;
    }

    virtual protected Action<T, T2> Handle<T, T2>(ref Action<T, T2> action, Func<T, T2, bool> handle, Action<Action<T, T2>> clean)
    {
        Action<T, T2> _handler = null;
        _handler = (c, c2) => { if (handle(c, c2)) clean(_handler); };
        action += _handler;
        return _handler;
    }

    virtual protected void ContinueOnClick(VisualElement v)
    {
        EventCallback<ClickEvent> doOnce = null;
        doOnce = ev =>
        {
            Dialog.Continue();
            v.UnregisterCallback<ClickEvent>(doOnce);
        };
        v.RegisterCallback<ClickEvent>(doOnce);
    }

    virtual protected Entry AddEntry(RichText content)
    {
        var t = new Entry(content);
        entries.Add(t);
        return t;
    }

    virtual protected void PlaySequence(Action addEntries)
    {
        addEntries();
        entries.Insert(0, new DialogEntry("init"));
        Dialog.PlaySequence(new DialogSequence(entries));
        entries = new List<Entry>();
    }

    private IEnumerator DoAfterSeconds(float seconds, Action action = null)
    {
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
    }

    virtual protected void Wait(float seconds) {
        StartCoroutine(DoAfterSeconds(seconds));
    }
    
    virtual protected void WaitThenDo(float seconds, Action action)
    {
        StartCoroutine(DoAfterSeconds(seconds, action));
    }

    virtual protected IEnumerator WaitThenDo<T>(float seconds, Action<T> action, T arg)
    {
        yield return new WaitForSeconds(seconds);
        action?.Invoke(arg);
    }

    virtual protected IEnumerator WaitThenDo<T, T2>(float seconds, Action<T, T2> action, T arg1, T2 arg2)
    {
        yield return new WaitForSeconds(seconds);
        action?.Invoke(arg1, arg2);
    }

}
