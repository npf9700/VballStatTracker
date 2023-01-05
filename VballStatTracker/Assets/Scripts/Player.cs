using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum Position { OH, S, MB, L, DS, OPP, UNDEF};

    private string playerName;
    private int number;
    private Position position;
    private int currentCourtPos;
    private int baseDefPos;

    public string PlayerName
    {
        get { return playerName; }
        set { playerName = value; }
    }

    public int Number
    {
        get { return number; }
        set { number = value; }
    }

    public Position PlayerPosition
    {
        get { return position; }
        set { position = value; }
    }

    public int CurrentCourtPos
    {
        get { return currentCourtPos; }
    }

    public int BaseDefPos
    {
        get { return baseDefPos; }
        set { baseDefPos = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerName = "Player";
        number = 0;
        position = Position.UNDEF;
        currentCourtPos = 0;
        baseDefPos = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
