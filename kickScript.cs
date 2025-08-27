using UnityEngine;

public class kickScript : MonoBehaviour
{
    enum movingDirection
    {
        Still,
        GoingLeft,
        GoingUp,
        GoingDown,
        GoingRight
    }

    movingDirection myDirection = movingDirection.Still;

    [SerializeField] GameObject pRef;
    Rigidbody body;

    [SerializeField] float kickSpeed = 1.0f;

    string pDir;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (myDirection)
        {
            case movingDirection.GoingLeft:
                transform.position += new Vector3(-1.0f, 0.0f,  0.0f) * kickSpeed;
                break;

            case movingDirection.GoingRight:
                transform.position += new Vector3(1.0f, 0.0f,  0.0f) * kickSpeed;
                break;

            case movingDirection.GoingUp:
            transform.position += new Vector3(0.0f, 0.0f,  1.0f) * kickSpeed;    
                break;

            case movingDirection.GoingDown:
                transform.position += new Vector3(0.0f, 0.0f,  -1.0f) * kickSpeed;
                break;
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "WorldObject")
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Wall")
        {
            Debug.Log("foi");
            myDirection = movingDirection.Still;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            pDir = pRef.GetComponent<movimento>().getDir();
            if (Input.GetKey(KeyCode.F))
            {
                switch (pDir)
                {
                    case "Left":
                        myDirection = movingDirection.GoingLeft;
                        break;

                    case "Right":
                        myDirection = movingDirection.GoingRight;
                        break;

                    case "Up":
                        myDirection = movingDirection.GoingUp;
                        break;

                    case "Down":
                        myDirection = movingDirection.GoingDown;
                        break;
                }
            }
        }
    }
}
