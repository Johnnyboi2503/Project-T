using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;

    Rigidbody2D RB;

    // for chracter sprites
    SpriteRenderer sr;

    // object the player is holding
    public Transform holding;
    private GameObject heldObject;
    public bool isHolding;
    private Vector2 lastMoveDir = Vector2.down; // default facing down

    // enemy attack
    public GameObject melee;
    public bool holdingMelee;
    //public Transform gun;

    //public Sprite upSprite;
    //public Sprite downSprite;
    //public Sprite rightSprite;
    //public Sprite leftSprite;

    // bools for keys to open things
    public bool hasKeyCard1;
    public bool hasKeyCard2;
    public bool hasKeyCard3;
    public bool hasPannelKey1;
    public bool hasPannelKey2;
    public bool hasPannelKey3;
    public bool PannelOn1;
    public bool PannelOn2;
    public bool PannelOn3;

    // to show player the pannel is on/correct
    public GameObject doorPannel1;
    public GameObject doorPannel2;
    public GameObject doorPannel3;

    public GameObject ExitDoors;
    public GameObject RepairPannelDoor;

    // the equipmeant part
    public GameObject builtBodyPart;
    public GameObject builtLookPart;
    public GameObject builtFloatPart;
    public GameObject builtMotorPart;
    public bool holdingBodyPart;
    public bool holdingFloatPart;
    public bool holdingLookPart;
    public bool holdingMotorPart;
    public bool BuiltBodyPart;
    public bool BuiltLookPart;
    public bool BuiltFloatPart;
    public bool BuiltMotorPart;
    public bool allPartsBuilt;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        Cursor.visible = false;

        melee.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //variables to mirror the player
        Vector3 newScale = transform.localScale;
        float currentScale = Mathf.Abs(transform.localScale.x); //take absolute value of the current x scale

        // movement
        Vector2 vel = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            //sr.sprite = upSprite;
            vel.y = speed;
            newScale.y = currentScale;
            holding.localPosition = new Vector3(0f, 1.6f, 0f);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            //sr.sprite = rightSprite;
            vel.x = speed;
            newScale.x = currentScale;
            holding.localPosition = new Vector3(1.6f, 0f, 0f);
            if(holdingBodyPart == true)
            {
                holding.localPosition = new Vector3(2.5f, 0f, 0f);
            }
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            //sr.sprite = downSprite;
            vel.y -= speed;
            newScale.y = -currentScale;
            holding.localPosition = new Vector3(0f, -1.6f, 0f);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            //sr.sprite = leftSprite;
            vel.x -= speed;
            newScale.x = -currentScale;
            holding.localPosition = new Vector3(-1.6f, 0f, 0f);
            if(holdingBodyPart == true)
            {
                holding.localPosition = new Vector3(-2.5f, 0f, 0f);
            }
            if (holdingFloatPart == true)
            {
                holding.localPosition = new Vector3(-2.9f, 0f, 0f);
            }
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 6.2f;
        }
        else
        {
            speed = 5;
        }
        RB.linearVelocity = vel;

        if (Input.GetKey(KeyCode.Q) && heldObject != null)
        {
            DropHeldObject();
            isHolding = false;
        }

        // equip melee
        if (Input.GetKey(KeyCode.Alpha1) && holdingMelee == false && heldObject == null)
        {
            melee.SetActive(true);
            holdingMelee = true;
            Cursor.visible = true;
        }
        // unequip melee
        if (Input.GetKey(KeyCode.Alpha2) && holdingMelee == true)
        {
            melee.SetActive(false);
            holdingMelee = false;
            Cursor.visible = false;
        }

        if (PannelOn1 == true && PannelOn2 == true && PannelOn3 == true)
        {
            ExitDoors.SetActive(false);
        }

        if(BuiltBodyPart == true && BuiltFloatPart == true && BuiltMotorPart == true && BuiltLookPart == true)
        {
            RepairPannelDoor.SetActive(false);
            allPartsBuilt = true;
        }
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        // to open the gates when player collect the key
        if (collision.gameObject.tag.Equals("KeyCard1"))
        {
            PickUpObject(collision.gameObject);
            hasKeyCard1 = true;
            isHolding = true;
        }
        if (collision.gameObject.tag.Equals("Gate1") && hasKeyCard1 == true)
        {
            Destroy(collision.gameObject);
            Destroy(heldObject);
            hasKeyCard1 = false;
        }

        if (collision.gameObject.tag.Equals("KeyCard2"))
        {
            PickUpObject(collision.gameObject);
            hasKeyCard2 = true;
            isHolding = true;
        }
        if (collision.gameObject.tag.Equals("Gate2") && hasKeyCard2 == true)
        {
            Destroy(collision.gameObject);
            Destroy(heldObject);
            hasKeyCard2 = false;
        }

        // to open the exit doors when player collects pannel keys
        if (collision.gameObject.tag.Equals("PannelKey1"))
        {
            PickUpObject(collision.gameObject);
            hasPannelKey1 = true;
            isHolding = true;
        }
        if (collision.gameObject.tag.Equals("DoorPannel1") && hasPannelKey1 == true)
        {
            PannelOn1 = true;
            doorPannel1.SetActive(true);
            Destroy(heldObject);
            hasPannelKey1 = false;
        }

        if (collision.gameObject.tag.Equals("PannelKey2"))
        {
            PickUpObject(collision.gameObject);
            hasPannelKey2 = true;
            isHolding = true;
        }
        if (collision.gameObject.tag.Equals("DoorPannel2") && hasPannelKey2 == true)
        {
            PannelOn2 = true;
            doorPannel2.SetActive(true);
            Destroy(heldObject);
            hasPannelKey2 = false;
        }

        if (collision.gameObject.tag.Equals("PannelKey3"))
        {
            PickUpObject(collision.gameObject);
            hasPannelKey3 = true;
            isHolding = true;
        }
        if (collision.gameObject.tag.Equals("DoorPannel3") && hasPannelKey3 == true)
        {
            PannelOn3 = true;
            doorPannel3.SetActive(true);
            Destroy(heldObject);
            hasPannelKey3 = false;
        }

        // player to hold equipment for submarine
        if (collision.gameObject.tag.Equals("BodyPart"))
        {
            PickUpObject(collision.gameObject);
            isHolding = true;
            holdingBodyPart = true;
        }
        if (collision.gameObject.tag.Equals("LookPart"))
        {
            PickUpObject(collision.gameObject);
            isHolding = true;
            holdingLookPart = true;
        }
        if (collision.gameObject.tag.Equals("FloatPart"))
        {
            PickUpObject(collision.gameObject);
            isHolding = true;
            holdingFloatPart = true;
        }
        if (collision.gameObject.tag.Equals("MotorPart"))
        {           
            PickUpObject(collision.gameObject);
            isHolding = true;
            holdingMotorPart = true;
        }

        // player give the equipment to repair pannel to build submarine 
        if (collision.gameObject.tag.Equals("RepairPannel") && holdingBodyPart == true)
        {
            builtBodyPart.SetActive(true);
            Destroy(heldObject);
            BuiltBodyPart = true;
        }
        if (collision.gameObject.tag.Equals("RepairPannel") && holdingFloatPart == true)
        {
            builtFloatPart.SetActive(true);
            Destroy(heldObject);
            BuiltFloatPart = true;
        }
        if (collision.gameObject.tag.Equals("RepairPannel") && holdingLookPart == true)
        {
            builtLookPart.SetActive(true);
            Destroy(heldObject);
            BuiltLookPart = true;
        }
        if (collision.gameObject.tag.Equals("RepairPannel") && holdingMotorPart == true)
        {
            builtMotorPart.SetActive(true);
            Destroy(heldObject);
            BuiltMotorPart = true;
        }

        // when player builds submarine and touches it the player wins
        if (collision.gameObject.tag.Equals("Submarine") && allPartsBuilt == true)
        {
            // player wins
            SceneManager.LoadScene(1);
        }

        //if (collision.gameObject.TryGetComponent<Health>(out var health))
        //{
        //    health.Damage(amount: 3);
        //}
    }

    void PickUpObject(GameObject obj)
    {
        if (heldObject == null && holdingMelee == false)
        {
            heldObject = obj;
            obj.transform.SetParent(holding);
            obj.transform.localPosition = Vector3.zero;
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        }
    }

    void DropHeldObject()
    {
        if (heldObject != null)
        {
            heldObject.transform.SetParent(null);
            Rigidbody2D rb = heldObject.GetComponent<Rigidbody2D>();
            heldObject = null;
            hasKeyCard1 = false;
            hasKeyCard2 = false;
            hasPannelKey1 = false;
            hasPannelKey2 = false;
            hasPannelKey2 = false;
            holdingBodyPart = false;
            holdingFloatPart = false;
            holdingLookPart = false;
            holdingMotorPart = false;
        }
    }
}
