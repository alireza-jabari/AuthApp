using System;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class PersianNameAttribute : Attribute
{
    public string Name { get; }

    public PersianNameAttribute(string name)
    {
        Name = name;
    }
}