using UnityEngine;
using System.IO;
using System.Text;
using System.Collections.Generic;
public class MapProvider
{
    private Dictionary<string, string> p;

    //private readonly string mapDir = "Assets/Resources/Mysophobia/Maps/";
    public Map MapInfos {get; private set;}

    public MapProvider()
    {
        MapInfos = new Map();
        p = new Dictionary<string, string>();
        p.Add("Maps/Campaign/campaign_1.txt", "12 12 40 8 2\n211110111111\n100011101001\n111100101111\n000111100001\n000100111111\n011101101010\n010001001010\n011111101111\n001000111101\n011110100101\n010011111101\n011110000114");
        p.Add("Maps/Campaign/campaign_2.txt", "15 15 50 10 1\n211000111011100\n101101101010100\n100101001110100\n111111101010100\n000100111011110\n111100101110111\n100111111011101\n111100101000101\n010111101111111\n010010100010001\n010110111111111\n010100010010010\n111100111011111\n100111101001001\n111100111111114");
        p.Add("Maps/Campaign/campaign_3.txt", "19 19 50 10 0 1\n2111111011111000000\n1010100010101000000\n1011111110111110000\n\n1010001010001010000\n1111111111111011100\n1000101000101010100\n1110101111101111111\n0010101000101010001\n1111111011111010111\n1000101010001010101\n1111101111101111111\n1010101010101000101\n1110111010111111101\n0010001010001010101\n0011111111111111111\n0000101010101010001\n0000111011111011111\n0000001010001010101\n0000001111111111114");
        p.Add("Maps/Freeplay/campaign_1.txt", "12 12 30 8 1\n211110111111\n100011101001\n111100101111\n000111100001\n000100111111\n011101101010\n010001001010\n011111101111\n001000111101\n011110100101\n010011111101\n011110000114");
        p.Add("Maps/Freeplay/campaign_2.txt", "15 15 40 10 1\n211000111011100\n101101101010100\n100101001110100\n111111101010100\n000100111011110\n111100101110111\n100111111011101\n111100101000101\n010111101111111\n010010100010001\n010110111111111\n010100010010010\n111100111011111\n100111101001001\n111100111111114");
        p.Add("Maps/Freeplay/campaign_3.txt", "19 19 50 10 2 1\n2111111011111000000\n1010100010101000000\n1011111110111110000\n\n1010001010001010000\n1111111111111011100\n1000101000101010100\n1110101111101111111\n0010101000101010001\n1111111011111010111\n1000101010001010101\n1111101111101111111\n1010101010101000101\n1110111010111111101\n0010001010001010101\n0011111111111111111\n0000101010101010001\n0000111011111011111\n0000001010001010101\n0000001111111111114");

    }
    public void LoadFromDisk(string path)
    {
        //FileInfo info = new FileInfo(path);
        byte[,] mapData;
        int rowsLength;
        int columnsLength;
        int lives;
        int accelerationCD;
        int sprayUses;
        int passThrought = 0;
        Debug.Log(path);
        //TextAsset textAsset = Resources.Load<TextAsset>(path);
        //if (textAsset == null)
         //   Debug.Log("null");
        string content = p[path];

        string[] lins = content.Split('\n');

/*
        if (!info.Exists)
        {
            throw new System.Exception("Wtf guy");
        }
*/
        try
        {
            string line;
            byte[] byteLine;

            //using StreamReader reader = info.OpenText();
            for (int k = 0; k < lins.Length; k++) {
                line = lins[k];
                //line = reader.ReadLine();
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
                    k++;
                    line = lins[k];
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
            }
            
        } catch (System.Exception e)
        {
            Debug.Log(e);
        }        
    }
}
