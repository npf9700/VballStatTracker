using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ContactMarker contactMarker;
    private List<ContactMarker> contacts;
    private int currentContact;
    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        contacts = new List<ContactMarker>();
        currentContact = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextContact(Vector2 contactPos, bool isAttack)
    {
        Vector2 worldPos = mainCam.ScreenToWorldPoint(contactPos);
        ContactMarker newContact = Instantiate(contactMarker, new Vector3(worldPos.x, worldPos.y, -0.3f), Quaternion.identity);
        newContact.IsAttack = isAttack;
        newContact.ContactNum = currentContact;
        contacts.Add(newContact);
        
        currentContact++;
    }
}
