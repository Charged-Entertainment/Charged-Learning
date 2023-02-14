using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogs;

// This class is used to test Dialogs for now. Later it may be deleted, or it'll actually be used to manage and load dialogs.
public class DialogLoader : Singleton<DialogLoader>
{
    private void Start() {
        List<DialogEntry> seq1Entries = new List<DialogEntry>();
        seq1Entries.Add(new DialogEntry(RichText.Bold("init")));
        seq1Entries.Add(new DialogEntry(RichText.Bold("Let me tell you a story...")));
        seq1Entries.Add(new DialogEntry(RichText.Bold("story part 1")));
        seq1Entries.Add(new DialogEntry(RichText.Bold("story part 2")));
        seq1Entries.Add(new DialogEntry(RichText.Bold("story part 3")));
        seq1Entries.Add(new DialogEntry(RichText.Bold("story part 4")));
        seq1Entries.Add(new DialogEntry(RichText.Bold("story part 5")));
        seq1Entries.Add(new DialogEntry(RichText.Bold("story part 6")));
        seq1Entries.Add(new DialogEntry(RichText.Bold("Thanks for listening to me.")));

        var seq = new DialogSequence(seq1Entries);
        Dialog.PlaySequence(seq);
    }
}
