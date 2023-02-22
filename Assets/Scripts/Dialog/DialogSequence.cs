using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Dialogs
{
    public class DialogSequence
    {
        public DialogEntry head { get; private set; }
        public DialogEntry curr { get; private set; }

        public int totalNumber { get; private set; } = 0;
        public int currentNumber { get; private set; } = 0;

        public DialogSequence(IList<DialogEntry> list)
        {
            if (list.Count < 1)
            {
                Debug.Log("Error: empty dialog entry");
                return;
            }

            head = list[0];
            curr = head;
            for (int i = 1; i < list.Count; i++) Add(list[i]);
        }

        public void Play() {
            Dialog.PlaySequence(this);
        }

        public DialogSequence(DialogEntry dialog)
        {
            Add(dialog);
            curr = head;
        }

        public void Add(DialogEntry dialog)
        {
            totalNumber++;
            head.next = dialog;
            head = dialog;
        }

        public void Next()
        {
            Dialog.entryEnded?.Invoke(curr);
            curr.ended?.Invoke();

            curr = curr.next;
            currentNumber++;
            if (curr != null)
            {
                Dialog.SetCurrent(curr);
                Dialog.entryStarted?.Invoke(curr);
                curr.started?.Invoke();
            }
            else Dialog.End();
        }
    }

}