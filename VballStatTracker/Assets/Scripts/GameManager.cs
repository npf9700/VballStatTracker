using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Camera mainCam;
    private ScoreManager scrMgr;

    public Transform net;
    public GameObject leftCourt;
    public GameObject rightCourt;

    public ContactMarker contactMarker;
    private List<ContactMarker> contacts;
    private int currentContact;

    private bool ballOnLeft;
    private bool isServing;
    private bool isFirstServe;
    private bool isReceive;

    private void Awake()
    {
        mainCam = Camera.main;
        scrMgr = this.GetComponent<ScoreManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        contacts = new List<ContactMarker>();
        currentContact = 1;

        ballOnLeft = true;
        isServing = true;
        isFirstServe = true;
        isReceive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextContact(Vector2 contactPos, bool isAttack)
    {
        //Convert coords to world space
        Vector2 worldPos = mainCam.ScreenToWorldPoint(contactPos);

        Debug.Log("Ball on left? " + ballOnLeft);
        Debug.Log("Contact on Left? " + IsContactOnLeft(worldPos));

        //Prevents the user from making the serve and receive on the same side of the net
        if (isReceive)
        {
            if (ballOnLeft)
            {
                if (IsContactOnLeft(worldPos))
                    return;
            }

            if (!ballOnLeft)
            {
                if (!IsContactOnLeft(worldPos))
                    return;
            }
            isReceive = false;
        }

        //Normal course of game after serve
        if (!isServing)
        {
            switch (ballOnLeft)
            {
                case true:
                    //If ball changes sides, clear current list of contacts and start over
                    if (!IsContactOnLeft(worldPos))
                    {
                        currentContact = 1;
                        for (int i = 0; i < contacts.Count; i++)
                        {
                            Destroy(contacts[i].gameObject);
                        }
                        contacts.Clear();
                        ballOnLeft = false;
                    }

                    break;
                case false:
                    if (IsContactOnLeft(worldPos))
                    {
                        currentContact = 1;
                        for (int i = 0; i < contacts.Count; i++)
                        {
                            Destroy(contacts[i].gameObject);
                        }
                        contacts.Clear();
                        ballOnLeft = true;
                    }

                    break;
            }

            //If 4 contacts, other team gets point
            if (currentContact >= 4)
            {
                PointScored();
                return;
            }

            createNewContact(worldPos, isAttack);//New marker is created

            currentContact++;
            return;
        }
        else if(isServing)
        {
            //If its the first serve, the user picks which side serves first (SO FAR - This will be changed once the results of the coin toss are implmented)
            if (isFirstServe)
            {
                ballOnLeft = IsContactOnLeft(worldPos);//Sets initial side of the net the ball is on
                isFirstServe = false;
            }
            else//If its not the first serve, the player is limited to the side that made the last point
            {
                if (ballOnLeft)
                {
                    if (!IsContactOnLeft(worldPos))
                        return;
                }

                if (!ballOnLeft)
                {
                    if (IsContactOnLeft(worldPos))
                        return;
                }
            }

            createNewContact(worldPos, isAttack);

            isServing = false;
            isReceive = true;
            return;
        }
    }

    //Determines which side of the court the user had tapped
    private bool IsContactOnLeft(Vector2 contactPos)
    {
        if (contactPos.x < net.position.x)
            return true;

        return false;
    }

    //Helper method to set up the new contactMarker with proper values
    private void createNewContact(Vector2 worldPos, bool isAttack)
    {
        ContactMarker newContact = Instantiate(contactMarker, new Vector3(worldPos.x, worldPos.y, -0.3f), Quaternion.identity);
        newContact.IsAttack = isAttack;
        newContact.ContactNum = currentContact;
        contacts.Add(newContact);
    }

    private void PointScored()
    {
        if (ballOnLeft)
        {
            scrMgr.AddToRightTeam();
            ballOnLeft = false;
        }
        else
        {
            scrMgr.AddToLeftTeam();
            ballOnLeft = true;
        }

        currentContact = 1;
        isServing = true;
        isReceive = false;

        for (int i = 0; i < contacts.Count; i++)
        {
            Destroy(contacts[i].gameObject);
        }
        contacts.Clear();
    }
}
