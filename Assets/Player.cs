using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public TMP_Text stepsText;
    public AudioSource audioSource;
    [Range(0.01f, 1f)] public float jumpHeight = 0.5f;
    [Range(0.01f, 1f)] public float moveDuration = 0.2f;

    Vector3 targetPos;
    Sequence moveSeq;

    Vector3 moveDirection;

    private Collider playerCollider;
    private float backBoundary;
    private float leftBoundary;
    private float rightBoundary;

    private int maxTravel;
    public int MaxTravel { get => maxTravel; }
    private int currentTravel;
    public int CurrentTravel { get => currentTravel; }

    public bool isDead = false;

    private void Start()
    {
        playerCollider = GetComponent<Collider>();
    }

    public void SetUp(int minZPos, int extent)
    {
        backBoundary = minZPos - 1;
        leftBoundary = -(extent + 1);
        rightBoundary = extent + 1;
    }

    void Update()
    {
        moveDirection = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.W))
        {
            moveDirection += new Vector3(0, 0, 1);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            moveDirection += new Vector3(-1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            moveDirection += new Vector3(0, 0, -1);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            moveDirection += new Vector3(1, 0, 0);
        }

        if (moveDirection == Vector3.zero) return;

        if (!IsJumping())
        {
            Jump(moveDirection);
        }
    }

    void Jump(Vector3 targetDir)
    {
        targetPos = transform.position + targetDir;

        transform.LookAt(targetPos);
        targetPos.y = 0;

        moveSeq = DOTween.Sequence(targetPos);
        moveSeq.Append(transform.DOMoveY(jumpHeight, moveDuration / 2));
        moveSeq.Append(transform.DOMoveY(0, moveDuration / 2));

        if (targetPos.z <= backBoundary || targetPos.x <= leftBoundary || targetPos.x >= rightBoundary) return;
        if (Tree.TreePositions.Contains(targetPos)) return;

        transform.DOMoveX(targetPos.x, moveDuration);
        transform.DOMoveZ(targetPos.z, moveDuration).OnComplete(UpdateTravel);
    }

    bool IsJumping()
    {
        return DOTween.IsTweening(transform);
    }

    void AnimateCrash()
    {
        transform.DOScaleY(0.1f * transform.localScale.y, 0.2f);
        transform.DOScaleX(3.0f * transform.localScale.x, 0.2f);
        transform.DOScaleZ(2.0f * transform.localScale.z, 0.2f);
        enabled = false;
    }

    void UpdateTravel()
    {
        currentTravel = (int) transform.position.z;
        if(currentTravel > maxTravel)
            maxTravel = currentTravel;

        stepsText.text = "STEP: " + maxTravel.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            AnimateCrash();
            playerCollider.enabled = false;
            isDead = true;

            audioSource.Play();
        }

    }
}
