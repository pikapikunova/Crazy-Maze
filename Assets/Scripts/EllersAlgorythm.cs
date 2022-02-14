using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllersAlgorythm
{
    System.Random random = new System.Random();
    List<HashSet<int>> set = new List<HashSet<int>>();
    int countSet = 0;

    public void completeFirstRow(MazeCell[] Row)
    {
        set.Add(new HashSet<int>());
        set[0].Add(0);

        Row[0] = new MazeCell(true, true, false, false, set[0]);
        countSet++;

        for (int i = 1; i < Row.Length - 1; i++)
        {
            set.Add(new HashSet<int>());
            set[i].Add(countSet);

            Row[i] = new MazeCell(false, true, false, false, set[i]);
            countSet++;
        }

        set.Add(new HashSet<int>());
        set[Row.Length - 1].Add(countSet);

        Row[Row.Length - 1] = new MazeCell(false, true, true, false, set[Row.Length - 1]);
    }

    public void completeSimpleRightWall(MazeCell[] Row)
    {
        for (int i = 0; i < Row.Length - 1; i++)
        {
            bool t = false;

            for (int j = i+1; j < Row.Length; j++)
                foreach (int value in Row[j].set)
                    if (Row[i].set.Contains(value))
                        t = true;

            if (t)
            {
                Row[i].right = true;
                Row[i + 1].left = true;
            }
            else
            {
                Row[i].right = random.Next(2) == 0 ? false : true;
                Row[i + 1].left = Row[i].right;

            }

        }

        for (int i = 0; i < Row.Length - 1; i++)
        {
            int j = i+1;
            while (j < Row.Length && !Row[j].left)
            {
                Row[i].set.UnionWith(Row[j].set);
                Row[j].set.UnionWith(Row[i].set);
                j++;
            }
        }
    }

    public void completeBottomWall(MazeCell[] Row)
    {
        List<structSet> str = new List<structSet>();
        str = aboutSet(Row);

        for (int i = 0; i < Row.Length - 1; i++)
        {
            for (int j = 0; j < str.Count; j++)
            {

                if (str[j].numOfSet.IsSubsetOf(Row[i].set) && str[j].count > 1)
                {
                    Row[i].bottom = random.Next(2) == 0 ? false : true;
                    if (Row[i].bottom == true)
                        str[j].count--;

                }
            }

        }

    }

    public void completeReplay(MazeCell[] Row)
    {

        List<structSet> str = new List<structSet>();
        str = aboutSet(Row);

        for (int i = 0; i < Row.Length - 1; i++)
            Row[i].right = false;
        for (int i = 1; i < Row.Length; i++)
            Row[i].left = false;

        for (int i = 0; i < Row.Length; i++)
        {
            if (Row[i].bottom)
                Row[i].top = true;
            else
                Row[i].top = false;

        }


        for (int i = 0; i < Row.Length; i++)
        {
            if (Row[i].bottom)
            {
                countSet++;
                set.Add(new HashSet<int>());
                set[set.Count - 1].Add(countSet);
                str.Add(new structSet(set[set.Count - 1], 1));

                Row[i].set.Clear();
                Row[i].set.Add(countSet);
                Row[i].bottom = false;
            }
        }
    }

    public void completeEnd(MazeCell[] Row)
    {
        for (int i = 0; i < Row.Length; i++)
            Row[i].top = Row[i].bottom;

        for (int i = 0; i < Row.Length - 1; i++)
        {
            Row[i].bottom = true;
            bool t = false;

            for (int j = i + 1; j < Row.Length; j++)
            {
                foreach (int value in Row[j].set)
                    if (Row[i].set.Contains(value))
                        t = true;
            }
            if (!t)
            {
                Row[i].right = false;
                Row[i + 1].left = false;
                Row[i].set.UnionWith(Row[i + 1].set);
                Row[i + 1].set.UnionWith(Row[i].set);
            }
            else
            {
                Row[i].right = true;
                Row[i + 1].left = true;
            }
        }

        Row[Row.Length - 1].bottom = true;

        for (int i = 0; i < Row.Length - 1; i++)
        {
            int j = i;
            while (!Row[j].right)
            {
                Row[i].set.UnionWith(Row[j].set);
                Row[j].set.UnionWith(Row[i].set);
                j++;

            }
        }
    }

    public List<structSet> aboutSet(MazeCell[] row)
    {
        List<structSet> str = new List<structSet>();
        int tempCount = 0;
        HashSet<int> tempSet = new HashSet<int>();

        for (int i = 0; i < row.Length; i++)
        {
            tempSet = row[i].set;
            if (isInList(str, tempSet) == false)
            {
                for (int j = 0; j < row.Length; j++)
                {
                    if (tempSet.SetEquals(row[j].set) && row[j].bottom == false)
                        tempCount++;
                }

                str.Add(new structSet(tempSet, tempCount));
            }
            tempCount = 0;
        }

        return str;
    }

    public bool isInList(List<structSet> str, HashSet<int> x)
    {
        bool t = false;
        for (int i = 0; i < str.Count && !t; i++)
            if (str[i].numOfSet.SetEquals(x))
                t = true;
        return t;
    }



}