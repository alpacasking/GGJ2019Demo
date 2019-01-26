using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 乘客的类
/// </summary>
public class Passenge:MonoBehaviour
{
    public enum PassengeState
    {
        Standing,
        Sitting,
    }
    //方块列表
    public List<Block> Blocks;
    public PassengeState State;
    public int Angle = 0;
    public void Rotate()
    {
        for (int i = 0; i < Blocks.Count; i++)
        {
            int x = Blocks[i].x;
            int y = Blocks[i].y;
            switch (Angle)
            {
                case 0:
                    Blocks[i].x = y;
                    Blocks[i].y = x;
                    break;
                case 90:
                    Blocks[i].x = y;
                    Blocks[i].y = x;
                    break;
                case 180:
                    Blocks[i].x = y;
                    Blocks[i].y = x;
                    break;
                case 270:
                    Blocks[i].x = y;
                    Blocks[i].y = x;
                    break;
            }
        }
    }
}
