using System.Collections.Generic;
using UnityEngine;
using static EnvironmentPosition;

public class EnvironmentRotationHelper
{
    private Dictionary<Placeholder, Quaternion> rotations;

    public EnvironmentRotationHelper()
    {

        rotations = new Dictionary<Placeholder, Quaternion> {
            { Placeholder.EAST,  Quaternion.identity},
            { Placeholder.NORTH, Quaternion.Euler(0, 0, 90) },
            { Placeholder.WEST, Quaternion.identity},
            { Placeholder.SOUTH, Quaternion.Euler(0, 0, 90) }
        };
    }
    public Quaternion getRotation(Placeholder placeholder)
    {
        return (rotations[placeholder]);
    }

}
