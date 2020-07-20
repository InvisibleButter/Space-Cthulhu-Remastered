using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TileConnectorScript : MonoBehaviour
{
    [HideInInspector]
    public int x, y;
    public enum TileType 
    {
        Sidewall, OuterCorner, InnerCorner
    }
    public TileType tyleType;

    //side1 side2 roof
    public NavMeshSurface[] meshs;


    public NavMeshSurface GetMesh(TileControler.Wall wall, float rotation, bool isflipped)
    {
        if (wall == TileControler.Wall.Roof)
            return meshs[meshs.Length-1];

        switch ((int)rotation)
        {
            case 0:
                return meshs[(int)wall];
            case 90:
                return wall == TileControler.Wall.Side ? meshs[1] : meshs[0];
            case 180:
                return meshs[(int)wall];
            case 270:
                return wall == TileControler.Wall.Side ? meshs[1] : meshs[0];
            default:
                return meshs[0];
        }
    }
}
