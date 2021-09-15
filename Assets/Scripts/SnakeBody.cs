using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBody : MonoBehaviour
{
    Snake snake;
    SpawnWalls walls;
    [NonSerializedAttribute] public List<Transform> body;
    [NonSerializedAttribute] public List<Vector3> bodyDirection;
    [SerializeField] GameObject snakeBodyObject;
    [SerializeField] int length = 3;
    void Start()
    {
        snake = GetComponent<Snake>();
        walls = snake.Walls;

        body = new List<Transform>();
        bodyDirection = new List<Vector3>();
        for(int i = 0; i < length - 1; i++)
            addBody(i);
    }

    public void addBody(int bodyNumber)
    {
        var position = Vector3.zero;
        if(bodyNumber > 0){
            position = body[body.Count - 1].transform.position;
            position -= bodyDirection[bodyDirection.Count - 1] * walls.SquareSize;
            bodyDirection.Add(bodyDirection[bodyDirection.Count - 1]);
        }else {
            position = snake.transform.position;
            position -= (walls.SquareSize * Vector3.right) * (bodyNumber + 1);
            bodyDirection.Add(Vector3.right);
        }
        
        var bodyGameObject = Instantiate(snakeBodyObject, position, Quaternion.identity);
        body.Add(bodyGameObject.transform);
        
    }

    public bool headHasTheSamePosition(){
        for(int i = 0; i < body.Count; i++)
            if(body[i].position == snake.transform.position)
                return true;
        return false;
    }

    public void addBody(){
        addBody(body.Count);
    }
}
