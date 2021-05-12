using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class MapProvider : ScriptableObject
{
    private readonly string mapDir = "Assets/Resources/Mysophobia/Maps/";

    public int RowsLength { private set; get; }
    public int ColumnsLength { private set; get; }

    public byte[,] Map { get; private set; }

    /*
    public void generate(MapManager.GeneratedMapOptions options)
    {
        RowsLength = options.rows;
        ColumnsLength = options.columns;
        //TODO
    }*/
    public void LoadFromDisk(string name)
    {
        FileInfo info = new FileInfo(mapDir + name + ".txt");

        if (!info.Exists)
        {
            Debug.Log("Map " + name + " doesn't exist in map directory");
            return;
        }

        try
        {
            string line;
            byte[] byteLine;

            using (StreamReader reader = info.OpenText())
            {
                line = reader.ReadLine();
                string[] split = line.Split(' ');
                RowsLength = int.Parse(split[0]);
                ColumnsLength = int.Parse(split[1]);
                Map = new byte[RowsLength, ColumnsLength];

                for (int i = 0; i < RowsLength; i++)
                {
                    line = reader.ReadLine();
                    byteLine = Encoding.ASCII.GetBytes(line);

                    for (int j = 0; j < ColumnsLength; j++)
                    {
                        Map[i, j] = (byte)(byteLine[j] - '0');
                    }
                }
            }
            /*string e = "";
            for (int i = 0; i < RowsLength; i++) {
                for (int j = 0; j < ColumnsLength; j++)
                    e += (char)(Map[i, j] + '0');
                Debug.Log(e);
                e = "";
            }*/
        } catch (System.Exception e)
        {
            Debug.Log(e);
        }        
    }
}
