using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //declaration of new variables
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    public float speed = 0;
    public TextMeshProUGUI countText;

    private int count;

    public GameObject winTextObject;

    //Sound effects
    public AudioSource audioSource;

    public AudioClip pickupSound;
    public AudioClip winSound;
    public AudioClip loseSound;

    private bool hasWon = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();

        winTextObject.SetActive(false);
        
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            audioSource.PlayOneShot(pickupSound); //So
            SetCountText();
        }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        //assign movement variables
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 10 && !hasWon) //count ha de ser >= que el nombre de PickUps
        {
            hasWon = true;
            winTextObject.SetActive(true);
            audioSource.PlayOneShot(winSound); //So
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //So
            AudioSource.PlayClipAtPoint(loseSound, transform.position);

            //Destroy current object
            Destroy(gameObject);

            //Update the winText to display "You Lose!"
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        }
    }

}
