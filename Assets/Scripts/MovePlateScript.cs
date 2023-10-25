using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlateScript : MonoBehaviour
{
    public GameObject controller;
    GameObject reference = null;

    int matrixX, matrixY;

    public bool IsAttacking = false; 

    public void Start()
    {
        if(IsAttacking)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f,0.0f,0.0f,1.0f);
        }
    }
    public void OnMouseUp() {

        controller = GameObject.FindGameObjectWithTag("GameController");

        if(IsAttacking)
        {
            GameObject chesspiece = controller.GetComponent<GameScript>().GetPosition(matrixX, matrixY);
            Destroy(chesspiece);
        }

        controller.GetComponent<GameScript>().SetPositionEmpty(reference.GetComponent<ChesspieceScript>().GetxChessboard(),
        reference.GetComponent<ChesspieceScript>().GetyChessboard());

        reference.GetComponent<ChesspieceScript>().SetxChessboard(matrixX);
        reference.GetComponent<ChesspieceScript>().SetyChessboard(matrixY);
        reference.GetComponent<ChesspieceScript>().SetCoordinates();

        controller.GetComponent<GameScript>().SetPosition(reference);

        reference.GetComponent<ChesspieceScript>().RemoveMovePlates();
    }
    public void SetCoordinates(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }
    public void SetReference(GameObject obj)
    {
        reference = obj;
    }
    public GameObject GetReference()
    {
        return reference;
    }
}
