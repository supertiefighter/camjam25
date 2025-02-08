using Godot;

public interface IGridObject{
    /// <summary>
    /// Try to push this object in the specified direction
    /// </summary>
    /// <param name="dir"></param>
    /// <returns>whether this object was pushed successfully</returns>
    bool TryPush(Vector2 movement);

    /// <summary>
    /// Called when the <see cref="IGridObject"/> is hit by a laser 
    /// </summary>
    /// <param name="direction">The direction of the laser</param>
    /// <param name="colour">The colour of the laser</param>
    void LaserHit(Vector2 direction, int colour);

}