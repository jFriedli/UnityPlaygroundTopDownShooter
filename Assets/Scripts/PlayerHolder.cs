using UnityEngine;

public class PlayerHolder : MonoBehaviour
{
    public enum Weapon { Simple, PassThrough}

    public Weapon weapon;
    public bool oneShot = false;
    public float movementSpeed;
    public float rotationSpeed;

    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float groundDistance = 0.4f;

    public int points = 0;

    public float attackDelay = 1.0f;

    public Transform playerModelTransform;
    public Transform groundCheck;
    public LayerMask groundMask;

    public Transform bulletSpawnTransform;
    public GameObject simpleBullet;
    public GameObject passThroughBullet;

    public bool projectileInAir = false;

    GameManager gameManager;
    CharacterController controller;

    Vector3 velocity;
    bool grounded;

    float lastAttacked;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!gameManager.gameHasEnded)
        {
            doRotation();
            doMovement();
        }

    }

    // Note: Can't do button detection in FixedUpdate
    private void Update()
    {
        if (!gameManager.gameHasEnded)
        {
            doShoot();
            doJump();
            doSwitchWeapon();
        }
    }

    void doRotation()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDist;

        if (playerPlane.Raycast(ray, out hitDist))
        {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Quaternion playerRotation = Quaternion.LookRotation(targetPoint - transform.position);
            playerRotation.x = 0.0f;
            playerRotation.z = 0.0f;
            playerModelTransform.rotation = Quaternion.Slerp(playerModelTransform.rotation, playerRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void doMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = 0.0f;
        float z = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(x, y, z);
        controller.Move(movement * movementSpeed * Time.deltaTime);
    }

    void doShoot()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > lastAttacked + attackDelay && oneShotOk())
        {

            FindObjectOfType<AudioManager>().play("Shot");

            switch (weapon)
            {
                case Weapon.Simple:
                    Instantiate(simpleBullet.transform, bulletSpawnTransform.position, bulletSpawnTransform.rotation);
                    break;
                case Weapon.PassThrough:
                    Instantiate(passThroughBullet.transform, bulletSpawnTransform.position, bulletSpawnTransform.rotation);
                    break;

            }
            lastAttacked = Time.time;
        }
    }

    bool oneShotOk()
    {
        if (!oneShot)
            return true;
        return !projectileInAir;
    }

    void doJump()
    {
        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (grounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime); // h = 1/2g * t^2
    }

    void doSwitchWeapon()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if (weapon == Weapon.PassThrough)
            {
                weapon = Weapon.Simple;
            } else
            {
                weapon = Weapon.PassThrough;
                oneShot = true;
            }
        }
    }
}
