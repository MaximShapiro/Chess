using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


}
