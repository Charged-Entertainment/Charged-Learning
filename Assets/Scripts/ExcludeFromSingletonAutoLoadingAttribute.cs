using System;

[AttributeUsage(AttributeTargets.Class)]
public class ExcludeFromSingletonAutoLoadingAttribute : Attribute
{
    public ExcludeFromSingletonAutoLoadingAttribute(){}
};