using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class movimento : MonoBehaviour
{
    [SerializeField] float vel = 0.01f;
    string Dir;
    [SerializeField] GameObject hookPrefab;
    Rigidbody myBody;

    float dx;
    float dz;

    int lives = 3;
    int healthPoints = 5;

    [SerializeField] float knockbackForce = 15.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myBody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        handleMovement();
        updateDir();


        handleHP();

        Debug.Log(healthPoints);
        Debug.Log(lives);

        handleHook();
    }

    void handleHP()
    {
        if (healthPoints >= 7) lives += 1;
        else if (healthPoints < 0)
        {
            lives -= 1;
            healthPoints = 3;
        }
        if (lives < 0) Debug.Log("GAMEOVER");

    }

    void handleHook()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(hookPrefab, this.transform, worldPositionStays: false);
        }
    }

    void handleMovement()
    {
        dx = Input.GetAxis("Horizontal");
        dz = Input.GetAxis("Vertical");

        Vector3 mv = new Vector3(dx, 0.0f, dz);

        if (mv.magnitude > 1.0f) mv.Normalize();

        transform.position += mv * vel;

    }

    void updateDir()
    {
        if (Input.GetKey(KeyCode.A)) Dir = "Left";
        else if (Input.GetKey(KeyCode.D)) Dir = "Right";
        else if (Input.GetKey(KeyCode.W)) Dir = "Up";
        else if (Input.GetKey(KeyCode.S)) Dir = "Down";
    }

    public string getDir()
    {
        return Dir;
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.CompareTag("Enemy"))
        {
            healthPoints -= 2;
            switch (Dir)
            {
                case "Left":
                    myBody.AddForce(Vector3.right * knockbackForce, ForceMode.Impulse);
                    break;

                case "Right":
                    myBody.AddForce(Vector3.left * knockbackForce, ForceMode.Impulse);
                    break;

                case "Up":
                    myBody.AddForce(Vector3.back * knockbackForce, ForceMode.Impulse);
                    break;

                case "Down":
                    myBody.AddForce(Vector3.forward * knockbackForce, ForceMode.Impulse);
                    break;
            }
        }
    }
}
