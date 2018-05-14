using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]

public class Actor : MonoBehaviour {

    [SerializeField] private float speed;
    protected Vector2 Axis;

    //Components
    private Rigidbody rigid;
    private Collider coll;
    private Animator anim;
    private AudioSource aud;

    // Use this for initialization
    protected virtual void Awake () {
        rigid = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        anim = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
        aud.loop = true;
	}

    protected virtual void Start()
    {
        Axis = Vector3.zero;
    }

    // Update is called once per frame
    void FixedUpdate () {
        Move(Axis);
        Turn(Axis);
	}
    
    /////////////////////////////////////////////////////FUNCIONES/////////////////////////////////////////////////

    protected virtual void Move(Vector2 Axis)
    {
        rigid.velocity = new Vector3(Axis.x * speed, rigid.velocity.y, Axis.y * speed);
    }

    protected virtual void Turn(Vector2 Axis)
    {
        if (Axis != Vector2.zero) transform.rotation = Quaternion.LookRotation(new Vector3(Axis.x, 0f, Axis.y));
    }

    ///////////////////////////////////////////////////////GET SET/////////////////////////////////////////////////

    protected Rigidbody Rigid
    {
        get
        {
            return rigid;
        }
    }

    protected Collider Coll
    {
        get
        {
            return coll;
        }
    }

    protected Animator Anim
    {
        get
        {
            return anim;
        }
    }

    protected AudioSource Aud
    {
        get
        {
            return aud;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }
}
