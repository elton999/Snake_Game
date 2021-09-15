using System;
using UnityEngine;

public class SpawnWalls : MonoBehaviour
{
    [SerializeField] GameObject WallObject;
    [SerializeField] GameObject AppleObject;
    [SerializeField] GameObject Background;
    [SerializeField] Snake Snake;
    SnakeBody snakeBody;
    public Vector2 gridSize;
    public float SquareSize = 1.03f;
    [NonSerializedAttribute] public int[] tiles;

    void Start()
    {
        tiles = new int[128]{
            1,1,3,1,1,1,1,1,1,1,1,1,1,1,1,1,
            1,1,1,1,1,2,1,1,1,1,1,1,1,3,1,1,
            1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
            1,1,1,1,3,2,1,1,1,1,1,3,1,1,1,1,
            1,1,1,1,2,2,2,1,1,1,1,1,1,2,1,1,
            3,1,1,1,1,2,1,1,1,1,1,3,1,1,1,1,
            1,1,1,1,1,1,1,1,1,2,2,1,1,2,1,1,
            1,1,3,1,1,2,1,1,1,1,1,1,1,2,1,3,
        };

        for(int i = 0; i < 128; i ++){
            int x = i % 16 + 1;
            int y = (int)(i / 16);
            
            if(tiles[i] == 2)
                setObject(WallObject,x, y);
            if (tiles[i] == 3){
                setObject(Background,x, y);
                var apple = setObject(AppleObject, x, y).GetComponent<Apple>();
                apple.index = i;
                apple.walls = Snake.Walls;
            }
            if(tiles[i] == 1)
                setObject(Background,x, y);
        }

        snakeBody = Snake.GetComponent<SnakeBody>();
    }

    GameObject setObject(GameObject gameObject, int x, int y)
    {
        return Instantiate(gameObject, new Vector3(x * SquareSize, -y * SquareSize, 0), Quaternion.identity);
    }

    public void CheckWall(){
        int x = (int)Math.Round(Snake.transform.position.x / SquareSize) - 1;
        int y = (int)Math.Round(Snake.transform.position.y / SquareSize * -1); 
        int index = (int)(16 *y + x);

        if(index > 127 || index < 0)
            return;
        
        if(tiles[index] == 2){
            Snake.isAlive = false;
        }
        if(tiles[index] == 3){
            snakeBody.addBody();
            tiles[index] = 1;
        }
    }

    public bool hasApples(){
        for(int i = 0; i < tiles.Length; i++)
            if(tiles[i] == 3)
                return true;
        return false;
    }
}
