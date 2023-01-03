using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Camera mainCam;

    public Transform net;
    public GameObject leftCourt;
    public GameObject rightCourt;

    public ContactMarker contactMarker;
    private List<ContactMarker> contacts;
    private int currentContact;

    private int leftTeamScore;
    private int rightTeamScore;

    private bool ballOnLeft;
    private bool isServing;
    private bool isReceive;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        contacts = new List<ContactMarker>();
        currentContact = 1;

        leftTeamScore = 0;
        rightTeamScore = 0;

        ballOnLeft = true;
        isServing = true;
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

            createNewContact(worldPos, isAttack);//New marker is created

            currentContact++;
            return;
        }
        else if(isServing)
        {
            ballOnLeft = IsContactOnLeft(worldPos);//Sets initial side of the net the ball is on

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
}
