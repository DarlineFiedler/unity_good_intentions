using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float runSpeed = 10f;

    [SerializeField]
    float jumpSpeed = 5f;

    Vector2 moveInput;

    Rigidbody2D myRigidbody;

    BoxCollider2D myBoxCollider;

    [SerializeField]
    GameObject Orb;

    [SerializeField]
    GameObject BellOrb;

    [SerializeField]
    GameObject BB;

    Sprite sprite;

    Animator myAnimator;

    bool isJumping;

    GameObject myCollision;

    /* ContactFilter2D filter = new ContactFilter2D();

    Collider2D[] otherColliders = new Collider2D[16];

    int colliderPosition; */
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
        PlayerPrefs.SetInt("isTalking", 0);
        /* 
        LayerMask layerMask = (LayerMask) LayerMask.GetMask("Interactables");
        filter.SetLayerMask (layerMask);
        filter.useLayerMask = true;
        filter.useTriggers = true; */
    }

    void Update()
    {
        Run();
        FlipSprite();
        Debug.Log("player");
        Debug.Log(PlayerPrefs.GetFloat("currentHealth"));
        Debug.Log(PlayerPrefs.GetInt("isHealing"));
        Debug.Log(PlayerPrefs.GetInt("isTalking"));
        Debug.Log("player end");
        if (PlayerPrefs.GetInt("hasBell") == 1)
        {
            myAnimator.SetBool("hasBell", true);
            Orb.SetActive(true);
            BellOrb.SetActive(true);
        }
        else
        {
            Orb.SetActive(false);
            BellOrb.SetActive(false);
        }
        if (PlayerPrefs.GetInt("hasBB") == 1)
        {
            myAnimator.SetBool("hasBB", true);
        }

        //colliderPosition = myRigidbody.OverlapCollider(filter, otherColliders);
        //
    }

    void OnMove(InputValue value)
    {
        if (
            PlayerPrefs.GetFloat("currentHealth") > 0 &&
            PlayerPrefs.GetInt("isHealing") == 0 &&
            PlayerPrefs.GetInt("isTalking") == 0
        )
        {
            moveInput = value.Get<Vector2>();
        }
    }

    void OnJump(InputValue value)
    {
        if (
            PlayerPrefs.GetFloat("currentHealth") > 0 &&
            PlayerPrefs.GetInt("isHealing") == 0 &&
            PlayerPrefs.GetInt("isTalking") == 0
        )
        {
            if (!myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                return;
            }
            if (value.isPressed)
            {
                myAnimator.SetTrigger("isJumping");
                myRigidbody.velocity +=
                    new Vector2(myRigidbody.velocity.x, jumpSpeed);
            }
        }
    }

    void Run()
    {
        if (
            PlayerPrefs.GetFloat("currentHealth") > 0 &&
            PlayerPrefs.GetInt("isHealing") == 0 &&
            PlayerPrefs.GetInt("isTalking") == 0
        )
        {
            Vector2 placerVelocity =
                new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
            myRigidbody.velocity = placerVelocity;

            bool playerHasHorizontalSpeed =
                Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

            myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
        }
        else
        {
            Vector2 placerVelocity = new Vector2(0f, myRigidbody.velocity.y);
            myRigidbody.velocity = placerVelocity;
        }
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed =
            Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            PlayerPrefs
                .SetFloat("playerFacing",
                Mathf.Sign(myRigidbody.velocity.x) == 1 ? 0.2f : -0.2f);
            float turnPlayer =
                Mathf.Sign(myRigidbody.velocity.x) == 1 ? 0.2f : -0.2f;
            transform.localScale = new Vector2(turnPlayer, 0.2f);
        }
    }

    void OnInteract(InputValue value)
    {
        bool playerHasHorizontalSpeed =
            Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (
            !myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) ||
            playerHasHorizontalSpeed
        )
        {
            return;
        }
        if (
            PlayerPrefs.GetFloat("currentHealth") > 0 &&
            PlayerPrefs.GetInt("isHealing") == 0 &&
            PlayerPrefs.GetInt("isTalking") == 0
        )
        {
            /* colliderPosition = myRigidbody.OverlapCollider(filter, otherColliders);
        Debug.Log("wat is on Position ColliderPosition");
        Debug.Log(otherColliders[colliderPosition]); */
            if (
                myBoxCollider
                    .IsTouchingLayers(LayerMask.GetMask("Interactables"))
            )
            {
                if (myCollision.tag == "Entrance")
                {
                    myCollision.GetComponent<LevelEntrance>().Interacting();
                }
                if (myCollision.tag == "Mom")
                {
                    myCollision
                        .GetComponent<MomBehavior>()
                        .Interacting("player");
                }
                if (myCollision.tag == "Bro")
                {
                    myCollision.GetComponent<BroBehavior>().Interacting();
                }
                if (myCollision.tag == "Bell")
                {
                    myCollision.GetComponent<CollectingBell>().Interacting();
                }
                if (myCollision.tag == "BB")
                {
                    myCollision.GetComponent<BbBehavior>().Interacting();
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        myCollision = other.gameObject;
    }

    void OnFire(InputValue value)
    {
        if (
            PlayerPrefs.GetFloat("currentHealth") > 0 &&
            PlayerPrefs.GetInt("isHealing") == 0 &&
            PlayerPrefs.GetInt("isTalking") == 0
        )
        {
            if (PlayerPrefs.GetInt("hasBell") == 1)
            {
                Orb.GetComponent<BellOrb>().FireBullet();
            }
        }
    }

    void OnOpenMenu(InputValue value)
    {
        //SceneManager.LoadScene(0);
        Debug.Log("QUIT!");
        Application.Quit();
    }

    void OnResetBell(InputValue value)
    {
        PlayerPrefs.SetInt("hasBell", 0);
        myAnimator.SetBool("hasBell", false);
        PlayerPrefs.SetInt("everTalkToMom", 0);
        PlayerPrefs.SetInt("ignoreMom", 0);
        PlayerPrefs.SetInt("talkedToMomAfterBell", 0);
        PlayerPrefs.SetInt("everTalkToBro", 0);
        PlayerPrefs.SetInt("talkedToBroAfterBell", 0);
        PlayerPrefs.SetInt("leaveTreeOnceAfterTalkedToBro", 0);
        PlayerPrefs.SetInt("canHeal", 0);
        PlayerPrefs.SetInt("isTalking", 0);
    }

    void OnResetBB(InputValue value)
    {
        PlayerPrefs.SetInt("hasBB", 0);
        myAnimator.SetBool("hasBB", false);
    }
}
