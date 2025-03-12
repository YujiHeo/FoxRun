using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform[] line;
    [SerializeField] int curLine;
    float Speed => GameManager.instance.player.condition.Speed;
    float JumpPower => GameManager.instance.player.condition.JumpPower;

    public Animator anim;
    Rigidbody rigi;
    Collider collider;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rigi = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    // Start is called before the first frame update
    void Start()
    {
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

        anim.SetTrigger("Jump");
        rigi.AddForce(transform.up * JumpPower, ForceMode.Impulse);
    }

    public void OnSlide(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Started) return;

        anim.SetTrigger("Slide");
    }
}
