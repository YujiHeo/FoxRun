using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform[] line;
    [SerializeField] int curLine;
    public Func<float> speed;
    float Speed => speed();
    public Func<float> jumpPower;
    float JumpPower => jumpPower();

    public Func<PSTAT> getStat;
    public Action<PSTAT> changeStat;
    PSTAT stat { get => getStat(); set => changeStat(value); }

    public Animator anim;
    Rigidbody rigi;
    BoxCollider hitBox;
    Vector3 originCenter, originSize;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rigi = GetComponent<Rigidbody>();
        hitBox = GetComponent<BoxCollider>();
        originCenter = hitBox.center;
        originSize = hitBox.size;
        curLine = line.Length / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(line[curLine].position, transform.position) < 0.1f)
            transform.position = line[curLine].position;
    }

    void FixedUpdate()
    {
        Vector3 dir = line[curLine].position - transform.position;
        dir = dir.normalized * Speed;
        dir.y = rigi.velocity.y;
        dir.z = rigi.velocity.z;

        rigi.velocity = dir;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Started) return;
        Vector2 dir = context.ReadValue<Vector2>();
        int nextLine = curLine + (int)dir.x;

        curLine = nextLine >= 0 && nextLine < line.Length ? nextLine : curLine;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Started) return;
        if (stat == PSTAT.JUMP || stat == PSTAT.DEAD) return;

        stat = PSTAT.JUMP;

        hitBox.center = originCenter + Vector3.up * JumpPower;
        hitBox.size = originSize - Vector3.up * originSize.y * 0.5f;
        anim.SetTrigger("Jump");
    }

    public void OnSlide(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Started || stat != PSTAT.RUN) return;

        stat = PSTAT.SLIDE;

        hitBox.center = originCenter - Vector3.up * originCenter.y * 0.5f;
        hitBox.size = originSize - Vector3.up * originSize.y * 0.5f;
        anim.SetTrigger("Slide");
    }

    public void ReturnCollider()
    {
        stat = PSTAT.RUN;

        hitBox.center = originCenter;
        hitBox.size = originSize;
    }
}
