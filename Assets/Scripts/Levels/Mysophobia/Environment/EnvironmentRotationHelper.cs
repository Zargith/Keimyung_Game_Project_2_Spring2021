using System.Collections.Generic;
using UnityEngine;
using static EnvironmentPosition;

public class EnvironmentRotationHelper
{
    private Dictionary<Placeholder, Quaternion> _rotations;

    public EnvironmentRotationHelper()
    {

        _rotations = new Dictionary<Placeholder, Quaternion> {
            { Placeholder.EAST,  Quaternion.identity},
            { Placeholder.NORTH, Quaternion.Euler(0, 0, 90) },
            { Placeholder.WEST, Quaternion.identity},
            { Placeholder.SOUTH, Quaternion.Euler(0, 0, 90) }
        };
    }
    public Quaternion GetRotation(Placeholder placeholder)
    {
        return (_rotations[placeholder]);
    }

}
