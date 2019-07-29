using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody rb;
    protected PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
    }

    public void Idle()
    {
        if (PV.IsMine)
        {
            anim.SetFloat("MovementSpeed", 0f);
        }
    }

    public void Walk()
    {
        if (PV.IsMine)
        {
            anim.SetFloat("MovementSpeed", 1f);
        }
        
    }

    public void Run()
    {
        if (PV.IsMine)
        {
            anim.SetFloat("MovementSpeed", 2f);
        }
        
    }

    public void Jump()
    {
        if (PV.IsMine)
        {
            anim.SetFloat("MovementSpeed", 0f);
            anim.SetTrigger("Jump");
        }
    }

    public void Interact()
    {
        if (PV.IsMine)
        {
            anim.SetFloat("MovementSpeed", 0f);
        }
    }

    private void Update()
    {
        if (PV.IsMine)
        {
            anim.SetFloat("MovementSpeed", rb.velocity.magnitude);
        }
    }

}
