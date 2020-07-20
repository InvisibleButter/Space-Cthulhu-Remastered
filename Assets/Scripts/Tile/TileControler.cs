using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TileControler : MonoBehaviour
{
    List<TileConnectorScript> tiles;
    public float tileWidth;
    public float offSet;
    public enum Wall
    {
        //a tile has max two walls
        Roof=0,
        //north or south
        Side=1,
        //west 
        SideTwo=2
    }

    void Start()
    {
        foreach (TileConnectorScript t in GetComponentsInChildren<TileConnectorScript>())
        {
            t.x = CalculateCoordinates(t.transform.position.x);
            t.y = CalculateCoordinates(t.transform.position.y);
            tiles.Add(t);
        }
    }

    private int CalculateCoordinates(float oldX)
    {
        return (int)(oldX / tileWidth - offSet);
    }

    /*public NavMeshSurface GetNavMeshSurfaceOfTile(int x, int y,float raotation, Wall w,bool isFliped)
    {
        foreach (TileConnectorScript t in tiles)
        {
            if(t.x==x&&t.y==y)
                return
        }
    }
    */
}
