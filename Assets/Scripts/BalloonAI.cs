using UnityEngine;
using System.Collections;

public class BalloonAI : MonoBehaviour {

    public float speed = 0.1f;

    private Vector3 currentTarget;

	// Use this for initialization
	void Start ()
    {
        int mdx = Random.Range(-10, 10);
        int mdy = Random.Range(-10, 10);
        currentTarget = new Vector3(mdx, mdy);
    }
	
	// Update is called once per frame
	void Update ()
    {
        int mdx = Random.Range(-1, 1);
        int mdy = Random.Range(-1, 1);
        currentTarget += new Vector3(mdx, mdy);
        if (Random.Range(0, 50) == 1)
        {
            mdx = Random.Range(-10, 10);
            mdy = Random.Range(-10, 10);
            currentTarget = transform.position + new Vector3(mdx, mdy);
        }
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed);
        Vector3 moveDirection = transform.position - currentTarget;
        if (moveDirection != Vector3.zero)//2016-04-16: copied from an answer by robertbu (http://answers.unity3d.com/questions/630670/rotate-2d-sprite-towards-moving-direction.html)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        }
    }


    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag.Equals("spike"))
        {
            Debug.Log("AI destroyed");
            Destroy(gameObject);
        }
    }
}
