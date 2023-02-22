using System;

public abstract class Goal
{
    public bool Achieved{get; protected set;} = false;
    public string Message{get; private set;}

    public Goal(string message){
        Message = message;
    }

}
