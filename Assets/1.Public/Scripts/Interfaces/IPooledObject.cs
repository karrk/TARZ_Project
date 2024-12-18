using System;
using UnityEngine;

public interface IPooledObject
{
    public Enum MyType { get; }
    public GameObject MyObj { get; }
    public void Return();
}