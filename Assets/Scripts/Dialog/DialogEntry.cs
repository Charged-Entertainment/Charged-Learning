using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Dialogs
{
    public class DialogEntry
    {
        public Action started;
        public Action ended;

        public RichText content { get; private set; }
        public Sprite image { get; private set; }

        public DialogEntry next { get; set; }

        public DialogEntry(RichText content, Sprite image, DialogEntry next)
        {
            this.content = content;
            this.image = image;
            this.next = next;
        }

        public DialogEntry(RichText content)
        {
            this.content = content;
            this.image = Resources.Load<Sprite>("Sprites/Robot/default");
        }

        public static implicit operator string(DialogEntry d)
        {
            return d.content;
        }

        public static implicit operator Sprite(DialogEntry d)
        {
            return d.image;
        }
    }
}