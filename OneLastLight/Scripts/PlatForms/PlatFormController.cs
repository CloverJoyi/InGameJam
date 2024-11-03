using UnityEngine;
using UnityEngine.Serialization;

public class PlatFormController : MonoBehaviour {
    public float canSeeTime = 0.1f;
    public bool NotSeen; //false: 墙模式, true: 平台模式
    private bool _canSee;
    private bool flip;
    private float timer;
    private Transform _entity;
    //private Transform playerParent;
    public SpriteRenderer sp;
    public Sprite[] Sprites;
    public bool bronAppear;
    public Collider2D _collider2D;
    // Start is called before the first frame update
    private void Awake()
    {
        //playerParent = GameObject.Find("Player").transform.parent;

        _entity = transform.Find("Entity");
    }
    private void Start() {
        
        if (NotSeen) {
            sp.color=Color.clear;
            _collider2D.isTrigger = true;
            _collider2D.gameObject.layer = LayerMask.NameToLayer("Default");
        }

        if (bronAppear)
        {
            appear();
        }
        else
        {
            disappear();
        }
    }

    // Update is called once per frame
    private void Update()
    {

        timer += Time.deltaTime;
        if (timer > canSeeTime)
        {
            flip = false;
        }

        if (flip)
        {
            if (bronAppear)
            {
                disappear();
            }
            else
            {
                appear();
            }
            
        }
        else
        {
            if (bronAppear)
            {
                appear();
            }
            else
            {
                disappear();
            }
        }
        
    }

    public void BoxAppear()
    {
        if (NotSeen)
        {
            sp.color = Color.white;
            NotSeen = false;
        }
        flip = true;
        if(!bronAppear)
            timer = 0f;
    }


    public void BoxDisAppear() 
    {
        if (NotSeen)
        {
            sp.color = Color.white;
            NotSeen = false;
        }
        flip = true;
        if(bronAppear)
            timer = 0f;
    }

    void disappear()
    {
        sp.sprite = Sprites[0];
        _collider2D.isTrigger = true;
        _collider2D.gameObject.layer = LayerMask.NameToLayer("Default");
    }
    void appear()
    {
        sp.sprite = Sprites[1];
        _collider2D.isTrigger = false;
        _collider2D.gameObject.layer = LayerMask.NameToLayer("Ground");
    }
}