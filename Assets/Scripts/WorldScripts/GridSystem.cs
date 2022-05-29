using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GridSystem : MonoBehaviour
{
    public int width;
    public int height;

    public GameObject SideWall;
    public GameObject TopWall;
    public GameObject BottomWall;

    public GameObject TopLeftCorner;
    public GameObject TopRightCorner;
    public GameObject BottomRightCorner;
    public GameObject BottomLeftCorner;

    public GameObject floorTile;

    public GameObject EntryStairs;
    public GameObject ExitStairs;
    public GameObject BarrelOne;
    public float spacing;

    public bool isReadyToSpawn = false;

    private int[,] roomGrid;

    void Start()
    {
        roomGrid = new int[width, height];

        PlaceTiles();
        PlaceStairs();
    }

    void Update()
    {
        
    }

    void SetGrid(){
        for(int x = 0; x < width; x++){
            for(int y = 0; y < height; y++){
                if(x == width-1 || x == 0){
                    roomGrid[x,y] = 1;
                }
                else if(y == 0 || y == height -1){
                    roomGrid[x,y] = 1;
                }
                else{
                roomGrid[x,y] = 0;
                }            
            }
        }
    }

    void PlaceTiles(){
        SetGrid();
        for(int x = 0; x < width; x++){
            for(int y = 0; y < height; y++){
                if(roomGrid[x,y] == 1){

                    if(x == 0 && y > 0 && y < height -1){
                        Instantiate(SideWall, new Vector3(x*spacing, 0, y*spacing), Quaternion.identity);
                    }
                    else if(x == 0 && y == 0){
                        Instantiate(BottomLeftCorner, new Vector3(x*spacing, 0, y*spacing), Quaternion.identity);

                    }  
                    else if(x == 0 && y == height-1){
                        Instantiate(TopLeftCorner, new Vector3(x*spacing, 0, y*spacing),Quaternion.identity);

                    }
                    else if(x > 0 && x < width -1 && y == height -1){
                       Instantiate(TopWall, new Vector3(x*spacing, 0, y*spacing), Quaternion.identity);
                    }
                    else if(x == width -1 && y == height -1){
                        Debug.Log("topright");
                        Instantiate(TopRightCorner, new Vector3(x*spacing, 0, y*spacing), Quaternion.identity);

                    }
                    else if(x == width -1 && y != 0){
                        Instantiate(SideWall, new Vector3(x*spacing, 0, y*spacing), Quaternion.identity);
                    }
                    else if(x == width -1 && y == 0){
                        Instantiate(BottomRightCorner, new Vector3(x*spacing, 0, y*spacing), Quaternion.identity);

                    }
                    else if(x > 0 && y==0){
                        Instantiate(BottomWall, new Vector3(x*spacing, 0, y*spacing), Quaternion.identity);
                    }  
                }
                if(roomGrid[x,y] ==0){
                    Instantiate(floorTile, new Vector3(x * spacing, 0 , y * spacing), Quaternion.identity);
                }
            }
            isReadyToSpawn = true;
        }

        
    }




    void PlaceStairs(){
        Instantiate(EntryStairs, new Vector3(5 , 0, 15), Quaternion.identity);
        Instantiate(ExitStairs, new Vector3(34f ,0, 88f), Quaternion.identity);
    }



}
