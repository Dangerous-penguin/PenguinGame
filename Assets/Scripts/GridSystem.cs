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
    public float spacing;

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
                Debug.Log($"{roomGrid[x,y]}");
            }
        }
    }

    void PlaceTiles(){
        SetGrid();
        spacing = 5;
        for(int x = 0; x < width; x++){
            for(int y = 0; y < height; y++){
                if(roomGrid[x,y] == 1){

                    if(x == 0 && y != 0){
                        Instantiate(SideWall, new Vector3(x*spacing, 0, y*spacing), Quaternion.identity);
                    }
                    else if(x == 0 && y == 0){
                        Instantiate(BottomLeftCorner, new Vector3(x*spacing, 0, y*spacing), Quaternion.identity);
                      //  spacing = 0;
                    }  
                    else if(x == 0 && y == height -1){
                        Instantiate(TopLeftCorner, new Vector3(x*spacing, 0, y * spacing),Quaternion.identity);
                       // spacing = 0;
                    }
                    else if(x > 0 && y == height -1){
                       Instantiate(TopWall, new Vector3(x* spacing, 0, y*spacing), Quaternion.identity);
                    }
                    else if(x == width -1 && y == height -1){
                        Instantiate(TopRightCorner, new Vector3(x * spacing, 0, y*spacing), Quaternion.identity);
                       // spacing = 0;
                    }
                    else if(x == width -1 && y != 0){
                        Instantiate(SideWall, new Vector3(x * spacing, 0, y*spacing), Quaternion.identity);
                    }
                    else if(x == width -1 && y == 0){
                        Instantiate(BottomRightCorner, new Vector3(x * spacing, 0, y*spacing), Quaternion.identity);
                      //  spacing = 0;
                    }
                    else if(x > 0 && y==0){
                        Instantiate(BottomWall, new Vector3(x * spacing, 0, y*spacing), Quaternion.identity);
                    }
                    
                    
                    
                }
            }
        }
    }

    void PlaceStairs(){
        System.Random psrd = new System.Random();
        float randY = psrd.Next(1, height - 1 );
        Instantiate(EntryStairs, new Vector3(5, 0, randY), Quaternion.identity);
        Instantiate(ExitStairs, new Vector3(width -1 ,0, randY), Quaternion.identity);
    }



}
