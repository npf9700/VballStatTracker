using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{
    private List<Player> players;
    private List<Player> rotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPlayer(Player p)
    {
        players.Add(p);
    }

    public void RemovePlayer(Player p)
    {
        players.Remove(p);
    }

    public void Rotate()
    {
        for(int i = players.Count; i > 0; i--)
        {
            players[i] = players[i - 1];
        }

        players[0] = players[6];
        players[6] = null;
    }
}
