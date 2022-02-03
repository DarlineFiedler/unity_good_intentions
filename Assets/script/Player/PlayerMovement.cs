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
    GameObject InfoBox;

    [SerializeField]
    GameObject Orb;

    [SerializeField]
    GameObject BellOrb;

    Animator myAnimator;

    GameObject myCollision;

    AudioSource audioSrc;

    /* ContactFilter2D filter = new ContactFilter2D();

    Collider2D[] otherColliders = new Collider2D[16];

    int colliderPosition; */
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
        audioSrc = GetComponent<AudioSource>();
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
            PlayerPrefs.GetInt("isTalking") == 0 &&
            PlayerPrefs.GetInt("save") == 0
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
            PlayerPrefs.GetInt("isTalking") == 0 &&
            PlayerPrefs.GetInt("save") == 0
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
            PlayerPrefs.GetInt("isTalking") == 0 &&
            PlayerPrefs.GetInt("save") == 0
        )
        {
            Vector2 placerVelocity =
                new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
            myRigidbody.velocity = placerVelocity;

            bool playerHasHorizontalSpeed =
                Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

            myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
            if (playerHasHorizontalSpeed)
            {
                if (!audioSrc.isPlaying)
                {
                    audioSrc.Play();
                }
            }
            else
            {
                audioSrc.Stop();
            }
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
                Mathf.Sign(myRigidbody.velocity.x) == 1 ? 1f : -1f);
            float turnPlayer =
                Mathf.Sign(myRigidbody.velocity.x) == 1 ? 1f : -1f;
            transform.localScale = new Vector2(turnPlayer, 1f);
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
                if (myCollision.tag == "MomWorry")
                {
                    myCollision.GetComponent<MomWorryBehavior>().Interacting();
                }
                if (myCollision.tag == "Bro")
                {
                    myCollision.GetComponent<BroBehavior>().Interacting();
                }
                if (myCollision.tag == "BroWorry")
                {
                    myCollision.GetComponent<BroWorryBehavior>().Interacting();
                }
                if (myCollision.tag == "Bell")
                {
                    myCollision.GetComponent<CollectingBell>().Interacting();
                }
                if (myCollision.tag == "BB")
                {
                    myCollision.GetComponent<BbBehavior>().Interacting();
                }
                if (myCollision.tag == "Robbenraupe")
                {
                    myCollision.GetComponent<RobbenBehavior>().Interacting();
                }
                if (myCollision.tag == "Shroom")
                {
                    myCollision.GetComponent<showShroomText>().Interacting();
                }
                if (myCollision.tag == "Wallblock")
                {
                    myCollision.GetComponent<OpenWallblock>().Interacting();
                }
                if (myCollision.tag == "Save")
                {
                    myCollision.GetComponent<SaveGame>().Interacting();
                }
                if (myCollision.tag == "FirstPower")
                {
                    myCollision
                        .GetComponent<CollectingFirstPower>()
                        .Interacting();

                    InfoBox.SetActive(false);
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
            PlayerPrefs.GetInt("isTalking") == 0 &&
            PlayerPrefs.GetInt("save") == 0
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
        //SceneManager.LoadSceneAsync(0);
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
        PlayerPrefs.SetInt("everTalkedToRobben", 0);
        PlayerPrefs.SetFloat("Healing", 2f);
        PlayerPrefs.SetInt("safePoint", 1);
        PlayerPrefs.SetInt("isRankWallOpen", 0);
        PlayerPrefs.SetInt("ForestSpiritIsDead", 0);
        PlayerPrefs.SetInt("Shroom1", 0);
        PlayerPrefs.SetInt("Shroom2", 0);
        PlayerPrefs.SetInt("Shroom3", 0);
        PlayerPrefs.SetInt("Shroom4", 0);
        PlayerPrefs.SetInt("Spike2", 0);
        PlayerPrefs.SetInt("hasFirstPower", 0);
        PlayerPrefs.SetInt("momHasBB", 0);
        PlayerPrefs.SetInt("talkedToBroAfterBoss", 0);
    }

    void OnResetBB(InputValue value)
    {
        PlayerPrefs.SetInt("hasBB", 0);
        myAnimator.SetBool("hasBB", false);
    }
}
