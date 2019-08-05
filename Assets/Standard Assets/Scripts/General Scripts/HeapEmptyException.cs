using UnityEngine;
using System.Collections;

public class HeapEmptyException : System.ApplicationException
{

    public HeapEmptyException() {}
    public HeapEmptyException(string msg) { }
    public HeapEmptyException(string msg, System.Exception inner) {}
    protected HeapEmptyException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) {}
}
