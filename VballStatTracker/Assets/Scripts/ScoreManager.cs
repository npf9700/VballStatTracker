using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int leftTeamScore;
    private int rightTeamScore;

    public TextMeshPro leftScoreText;
    public TextMeshPro rightScoreText;

    // Start is called before the first frame update
    void Start()
    {
        leftTeamScore = 0;
        rightTeamScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        leftScoreText.text = "" + leftTeamScore;
        rightScoreText.text = "" + rightTeamScore;
    }


    public void AddToLeftTeam()
    {
        leftTeamScore++;
    }


    public void AddToRightTeam()
    {
        rightTeamScore++;
    }
}
