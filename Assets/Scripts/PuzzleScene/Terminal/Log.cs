using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LogType{Error, Status}

public class Log
{
    public LogType Type{get; private set;}
    public RichText Message{get; private set;}

    public Log(RichText message, LogType type){
        Message = message;
        Type = type;
    }
}
