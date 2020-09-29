using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float attackCooldown;
    public float speed;
    private bool canAttack = true;
    private Rigidbody2D rb;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckWalking();
        CheckAttacking();
    }

    public void CheckWalking()
    {
        float h = Input.GetAxis("Horizontal");
        if (h != 0)
        {
            anim.SetBool("IsWalking", true);
            Vector2 tempVect = new Vector2(h, 0);
            tempVect = tempVect.normalized * speed * Time.deltaTime;
            rb.MovePosition(rb.position + tempVect * speed);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }
    }

    public void CheckAttacking()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            StartCoroutine(Attacking());
        }
    }

    public IEnumerator Attacking()
    {
        canAttack = false;
        anim.SetBool("IsAttacking", true);
        yield return new WaitForSeconds(attackCooldown);
        anim.SetBool("IsAttacking", false);
        canAttack = true;
    }
}
