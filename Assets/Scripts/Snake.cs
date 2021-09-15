using System;
using UnityEngine;

public class Snake : MonoBehaviour
{    
    
    public SpawnWalls Walls;
    [NonSerializedAttribute] public bool isAlive = true;
    [NonSerializedAttribute] public bool win = false;
    float timer = 0;
    public float delayMove = 1.0f;
    [NonSerializedAttribute] public Vector3 direction;
    public Vector3 lastDirection;
    SnakeBody snakeBody;

    SpriteRenderer sprite;
    void Start()
    {
        direction = Vector3.right;
        lastDirection = direction;
        snakeBody = GetComponent<SnakeBody>();
        sprite = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (!isAlive || win){
            if(!win)loseAnimation();
            return;
        }

        move();
        Walls.CheckWall();
        input();
    }

    void move()
    {
        timer += Time.deltaTime;
        if (timer > delayMove)
        {
            moveBody();
            lastDirection = direction;
            transform.position += direction * Walls.SquareSize;

            timer = 0.0f;
        }
    }

    void moveBody(){
        for(int i = snakeBody.body.Count - 1; i >= 0 ; i--){
            snakeBody.bodyDirection[i] = i > 0 ? snakeBody.bodyDirection[i-1] : lastDirection; 
            snakeBody.body[i].position += snakeBody.bodyDirection[i] * Walls.SquareSize;
        }
    }

    void FixedUpdate()
    {
        if(CheckLimits() || snakeBody.headHasTheSamePosition()){
            isAlive = false;
        }

        if(!Walls.hasApples())
            win = true;
    }

    void input(){
        if(Input.GetKey(KeyCode.UpArrow))
            direction = Vector3.up;
        if(Input.GetKey(KeyCode.DownArrow))
            direction = Vector3.down;
        if(Input.GetKey(KeyCode.RightArrow))
            direction = Vector3.right;
        if(Input.GetKey(KeyCode.LeftArrow))
            direction = Vector3.left;
    }

    void loseAnimation(){
        var color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.7f);
        if(Time.time % 1.0f > 0.5f)
            color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1.0f);
        
        sprite.color = color;
        foreach(Transform body in snakeBody.body)
            body.GetComponent<SpriteRenderer>().color = color;
    }

    bool CheckLimits(){
        if(
        (transform.position.x >=  Walls.gridSize.x * Walls.SquareSize || 
        transform.position.x <  0) ||
        (transform.position.y < -Walls.gridSize.y * Walls.SquareSize || 
        transform.position.y > 0)
        ){
            return true;
        }
        return false;
    }
}
