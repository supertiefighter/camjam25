using Godot;

public interface IGridObject{
    public enum Direction{
        UP,
        LEFT,
        DOWN,
        RIGHT
    }
    /// <summary>
    /// Try to push this object in the specified direction
    /// </summary>
    /// <param name="dir"></param>
    /// <returns>whether this object was pushed successfully</returns>
    bool TryPush(Direction dir);


}