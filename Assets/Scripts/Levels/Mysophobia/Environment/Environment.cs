using System;
using System.Collections.Generic;
using UnityEngine;
using static EnvironmentPosition;
using Random = UnityEngine.Random;

public class Environment : PositionableGraphic
{
    //private Transform boardTranform; // useful ?

    private List<EnvironmentVirus> viruses;

    private EnvironmentPositionHelper positionHelper;

    private EnvironmentRotationHelper rotationHelper;
    
    public Environment(PositionProvider pp) : base(pp) {
        positionHelper = new EnvironmentPositionHelper(pp);
        rotationHelper = new EnvironmentRotationHelper();
        ReceivePrefab("Environment");
        viruses = new List<EnvironmentVirus>() { 
            new EnvironmentVirus(EnvironmentVirus.Type.INSTALLER, GetPrefab("Installer")),
            new EnvironmentVirus(EnvironmentVirus.Type.CORONA, GetPrefab("Corona")),
            new EnvironmentVirus(EnvironmentVirus.Type.FLU, GetPrefab("Flu")),
            new EnvironmentVirus(EnvironmentVirus.Type.PLAGUE, GetPrefab("Plague"))
        };
    }

    public override void Draw()
    {
        var posList = new List<Placeholder>() { Placeholder.EAST, Placeholder.NORTH, Placeholder.SOUTH, Placeholder.WEST };
        Random.InitState((int)DateTime.Now.Ticks);

        int rand;
        Placeholder elem;
        int positionIndex = 0;
        Quaternion demiAngle = Quaternion.Euler(0, 0, 90);

        while (posList.Count > 0)
        {
            rand = Random.Range(0, posList.Count - 1);
            elem = posList[rand];
            posList.RemoveAt(rand);
            viruses[positionIndex].Instanciat(new EnvironmentPosition(elem, positionHelper.getPosition(elem), rotationHelper.getRotation(elem)));
            positionIndex++;
        }
        
    }
}
