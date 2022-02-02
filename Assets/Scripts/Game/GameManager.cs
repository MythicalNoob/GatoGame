using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region variables

    public GameObject spaceManager;
    public static int row = 3, column = 3;
    public GameObject[,] Spaces = new GameObject[row, column];
    public GameObject xWinner;
    public GameObject oWinner;
    public GameObject draw;
    public GameObject menuButton;

    [SerializeField]GameObject xPlayer;
    [SerializeField]GameObject oPlayer;
    [SerializeField] List<GameObject> mySpaces = new List<GameObject>();


    private string[,] checkWinner = new string[row, column];
    private bool[,] isEmpty = new bool[row, column];

    bool xPlayerTurn = true;
    bool oPlayerTurn = false;
    bool isFinish = false;
    bool gameOver = false;

    int counter = 0;
    int turnCounter;
    ButtonPosition myPos;

    #endregion

    #region Body

    private void Start()
    {
        //Inicializo los turnos en 0 y obtengo el gameobject con el componente buttonposition
        turnCounter = 0;
        myPos = spaceManager.GetComponent<ButtonPosition>();

        //desactivo pantallas condicionales
        xWinner.SetActive(false);
        oWinner.SetActive(false);
        draw.SetActive(false);
        menuButton.SetActive(false);

        //inicializo mis arreglos y les doy valores
        for (int i = 0; i < row; i++)
        {
           for(int x = 0; x < column; x++)
            {
                checkWinner[i,x] = "empty";
                isEmpty[i,x] = true;
                Spaces[i,x] = mySpaces[counter];
                counter++;
            }
        }
    }

    private void Update()
    {
        CheckTurn();
    }

    //Reviso de quien es el turno de tirar
    public void CheckTurn()
    {
        if(xPlayerTurn == true && oPlayerTurn == false && gameOver == false)
        {
            XPTurn();
        }
        else if(oPlayerTurn == true && xPlayerTurn == false && gameOver == false)
        {
            OPTurn();
        }
    }

    //Para cada caso inicializo la corrutina de verificacion de espacio libre, calculo si hay ganador o empate y modifico valores
    void XPTurn()
    {
        Debug.Log("TURNO X");
        if(myPos.gotPos == true)
        {
            
            StartCoroutine(SpaceSelector());
            turnCounter++;
            StartCoroutine(Winner());
            isFinish = false;
            xPlayerTurn = false;
            oPlayerTurn = true;
            myPos.gotPos = false;
        }
    }

    void OPTurn()
    {
        Debug.Log("TURNO O");
        if (myPos.gotPos == true)
        {
            StartCoroutine(SpaceSelector());
            turnCounter++;
            StartCoroutine(Winner());
            isFinish = false;
            oPlayerTurn = false;
            xPlayerTurn = true;
            myPos.gotPos = false;
        }
    }

    //Cada que selecciono un espacio verifico si esta  vacio, si lo esta hago una instancia de sprite X u O, ademas de agregar en otro arreglo X u O para saber que espacio ocupa
    IEnumerator SpaceSelector()
    {
        Debug.Log("estoyencorrutina");
       
        for (int i = 0; i < row; i++)
        {
            for (int x = 0; x < column; x++)
            {
                if (myPos.tran.position == Spaces[i, x].transform.position && xPlayerTurn == true && isEmpty[i, x] == true)
                {
                    GameObject turn = Instantiate(xPlayer, new Vector3(myPos.tran.position.x, myPos.tran.position.y, 0), Quaternion.identity);
                    isEmpty[i, x] = false;
                    checkWinner[i, x] = "X";
                    isFinish = true;
                }

                if (myPos.tran.position == Spaces[i, x].transform.position && oPlayerTurn == true && isEmpty[i, x] == true)
                {
                    GameObject turn = Instantiate(oPlayer, new Vector3(myPos.tran.position.x, myPos.tran.position.y, 0), Quaternion.identity);
                    isEmpty[i, x] = false;
                    checkWinner[i, x] = "O";
                    isFinish = true;
                }
            }
        }


        Debug.Log("salicorrutina");
        yield return new WaitUntil(() => isFinish == true);
    }

    //Calculo si en las diferentes posibilidades gano X u O, o si empataron
    IEnumerator Winner()
    {
        if (checkWinner[0, 0] == "X" && checkWinner[1, 0] == "X" && checkWinner[2, 0] == "X" || checkWinner[0, 1] == "X" && checkWinner[1, 1] == "X" && checkWinner[2, 1] == "X" || checkWinner[0, 2] == "X" && checkWinner[1, 2] == "X" && checkWinner[2, 2] == "X" || checkWinner[0, 0] == "X" && checkWinner[0, 1] == "X" && checkWinner[0, 2] == "X" || checkWinner[1, 0] == "X" && checkWinner[1, 1] == "X" && checkWinner[1, 2] == "X" || checkWinner[2, 0] == "X" && checkWinner[2, 1] == "X" && checkWinner[2, 2] == "X" || checkWinner[0, 0] == "X" && checkWinner[1, 1] == "X" && checkWinner[2, 2] == "X" || checkWinner[0, 2] == "X" && checkWinner[1, 1] == "X" && checkWinner[2, 0] == "X")
        {
            xWinner.SetActive(true);
            menuButton.SetActive(true);
            gameOver = true;
        }
        else if (checkWinner[0, 0] == "O" && checkWinner[1, 0] == "O" && checkWinner[2, 0] == "O" || checkWinner[0, 1] == "O" && checkWinner[1, 1] == "O" && checkWinner[2, 1] == "O" || checkWinner[0, 2] == "O" && checkWinner[1, 2] == "O" && checkWinner[2, 2] == "O" || checkWinner[0, 0] == "O" && checkWinner[0, 1] == "O" && checkWinner[0, 2] == "O" || checkWinner[1, 0] == "O" && checkWinner[1, 1] == "O" && checkWinner[1, 2] == "O" || checkWinner[2, 0] == "O" && checkWinner[2, 1] == "O" && checkWinner[2, 2] == "O" || checkWinner[0, 0] == "O" && checkWinner[1, 1] == "O" && checkWinner[2, 2] == "O" || checkWinner[0, 2] == "O" && checkWinner[1, 1] == "O" && checkWinner[2, 0] == "O")
        {
            oWinner.SetActive(true);
            menuButton.SetActive(true);
            gameOver = true;
        }

        if(turnCounter == 9)
        {
            draw.SetActive(true);
            menuButton.SetActive(true);
            gameOver = true;
        }

        isFinish = true;

        yield return new WaitUntil(() => isFinish == true);
    }

    #endregion
}
