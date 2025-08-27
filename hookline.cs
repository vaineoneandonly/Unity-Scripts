using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class hookline : MonoBehaviour
{
    [SerializeField] const float changeRate = 0.25f;

    [SerializeField] float pushForce = 3.0f;
    Vector3 pushDir = new Vector3(0.0f, 0.0f, 0.0f);

    Vector3 Distort;
    Vector3 Dislocate;

    Vector3 VerticalAdjustment = new Vector3(4.165f, -1.25f, 9.0f);
    Vector3 HorizontalAdjustment = new Vector3(4.165f, -1.25f, 8.75f);

    bool pullingBack = false;

    [SerializeField] GameObject pRef;
    string pDir;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pRef = GameObject.Find("Player");
        pDir = pRef.GetComponent<movimento>().getDir();

        HandleDirection();
    }

    // Update is called once per frame
    void Update()
    {
        if (pullingBack)
        {
            this.transform.localScale -= Distort;
            this.transform.position -= Dislocate;

            if (this.transform.localScale.x + this.transform.localScale.y + this.transform.localScale.z == 0) Destroy(this.gameObject);
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            this.transform.localScale += Distort;
            this.transform.position += Dislocate;
        }
        else Destroy(this.gameObject);


    }

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Enemy")
        {
            Debug.Log("foi");

            Rigidbody enemyBody = c.gameObject.GetComponent<Rigidbody>();

            enemyBody.linearVelocity = Vector3.zero;
            enemyBody.AddForce(pushDir, ForceMode.Impulse);

            Destroy(this.gameObject);
        }
        else if (c.gameObject.tag == "WorldObject")
        {
            pullingBack = true;
            Rigidbody objectBody = c.gameObject.GetComponent<Rigidbody>();

            objectBody.AddForce(pushDir * 3.0f * -1, ForceMode.Impulse);

        }
    }

    void HandleDirection()
    {
        switch (pDir)
        {
            case "Left":
                Distort = new Vector3(changeRate * -1, 0.0f, 0.0f);
                Dislocate = new Vector3(changeRate / 2 * -1, 0.0f, 0.0f);

                pushDir.x = pushForce * -1;

                this.transform.position += HorizontalAdjustment;
                break;

            case "Right":
                Distort = new Vector3(changeRate, 0.0f, 0.0f);
                Dislocate = new Vector3(changeRate / 2, 0.0f, 0.0f);

                pushDir.x = pushForce;

                this.transform.position += HorizontalAdjustment;
                break;

            case "Down":
                Distort = new Vector3(0.0f, 0.0f, changeRate * -1);
                Dislocate = new Vector3(0.0f, 0.0f, changeRate / 2 * -1);

                pushDir.z = pushForce * -1;

                this.transform.position += VerticalAdjustment;
                break;

            case "Up":
                Distort = new Vector3(0.0f, 0.0f, changeRate);
                Dislocate = new Vector3(0.0f, 0.0f, changeRate / 2);

                pushDir.z = pushForce;

                this.transform.position += VerticalAdjustment;
                break;
        }
    }
}