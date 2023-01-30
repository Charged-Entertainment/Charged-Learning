using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoRedoManager : Manager
{
    private LinkedList<ICommand> commandLog;
    private LinkedListNode<ICommand> logIndex;

    private void Start() {
        commandLog = new LinkedList<ICommand>();
    }

    public void RegisterCommand(ICommand command){
        commandLog.AddLast(command);
        if(commandLog.Count == 0){
            logIndex = commandLog.Last;
        }
    }

    public void Undo(){
        if(logIndex.Previous == null)
            return;
        logIndex.Value.Undo();
        logIndex = logIndex.Previous;
    }

    public void Redo(){
        if(logIndex.Next == null)
            return;
        logIndex.Value.Execute();
        logIndex = logIndex.Next;

    }
}
