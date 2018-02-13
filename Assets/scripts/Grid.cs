using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    // The 3 Grid 
    public static int g1w = 30;
    public static int g1h = 20;
    public static bool removeLinesGrid1 = false;
    public static bool removeLinesGrid2 = false;
    public static bool removeLinesGrid3 = false;

    // 3 grids
    public static Transform[,] grid1 = new Transform[g1w, g1h];


    public static Vector2 roundVec2(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }


    public static bool insideBorder(Vector2 pos)
    {
        //Debug.Log("Vector Position: " + pos.x + " " + pos.y);

        return ((int)pos.x >= 0 &&
                (int)pos.x < g1w &&
                (int)pos.y >= 0 );
    }

    public static void deleteRow(int y)
    {
        Debug.Log("public static void deleteRow(int y)\n");
        if (removeLinesGrid1 == true)
        {
            Debug.Log("removeLinesGrid1 == " + removeLinesGrid1 + "\n");
            for (int x = 0; x < 10; ++x)
            {
                Destroy(grid1[x, y].gameObject);
                grid1[x, y] = null;
            }
        }

        if (removeLinesGrid2 == true)
        {
            Debug.Log("removeLinesGrid2 == " + removeLinesGrid2 + "\n");
            for (int x = 10; x < 20; ++x)
            {
                Destroy(grid1[x, y].gameObject);
                grid1[x, y] = null;
            }
        }

        if (removeLinesGrid3 == true)
        {
            Debug.Log("removeLinesGrid3 == " + removeLinesGrid3 + "\n");
            for (int x = 20; x < 30; ++x)
            {
                Destroy(grid1[x, y].gameObject);
                grid1[x, y] = null;
            }
        }

    }

    public static void decreaseRow(int y)
    {
        Debug.Log("public static void decreaseRow(int y)\n");
        if (removeLinesGrid1 == true)
        {
            for (int x = 0; x <10; ++x)
            {
                //Debug.Log(" for (int x = 0; x <10; ++x)\n");
                if (grid1[x, y] != null && y > 0)
                {
                    //Debug.Log("     if (grid1[x, y] != null)\n");
                    // Move one towards bottom
                    grid1[x, y - 1] = grid1[x, y];
                    grid1[x, y] = null;

                    // Update Block position
                    grid1[x, y - 1].position += new Vector3(0, -1, 0);
                }
            }
        }

        if (removeLinesGrid2 == true)
        {
            Debug.Log("public static void decreaseRow(int y)\n");
            for (int x = 10; x < 20; ++x)
            {
                //Debug.Log(" for (int x =10; x < 20; ++x)\n");
                if (grid1[x, y] != null)
                {
                    //Debug.Log("     if (grid1[x, y] != null)\n");
                    // Move one towards bottom
                    grid1[x, y - 1] = grid1[x, y];
                    grid1[x, y] = null;

                    // Update Block position
                    grid1[x, y - 1].position += new Vector3(0, -1, 0);
                }
            }
        }

        if (removeLinesGrid3 == true)
        {
            Debug.Log("public static void decreaseRow(int y)\n");
            for (int x = 20; x < 30; ++x)
            {
                //Debug.Log(" for (int x = 20; x < 30; ++x)\n");
                if (grid1[x, y] != null)
                {
                    // Move one towards bottom
                    grid1[x, y - 1] = grid1[x, y];
                    grid1[x, y] = null;

                    // Update Block position
                    grid1[x, y - 1].position += new Vector3(0, -1, 0);
                }
            }
        }

    }

    public static void decreaseRowsAbove(int y)
    {
        //Debug.Log("public static void decreaseRowsAbove(int y)\n");
        for (int i = y; i < g1h; ++i)
            decreaseRow(i);
    }

    public static bool isRowFullGrid1(int y)
    {
        removeLinesGrid1 = false;
        //Debug.Log("public static bool isRowFullGrid1(int y)\n");
        for (int x = 0; x < 10; ++x)
        {
            //Debug.Log("grid1 : " + x + " , " + y + "\n");
            if (grid1[x, y] == null)
            {
                //removeLinesGrid1 = true;

                return false;
            }
        }
        //get number rows cleared so far
        FindObjectOfType<updateScore>().numberOfRowsThisTurn++;
        //Debug.Log("     removeLinesGrid1 = true;\n");
        removeLinesGrid1 = true;
        return true;
    }

    public static bool isRowFullGrid2(int y)
    {
        removeLinesGrid2 = false;
        //Debug.Log("public static bool isRowFullGrid2(int y)\n");
        for (int x = 10; x < 20; ++x)
        {
            //Debug.Log("grid1 : " + x + " , " + y + "\n");
            if (grid1[x, y] == null)
            {
                return false;
            }
        }

        //get number rows cleared so far
        FindObjectOfType<updateScore>().numberOfRowsThisTurn++;
        removeLinesGrid2 = true;
        Debug.Log("     removeLinesGrid2 = true;\n");
        return true;
    }

    public static bool isRowFullGrid3(int y)
    {
        removeLinesGrid3 = false;
        //Debug.Log("public static bool isRowFullGrid3(int y)\n");
        for (int x = 20; x < 30; ++x)
        {
            //Debug.Log("public static bool isRowFullGrid2(int y)\n");
            if (grid1[x, y] == null)
            {

                return false;
            }
        }
        //get number rows cleared so far
        FindObjectOfType<updateScore>().numberOfRowsThisTurn++;
        removeLinesGrid3 = true;
        Debug.Log("     removeLinesGrid3 = true;\n");
        return true;
    }

    public static void deleteFullRows()
    {
        //Debug.Log("*****public static void deleteFullRows()\n");
        for (int y = 0; y < g1h; ++y)
        {
            if (isRowFullGrid1(y))
            {
                //Debug.Log("     if (isRowFullGrid1(y))\n");
                deleteRow(y);
                decreaseRowsAbove(y + 1);
                --y;
            }
            if (isRowFullGrid2(y))
            {
                //Debug.Log("     if (isRowFullGrid2(y))\n");
                deleteRow(y);
                decreaseRowsAbove(y + 1);
                --y;
            }
            if (isRowFullGrid3(y))
            {
                //Debug.Log("     if (isRowFullGrid3(y))\n");
                deleteRow(y);
                decreaseRowsAbove(y + 1);
                --y;
            }

        }
    }
}
