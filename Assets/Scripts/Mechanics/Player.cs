using Platformer.Gameplay;
using Platformer.Mechanics;
using static Platformer.Core.Simulation;
using UnityEngine;

public class Player : MonoBehaviour
{
    bool isJumping;

    KinematicObject kinematicObject;
    Collider2D collider2d;

    int score = 0;
    public TMPro.TextMeshProUGUI scoreLabel;

    //speed
    [Range(0.0f, 10.0f)]
    public float speed = 1;

    void Start()
    {
        kinematicObject = GetComponent<KinematicObject>();
        collider2d = GetComponent<Collider2D>();
    }
    
    void Update()
    {
        
        //2. move code
        if (Input.GetKey(KeyCode.A))
            kinematicObject.velocity.x = -1 * speed;
        else if (Input.GetKey(KeyCode.D))
            kinematicObject.velocity.x = 1 * speed;
        else
            kinematicObject.velocity.x = 0;
        

        //4. jump code
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            kinematicObject.velocity.y = 8;
            isJumping = true;
            return;
        }

        if (kinematicObject.IsGrounded)
            isJumping = false;
    }

	private void FixedUpdate()
	{
        //1. gravity
        kinematicObject.velocity.y -= 0.2f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyController enemy = collision.collider.GetComponent<EnemyController>();

        //5. death 
        if (enemy != null)
        {
            Debug.Log("die"); //Schedule<PlayerDeath>();

            //6. enemy or me
            if (collider2d.bounds.center.y >= enemy.Bounds.max.y)
                Schedule<EnemyDeath>().enemy = enemy;
            else
                Schedule<PlayerDeath>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //7. token
        TokenInstance token = collider.GetComponent<TokenInstance>();

        if (token != null && false == token.collected)
        {
            token.Collect(GetComponent<PlayerController>());

            //8. score label
            score = score + 1;
            Debug.Log(score);
            scoreLabel.text = score.ToString();
        }
    }
}
