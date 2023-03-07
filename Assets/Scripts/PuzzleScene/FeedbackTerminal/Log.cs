using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LogType{Error, Status, Warning}

public class Log
{
    public LogType Type{get; private set;}
    public RichText Message{get; private set;}

    public Log(RichText message, LogType type = LogType.Status){
        Message = message;
        Type = type;
    }
}
