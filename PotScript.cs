using System.Numerics;
using UnityEngine;

public class PotScript : MonoBehaviour
{
    enum ObjectState
    {
        onGround,
        onGoofy,
        airborne
    }

    [SerializeField] float startJSpd = 0.2f;    
    float actualJSpd = 0.2f;
    [SerializeField] float jChg = 0.01f;

    [SerializeField] float axisSpd = 0.2f;
    float xSpd = 0.0f;
    float zSpd = 0.0f;


    ObjectState currentState;
    [SerializeField] GameObject pRef;
    string pDirRef;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pRef = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == ObjectState.onGoofy)
        {
            transform.position = pRef.transform.position + new UnityEngine.Vector3(0.0f, 1.0f, 0.0f);

            if (Input.GetKeyDown(KeyCode.F))
            {
                currentState = ObjectState.airborne;
                pDirRef = pRef.GetComponent<movimento>().getDir();

                switch (pDirRef)
                {
                    case "Up":
                        zSpd = axisSpd;
                        break;

                    case "Down":
                        zSpd = -axisSpd;
                        break;

                    case "Left":
                        xSpd = -axisSpd;
                        break;

                    case "Right":
                        xSpd = +axisSpd;
                        break;
                }
            }
        }
        else if (currentState == ObjectState.airborne)
        {
            transform.position += new UnityEngine.Vector3(xSpd, actualJSpd, zSpd);
            actualJSpd -= jChg;
        }

    }

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Player")
        {
            if (currentState != ObjectState.onGoofy)
            {
                currentState = ObjectState.onGoofy;
                actualJSpd = startJSpd;

                c.gameObject.GetComponent<Rigidbody>().linearVelocity = UnityEngine.Vector3.zero;
            }
        }
    }
}
