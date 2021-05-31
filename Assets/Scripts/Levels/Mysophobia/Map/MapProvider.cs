using UnityEngine;
using System.IO;
using System.Text;

public class MapProvider
{
    //private readonly string mapDir = "Assets/Resources/Mysophobia/Maps/";
    public Map MapInfos {get; private set;}

    public MapProvider()
    {
        MapInfos = new Map();
    }
    public void LoadFromDisk(string path)
    {
        FileInfo info = new FileInfo(path);
        byte[,] mapData;
        int rowsLength;
        int columnsLength;
        int lives;
        int accelerationCD;
        int sprayUses;
        int passThrought = 0;

        if (!info.Exists)
        {
            throw new System.Exception("Wtf guy");
        }

        try
        {
            string line;
            byte[] byteLine;

            using StreamReader reader = info.OpenText();

            line = reader.ReadLine();
            string[] split = line.Split(' ');
            rowsLength = int.Parse(split[0]);
            columnsLength = int.Parse(split[1]);
            lives = int.Parse(split[2]);
            accelerationCD = int.Parse(split[3]);
            sprayUses = int.Parse(split[4]);
            if (split.Length == 6)
            {
                passThrought = int.Parse(split[5]);
            }
            mapData = new byte[rowsLength, columnsLength];

            for (int i = 0; i < rowsLength; i++)
            {
                line = reader.ReadLine();
                byteLine = Encoding.ASCII.GetBytes(line);

                for (int j = 0; j < columnsLength; j++)
                {
                    mapData[i, j] = (byte)(byteLine[j] - '0');
                }
            }
            MapInfos.Data = mapData;
            MapInfos.Rows = rowsLength;
            MapInfos.Cols = columnsLength;
            MapInfos.Lives = lives;
            MapInfos.AccelerationCD = accelerationCD;
            MapInfos.SprayUses = sprayUses;
            MapInfos.passThroughtVirus = passThrought == 1;
        } catch (System.Exception e)
        {
            Debug.Log(e);
        }        
    }
}
