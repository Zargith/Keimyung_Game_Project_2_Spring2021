using System;
using System.Collections.Generic;
using static EnvironmentPosition;
using Random = UnityEngine.Random;

public class Environment : PositionableGraphic
{
    private List<EnvironmentVirus> _viruses;

    private EnvironmentPositionHelper _positionHelper;

    private EnvironmentRotationHelper _rotationHelper;

    public override void Init(PositionProvider pp)
    {
        _pp = pp;

        ReceivePrefab("Environment");

        _positionHelper = new EnvironmentPositionHelper(pp);
        _rotationHelper = new EnvironmentRotationHelper();

        _viruses = new List<EnvironmentVirus>() {
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

            actualVirus = _viruses[positionIndex];
            actualPos = new EnvironmentPosition(actualPlaceholder, _positionHelper.GetPosition(actualPlaceholder), _rotationHelper.GetRotation(actualPlaceholder));

            actualVirus.InitPosition(actualPos);
            //Debug.Log(actualVirus.type + " at " + actualPlaceholder + " place");
            actualVirus.Instance = Instantiate(actualVirus.Prefab, actualPos.Position, actualPos.Rotation);

            positionIndex++;
        }
    }

    public void SwapVirusPlace(EnvironmentVirus.Type type)
    {
        EnvironmentVirus virus = GetVirus(type);
        EnvironmentVirus installer = GetVirus(EnvironmentVirus.Type.INSTALLER);
        EnvironmentPosition savedPos = virus.Pos;

        virus.Move(installer.Pos);
        installer.Move(savedPos);
    }

    public Placeholder GetInstallerPlace()
    {
        return GetVirus(EnvironmentVirus.Type.INSTALLER).Pos.placeholder;
    }

    private EnvironmentVirus GetVirus(EnvironmentVirus.Type type)
    {
        foreach (EnvironmentVirus virus in _viruses)
        {
            if (virus.type == type)
                return (virus);
        }
        return (null);
    }
}
