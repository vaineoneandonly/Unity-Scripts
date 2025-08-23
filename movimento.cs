using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class movimento : MonoBehaviour
{
    [SerializeField] float vel = 0.01f;
    string Dir;
    [SerializeField] GameObject hookPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float dx = Input.GetAxis("Horizontal");
        float dz = Input.GetAxis("Vertical");

        Vector3 mv = new Vector3(dx, 0.0f, dz);

        if (mv.magnitude > 1.0f) mv.Normalize();

        transform.position += mv * vel;

        updateDir();


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(hookPrefab, this.transform, worldPositionStays: false);
        }

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
}
