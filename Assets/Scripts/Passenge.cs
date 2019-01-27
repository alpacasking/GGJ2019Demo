using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 乘客的类
/// </summary>
public class Passenge : MonoBehaviour
{
    public enum PassengeState
    {
        Standing,
        Sitting,
    }
    //方块列表
    public List<Block> BlocksCache;
    public List<Block> Blocks;
    public PassengeState State;
    public void Rotate(bool inverse = true)
    {
        for (int i = 0; i < Blocks.Count; i++)
        {
            int x = Blocks[i].x;
            int y = Blocks[i].y;
            if (inverse)
            {
                Blocks[i].x = -y;
                Blocks[i].y = x;
            }
            else
            {
                Blocks[i].x = y;
                Blocks[i].y = -x;
            }
        }
    }
    public void Cache()
    {
        Debug.Log("Cache");
        BlocksCache = new List<Block>();
        foreach(var b in Blocks)
        {
            BlocksCache.Add(new Block { x = b.x, y = b.y});
        }
    }
}
