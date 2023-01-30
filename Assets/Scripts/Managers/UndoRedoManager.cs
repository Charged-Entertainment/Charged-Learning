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
        if(logIndex != commandLog.Last){
            RemoveAllAfter(logIndex);
        }
        commandLog.AddLast(command);
        logIndex = commandLog.Last;
    }

    public void Undo(){
        if(logIndex.Previous == null)
            return;
        logIndex.Value.Undo();
        logIndex = logIndex.Previous;
        Debug.Log($"Undo!{logIndex}");
    }

    public void Redo(){
        if(logIndex.Next == null)
            return;
        logIndex.Value.Execute();
        logIndex = logIndex.Next;
        Debug.Log($"Redo!{logIndex}");

    }

    private void RemoveAllAfter(LinkedListNode<ICommand> node){
        while(node.Next != null){
            commandLog.Remove(node.Next);
        }
    }
}
