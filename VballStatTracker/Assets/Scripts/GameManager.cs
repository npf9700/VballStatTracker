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

        //Normal course of game after serve
        if (!isServing)
        {
            switch (ballOnLeft)
            {
                case true:
                    //If ball changes sides, clear current list of contacts and start over
                    if (worldPos.x > net.position.x)
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
                    if (worldPos.x < net.position.x)
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

            if (isReceive)
            {
                if (ballOnLeft)
                {
                    if (Contains(leftCourt, worldPos))
                        return;
                }

                if (!ballOnLeft)
                {
                    if (Contains(rightCourt, worldPos))
                        return;
                }
            }

                createNewContact(worldPos, isAttack);

            currentContact++;
            return;
        }
        else if(isServing)
        {
            ballOnLeft = Contains(leftCourt, worldPos);

            createNewContact(worldPos, isAttack);

            isServing = false;
            isReceive = true;
            return;
        }
    }

    //Collision detection to see if contact is within a certain area
    private bool Contains(GameObject side, Vector2 contactPos)
    {
        Vector2 CourtPos = side.transform.position;
        float width = side.GetComponent<SpriteRenderer>().size.x;
        float height = side.GetComponent<SpriteRenderer>().size.y;

        if (contactPos.x < CourtPos.x)
            return false;
        if (contactPos.y < CourtPos.y)
            return false;
        if (contactPos.x > CourtPos.x + width)
            return false;
        if (contactPos.y > CourtPos.y + height)
            return false;

        return true;
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
