using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContactMarker : MonoBehaviour
{
    private Vector2 contactPos;
    private int contactNum;
    //private Player contacter;
    private bool isAttack;

    private void Start()
    {
        contactPos = this.transform.position;
    }

    public Vector2 ContactPos
    {
        get { return contactPos; }
    }

    public int ContactNum
    {
        get { return contactNum; }
        set { 
            contactNum = value;
            this.transform.GetChild(0).GetComponent<TextMeshPro>().text = "" + contactNum;
        }
    }

    public bool IsAttack
    {
        get { return isAttack; }
        set { isAttack = value; }
    }

    //public Player GetPlayer()
    //{
    //    return contacter;
    //}


}
