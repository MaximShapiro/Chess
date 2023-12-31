using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ChesspieceScript : MonoBehaviour
{
    public GameObject controller, movePlate;
    private int xChessboard, yChessboard = -1;
    private string player;
    public Sprite black_queen, black_knight, black_bishop, black_king, black_rook, black_pawn, white_queen, white_knight, white_bishop, white_king, white_rook, white_pawn;
                    
    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        SetCoordinates(); 
        
        switch(this.name)
        {
            case "black_queen": this.GetComponent<SpriteRenderer>().sprite = black_queen; player = "black"; break;
            case "black_knight": this.GetComponent<SpriteRenderer>().sprite = black_knight; player = "black"; break;
            case "black_bishop": this.GetComponent<SpriteRenderer>().sprite = black_bishop; player = "black"; break;
            case "black_king": this.GetComponent<SpriteRenderer>().sprite = black_king; player = "black"; break;
            case "black_rook": this.GetComponent<SpriteRenderer>().sprite = black_rook; player = "black"; break;
            case "black_pawn": this.GetComponent<SpriteRenderer>().sprite = black_pawn; player = "black"; break;
            case "white_queen": this.GetComponent<SpriteRenderer>().sprite = white_queen; player = "white"; break;
            case "white_knight": this.GetComponent<SpriteRenderer>().sprite = white_knight; player = "white"; break;
            case "white_bishop": this.GetComponent<SpriteRenderer>().sprite = white_bishop; player = "white"; break;
            case "white_king": this.GetComponent<SpriteRenderer>().sprite = white_king; player = "white"; break;
            case "white_rook": this.GetComponent<SpriteRenderer>().sprite = white_rook; player = "white"; break;
            case "white_pawn": this.GetComponent<SpriteRenderer>().sprite = white_pawn; player = "white"; break;
        }
    }
    public void SetCoordinates()
    {
        float x = xChessboard, y = yChessboard;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;
        
        this.transform.position = new Vector3(x,y,-1.0f);
    }
    public int GetxChessboard()
    {
        return xChessboard;
    }

    public int GetyChessboard()
    {
        return yChessboard;
    }

    public void SetxChessboard(int x)
    {
        xChessboard = x;
    }

    public void SetyChessboard(int y)
    {
        yChessboard = y;
    }

    private void OnMouseUp()
    {
        if(!controller.GetComponent<GameScript>().IsGameOver() && controller.GetComponent<GameScript>().GetCurrentPlayer() == player)
        {
        RemoveMovePlates();
        InitiateMovePlates();
        }
    }
    public void RemoveMovePlates()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for(int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]);
        }
    }
    public void InitiateMovePlates()
    {
        switch(this.name)
        {
            case "black_queen":
            case "white_queen":
                LineMovePlate(1,0);
                LineMovePlate(0,1);
                LineMovePlate(1,1);
                LineMovePlate(-1,0);
                LineMovePlate(0,-1);
                LineMovePlate(-1,-1);
                LineMovePlate(-1,1);
                LineMovePlate(1,-1);
                break;

            case "black_knight":
            case "white_knight":
                LMovePlate();
                break;

            case "black_bishop":
            case "white_bishop":
                LineMovePlate(1,1);
                LineMovePlate(1,-1);
                LineMovePlate(-1,1);
                LineMovePlate(-1,-1);                
                break;

            case "black_king":
            case "white_king":
                SurroundMovePlate();
                break;
            
            case "black_rook":
            case "white_rook":
                LineMovePlate(1,0);
                LineMovePlate(0,1);
                LineMovePlate(-1,0);
                LineMovePlate(0,-1);
                break;

            case "black_pawn":
                PawnMovePlate(xChessboard, yChessboard -1);
                break;

            case "white_pawn":
                PawnMovePlate(xChessboard, yChessboard +1);
                break;
        }
    }
    public void LineMovePlate(int xIncrement, int yIncrement)
    {
        GameScript sc = controller.GetComponent<GameScript>();
        int x = xChessboard + xIncrement,  y = yChessboard + yIncrement;

        while(sc.IsPositionOnBoard(x,y) && sc.GetPosition(x,y) == null)
        {
            MovePlateSpawn(x,y);
            x += xIncrement;
            y += yIncrement;
        }
        if(sc.IsPositionOnBoard(x,y) && sc.GetPosition(x,y).GetComponent<ChesspieceScript>().player != player)
        {
            MovePlateAttackSpawn(x,y);
        }
    }
    public void LMovePlate()
    {
        PointMovePlate(xChessboard + 1, yChessboard + 2);
        PointMovePlate(xChessboard - 1, yChessboard + 2);

        PointMovePlate(xChessboard + 2, yChessboard + 1);
        PointMovePlate(xChessboard + 2, yChessboard - 1);

        PointMovePlate(xChessboard + 1, yChessboard - 2);
        PointMovePlate(xChessboard - 1, yChessboard - 2);

        PointMovePlate(xChessboard - 2, yChessboard + 1);
        PointMovePlate(xChessboard - 2, yChessboard - 1);

    }
    public void SurroundMovePlate()
    {
        PointMovePlate(xChessboard, yChessboard + 1);
        PointMovePlate(xChessboard, yChessboard - 1);
        PointMovePlate(xChessboard - 1, yChessboard + 0);
        PointMovePlate(xChessboard - 1, yChessboard - 1);
        PointMovePlate(xChessboard - 1, yChessboard + 1);
        PointMovePlate(xChessboard + 1, yChessboard + 0);
        PointMovePlate(xChessboard + 1, yChessboard - 1);
        PointMovePlate(xChessboard + 1, yChessboard + 1);
    }
    public void PointMovePlate(int x, int y)
    {
        GameScript sc = controller.GetComponent<GameScript>();
        if(sc.IsPositionOnBoard(x,y))
        {
            GameObject chesspiece = sc.GetPosition(x,y);
            if(chesspiece == null)
            {
                MovePlateSpawn(x,y);
            } 
            else if(chesspiece.GetComponent<ChesspieceScript>().player != player)
            {
                MovePlateAttackSpawn(x,y);
            }
        }
    }
    public void PawnMovePlate(int x, int y)
    {
        GameScript sc = controller.GetComponent<GameScript>();
        if(sc.IsPositionOnBoard(x,y))
        {
            if(sc.GetPosition(x,y) == null)
            {
                MovePlateSpawn(x,y);
            }
            if(sc.IsPositionOnBoard(x + 1, y) && sc.GetPosition(x+1,y) != null && sc.GetPosition(x+1,y).GetComponent<ChesspieceScript>().player != player)
            {
                MovePlateAttackSpawn(x + 1, y);
            }
            if(sc.IsPositionOnBoard(x - 1, y) && sc.GetPosition(x-1,y) != null && sc.GetPosition(x-1,y).GetComponent<ChesspieceScript>().player != player)
            {
                MovePlateAttackSpawn(x - 1, y);
            }
        }
    }
    public void MovePlateSpawn(int matrixX, int matrixY)
    {
        float x = matrixX, y = matrixY;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        GameObject map = Instantiate(movePlate, new Vector3(x,y,-3.0f), Quaternion.identity);

        MovePlateScript mapScript = map.GetComponent<MovePlateScript>(); 
        mapScript.SetReference(gameObject);
        mapScript.SetCoordinates(matrixX, matrixY);
        
    }
        public void MovePlateAttackSpawn(int matrixX, int matrixY)
    {
        float x = matrixX, y = matrixY;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        GameObject map = Instantiate(movePlate, new Vector3(x,y,-3.0f), Quaternion.identity);

        MovePlateScript mapScript = map.GetComponent<MovePlateScript>(); 
        mapScript.IsAttacking = true;
        mapScript.SetReference(gameObject);
        mapScript.SetCoordinates(matrixX, matrixY);
        
    }
}