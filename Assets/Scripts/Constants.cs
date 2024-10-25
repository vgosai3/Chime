using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An enum that defines all enemy/attack types.
/// </summary>
#warning Change placeholder types
public enum Type
{
    None,
    Water,
    Fire,
    Earth,
    Air
}

/// <summary>
/// An enum that defines all items that the player
/// can interact with.
/// </summary>
public enum Item
{
    None,
    Dagger,
    Sword,
    Scythe,
    Turret,
    ItemTest,
    TurretItemTest
}

/// <summary>
/// An enum that defines all npcs that the player
/// can interact with.
/// </summary>
public enum NonPlayerCharacters
{
    Farmer,
    Blacksmith,
    King
}

public class Constants
{
    /// <summary>
    /// This function is used to retrieve the angle of the camera. This can be used to
    /// position text properly such that it appears parallel to the camera, rather than
    /// slanted
    /// </summary>
    /// <returns>A quaternion representing the angle of the camera</returns>
    public static Quaternion GetCameraAngles()
    {
        SmoothCameraFollow follow = Camera.main.GetComponent<SmoothCameraFollow>();
        return Quaternion.Euler((float) follow.VerticalAngle, (float) follow.HorizontalAngle, 0.0f);
    }
}