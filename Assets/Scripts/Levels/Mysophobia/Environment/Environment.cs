using System;
using System.Collections.Generic;
using UnityEngine;
using static EnvironmentPosition;
using Random = UnityEngine.Random;

public class Environment : PositionableGraphic
{
    private List<EnvironmentVirus> viruses;

    private EnvironmentPositionHelper positionHelper;

    private EnvironmentRotationHelper rotationHelper;

    public override void Init(PositionProvider pp)
    {
        _pp = pp;

        ReceivePrefab("Environment");

        positionHelper = new EnvironmentPositionHelper(pp);
        rotationHelper = new EnvironmentRotationHelper();

        viruses = new List<EnvironmentVirus>() {
            new EnvironmentVirus(EnvironmentVirus.Type.INSTALLER, GetPrefab("Installer")),
            new EnvironmentVirus(EnvironmentVirus.Type.CORONA, GetPrefab("Corona")),
            new EnvironmentVirus(EnvironmentVirus.Type.FLU, GetPrefab("Flu")),
            new EnvironmentVirus(EnvironmentVirus.Type.PLAGUE, GetPrefab("Plague"))
        };
    }

    public override void Draw()
    {
        Random.InitState((int)DateTime.Now.Ticks);

        var placeholderList = new List<Placeholder>() { Placeholder.EAST, Placeholder.NORTH, Placeholder.SOUTH, Placeholder.WEST };
        Placeholder actualPlaceholder;

        int rand;
        int positionIndex = 0;
        EnvironmentPosition actualPos;
        EnvironmentVirus actualVirus;

        while (placeholderList.Count > 0)
        {
            rand = Random.Range(0, placeholderList.Count - 1);
            actualPlaceholder = placeholderList[rand];
            placeholderList.RemoveAt(rand);

            actualVirus = viruses[positionIndex];
            actualPos = new EnvironmentPosition(actualPlaceholder, positionHelper.getPosition(actualPlaceholder), rotationHelper.getRotation(actualPlaceholder));

            actualVirus.InitPosition(actualPos);
            Debug.Log(actualVirus.type + " at " + actualPlaceholder + " place");
            actualVirus._instance = Instantiate(actualVirus._prefab, actualPos.position, actualPos.rotation);

            positionIndex++;
        }
    }

    public void SwapVirusPlace(EnvironmentVirus.Type type)
    {
        EnvironmentVirus virus = GetVirus(type);
        EnvironmentVirus installer = GetVirus(EnvironmentVirus.Type.INSTALLER);
        EnvironmentPosition savedPos = virus._pos;

        virus.Move(installer._pos);
        installer.Move(savedPos);
    }

    public Placeholder GetInstallerPlace()
    {
        return GetVirus(EnvironmentVirus.Type.INSTALLER)._pos.placeholder;
    }

    private EnvironmentVirus GetVirus(EnvironmentVirus.Type type)
    {
        foreach (EnvironmentVirus virus in viruses)
        {
            if (virus.type == type)
                return (virus);
        }
        return (null);
    }
}
