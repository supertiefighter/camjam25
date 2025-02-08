using System;
using System.Security.AccessControl;
using Godot;

public interface IGridObject{
    
    /// <summary>
    /// Try to push this object in the specified direction
    /// </summary>
    /// <param name="dir"></param>
    /// <returns>whether this object was pushed successfully</returns>
    bool TryPush(Vector2 movement);


}