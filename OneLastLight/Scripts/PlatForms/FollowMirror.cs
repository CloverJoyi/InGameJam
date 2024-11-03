using Unity.Mathematics;
using UnityEngine;

public class FollowMirror : MonoBehaviour {
    public Rigidbody2D _goal;


    private float speed;
    private Rigidbody2D _rb;
    private float blockPosition;
    private int _canMove; //1:可随意移动，2：右边撞墙，3：左边撞墙


    // Start is called before the first frame update
    void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _canMove = 1;
    }

    // Update is called once per frame
    void Update() {
        speed = math.abs(_goal.velocity.x);
        //Debug.Log(_canMove);

        switch (_canMove) {
            case 1:
                if (GetDir() > 0.2) {
                    _rb.velocity = new Vector2(-speed, 0);
                }
                else if (GetDir() < -0.2) {
                    _rb.velocity = new Vector2(speed, 0);
                }
                else {
                    _rb.velocity = Vector2.zero;
                }

                break;
            case 2:
                if (GetDir() > 0.2) {
                    _rb.velocity = new Vector2(-speed, 0);
                }
                else {
                    _rb.velocity = Vector2.zero;
                }

                break;
            case 3:
                if (GetDir() < -0.2) {
                    _rb.velocity = new Vector2(speed, 0);
                }
                else {
                    _rb.velocity = Vector2.zero;
                }

                break;
            default:
                _rb.velocity = Vector2.zero;
                break;
        }
    }

    private float GetDir() {
        float dir;
        dir = _rb.position.x - _goal.position.x;
        return dir;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other == null) {
            _canMove = 1;
        }

        if (other.gameObject.CompareTag("Block")) {
            _rb.velocity = Vector2.zero;
            blockPosition = other.transform.position.x - _rb.position.x;
            if (blockPosition > 0.1) {
                _canMove = 2;
            }
            else {
                _canMove = 3;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        _canMove = 1;
    }
}