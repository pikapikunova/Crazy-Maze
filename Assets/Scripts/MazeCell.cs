using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell
{
    public bool left = true;
    public bool right = true;
    public bool top = true;
    public bool bottom = true;
    public HashSet<int> set = new HashSet<int>();

    public MazeCell(bool Left, bool Top, bool Right, bool Bottom, HashSet<int> Set)
    {
        left = Left;
        right = Right;
        top = Top;
        bottom = Bottom;
        set = Set;
    }
}

