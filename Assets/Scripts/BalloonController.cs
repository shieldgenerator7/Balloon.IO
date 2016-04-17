using UnityEngine;
using UnityEngine.Networking;
//using static UnityEngine.Networking.NetworkBehaviour;
using System.Collections;

public class BalloonController : NetworkBehaviour {

    [SyncVar]
    public float speed;
    [SyncVar]
    public float baseSpeed;
    [SyncVar]
    public float speedMultiplier;

    private Rigidbody2D rb2d;

    override public void OnStartLocalPlayer()
    {
        init();
    }

    void Start()
    {
        init();
    }

    void init()
    {
        if (isLocalPlayer)
        {
            Camera.main.GetComponent<CameraController>().player = this.gameObject;
            rb2d = GetComponent<Rigidbody2D>();
            int low = 150;
            float red = ((float)Random.Range(low, 255)) / 255;
            float green = ((float)Random.Range(low, 255)) / 255;
            float blue = ((float)Random.Range(low, 255)) / 255;
            Debug.Log("red: " + red + " green: " + green + " blue: " + blue);
            GetComponent<SpriteRenderer>().color = new Color(red, green, blue);
        }
    }

    // Update is called once per frame
   void Update()
    {
        if (isLocalPlayer)
        {
            //Speed
            CmdUpdateSpeed(Input.GetMouseButton(0));
            Vector3 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CmdMovePlayer(mousePos);
        }
    }

   void CmdUpdateSpeed(bool boost)
    {
        speed = baseSpeed;
        if (boost)
        {
            speed *= speedMultiplier;
        }
    }

    void CmdMovePlayer(Vector3 pos)
    {
        rb2d.velocity = Vector3.zero;
        //transform.position = Vector3.MoveTowards(transform.position, mousePos, speed);
        Vector3 moveDirection = transform.position - pos;
        rb2d.velocity = -moveDirection.normalized * speed * 10;
        if (moveDirection != Vector3.zero)//2016-04-16: copied from an answer by robertbu (http://answers.unity3d.com/questions/630670/rotate-2d-sprite-towards-moving-direction.html)
        {
            float adjust = 90;
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg + adjust;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag.Equals("spike") && ! transform.GetChild(0).gameObject.Equals(coll.gameObject))
        {
            Debug.Log("Player destroyed by: "+coll.gameObject);
            Destroy(gameObject);
        }
    }
}
