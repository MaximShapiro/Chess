using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine; 
using UnityEngine.SceneManagement;


public class GameScript : MonoBehaviour
{
    public GameObject chesspiece;
    private GameObject[,] positionsChesspiece = new GameObject[8,8];
    private GameObject[] playerBlack, playerWhite = new GameObject[16];
    private string currentPlayer = "white";
    private bool isGameOver = false;
    public void Start()
    {
      playerWhite = new GameObject[]
      {
        Create("white_rook",0,0), Create("white_knight",1,0), 
        Create("white_bishop",2,0), Create("white_queen",3,0), 
        Create("white_king",4,0), Create("white_bishop",5,0), 
        Create("white_knight",6,0), Create("white_rook",7,0),

        Create("white_pawn",0,1), Create("white_pawn",1,1),
        Create("white_pawn",2,1), Create("white_pawn",3,1),
        Create("white_pawn",4,1), Create("white_pawn",5,1),
        Create("white_pawn",6,1), Create("white_pawn",7,1)
      };

      playerBlack = new GameObject[]
      {
        Create("black_rook",0,7), Create("black_knight",1,7), 
        Create("black_bishop",2,7), Create("black_queen",3,7), 
        Create("black_king",4,7), Create("black_bishop",5,7), 
        Create("black_knight",6,7), Create("black_rook",7,7),

        Create("black_pawn",0,6), Create("black_pawn",1,6),
        Create("black_pawn",2,6), Create("black_pawn",3,6),
        Create("black_pawn",4,6), Create("black_pawn",5,6),
        Create("black_pawn",6,6), Create("black_pawn",7,6)
      };
      for(int i = 0; i < playerBlack.Length; i++)
      {
        SetPosition(playerBlack[i]);
        SetPosition(playerWhite[i]);
      }
    }

    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(chesspiece, new Vector3(0, 0, -1), Quaternion.identity);
        ChesspieceScript cp = obj.GetComponent<ChesspieceScript>();
        cp.name = name;
        cp.SetxChessboard(x);
        cp.SetyChessboard(y);
        cp.Activate();
        SetPosition(obj);
        return obj;
    }
    public void SetPosition(GameObject obj)
    {
      
        ChesspieceScript cp = obj.GetComponent<ChesspieceScript>();
        positionsChesspiece[cp.GetxChessboard(), cp.GetyChessboard()] = obj;
    }

    public void SetPositionEmpty(int x, int y)
    {
        positionsChesspiece[x,y] = null;
    }

    public GameObject GetPosition(int x, int y)
    {
      return positionsChesspiece[x,y];
    }

    public bool IsPositionOnBoard(int x, int y)
    {
      if(x < 0 || y < 0 || x >= positionsChesspiece.GetLength(0) || y >= positionsChesspiece.GetLength(1)) return false;
      return true;
    }
    public string GetCurrentPlayer()
    {
      return currentPlayer;
    }
    public bool IsGameOver()
    {
      return isGameOver;
    }
    public void NextTurn()
    {
        if(currentPlayer == "white")
        
        {
          currentPlayer = "black";
        }

        else
        {
          currentPlayer = "white";
        }
    }
    public void Update() 
    {
      if(isGameOver == true && Input.GetMouseButtonDown(0))
      {
        isGameOver = false;
        SceneManager.LoadScene("Game");
      }
    }

    public void Winner(string playerWinner)
    {
      isGameOver = true;
      GameObject.FindGameObjectWithTag("WinnerText").GetComponent<TextMeshProUGUI>().enabled = true;
      GameObject.FindGameObjectWithTag("WinnerText").GetComponent<TextMeshProUGUI>().text = playerWinner + " won!";
      GameObject.FindGameObjectWithTag("RestartText").GetComponent<TextMeshProUGUI>().enabled = true;
    } 
}