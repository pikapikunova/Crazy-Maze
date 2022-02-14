using System;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    public GameObject CellPrefab;
    public static int n = 5;
    public static int m = 5;
    public static Vector3 CellSize = new Vector3(10, 0, 10);
    public System.Random randX = new System.Random();
    public System.Random randZ = new System.Random();

    public GameObject ex;
    public GameObject fire;

    void Start()
    {
        EllersAlgorythm alg = new EllersAlgorythm();
        MazeCell[] row1 = new MazeCell[n];
        Cell c;
        int countOfFire = randX.Next(1, (n * m) / 4);

        alg.completeFirstRow(row1);
        alg.completeSimpleRightWall(row1);
        alg.completeBottomWall(row1);

        for (int i = 0; i < n-1; i++)
        {
            c = Instantiate(CellPrefab, new Vector3(0, 0, i * CellSize.z), Quaternion.identity).GetComponent<Cell>();

            if (row1[i].right)
            {
                row1[i + 1].left = false;
            }
            setActive(i, row1, c);
        }

        c = Instantiate(CellPrefab, new Vector3(0, 0, (n-1) * CellSize.z), Quaternion.identity).GetComponent<Cell>();
        setActive(n - 1, row1, c);

        for (int i = 1; i < m - 1; i++)
        {
            alg.completeReplay(row1);
            alg.completeSimpleRightWall(row1);
            alg.completeBottomWall(row1);

            for (int j = 0; j < n-1; j++)
            {
                c = Instantiate(CellPrefab, new Vector3(i * CellSize.x, 0, j * CellSize.z), Quaternion.identity).GetComponent<Cell>();
                
                if (row1[j].right)
                    row1[j + 1].left = false;

                if (row1[j].top)
                    row1[j].top = false;

                setActive(j, row1, c);
            }

            c = Instantiate(CellPrefab, new Vector3(i * CellSize.x, 0, (n-1) * CellSize.z), Quaternion.identity).GetComponent<Cell>();

            if (row1[n - 1].top)
                row1[n - 1].top = false;

            setActive(n - 1, row1, c);
        }

        alg.completeReplay(row1);
        alg.completeEnd(row1);

        for (int i = 0; i < n - 1; i++)
        {
            c = Instantiate(CellPrefab, new Vector3((m - 1) * CellSize.x, 0, i * CellSize.z), Quaternion.identity).GetComponent<Cell>();
            if (row1[i].right)
                row1[i + 1].left = false;

            if (row1[i].top)
                row1[i].top = false;

            setActive(i, row1, c);
        }
        c = Instantiate(CellPrefab, new Vector3((m - 1) * CellSize.x, 0, (n-1) * CellSize.z), Quaternion.identity).GetComponent<Cell>();

        if (row1[n-1].top)
        {
            row1[n-1].top = false;
        }

        setActive(n - 1, row1, c);
        
        var obj = Instantiate(ex);
        obj.transform.position = new Vector3((m-1) * CellSize.x, 1, (n-1) * CellSize.z);

        for (int i = 0; i < countOfFire; i++)
        {
            int posX = randX.Next(2, m);
            int posZ = randZ.Next(2, n);

            var objFire = Instantiate(fire);
            objFire.transform.position = new Vector3(posX * CellSize.x, 1, posZ * CellSize.z); 
        }
        
    }

    public static float nextActionTime = 5f;
    public static float period = 5f;
    bool t = false;
    public GameObject Player;
    bool flag = true;

    public void Update()
    {
        if (Time.time > nextActionTime)
        {
            int countOfFire = randX.Next(1, (n * m)/4);
            nextActionTime += period;

            if (t)
            {
                for (int i = 0; i < countOfFire; i++)
                {
                    int posX = randX.Next(2, m);
                    int posZ = randZ.Next(2, n);
                    GameObject[] f = GameObject.FindGameObjectsWithTag("dangerous");
                    while (Math.Pow(posX * CellSize.x - Player.transform.position.x,2) + Math.Pow(posZ * CellSize.z - Player.transform.position.z, 2) <= 300)
                    {
                        posX = randX.Next(2, m);
                        posZ = randZ.Next(2, n);
                    }
                    
                    for (int j = 0; j < f.Length; j++)
                    {
                        if (f[j].transform.position.x / CellSize.x == posX && f[j].transform.position.z / CellSize.z == posZ)
                            flag = false;
                    }

                    if (flag)
                    {
                        var objFire = Instantiate(fire);
                        objFire.transform.position = new Vector3(posX * CellSize.x, 1, posZ * CellSize.z);
                    }
                }
                flag = true;
                t = false;

            }
            else
            {
                GameObject[] f = GameObject.FindGameObjectsWithTag("dangerous");
                for (int i = 0; i < f.Length; i++)
                    Destroy(f[i]);
                t = true;
            }
        }
       
    }

    public void setActive(int a, MazeCell[] row1, Cell c)
    {
        c.WallLeft.SetActive(row1[a].left);
        c.WallBottom.SetActive(row1[a].bottom);
        c.WallRight.SetActive(row1[a].right);
        c.WallTop.SetActive(row1[a].top);
    }
}
