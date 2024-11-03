using System;
using UnityEngine;

namespace TarodevController {
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class PlayerController : MonoBehaviour, IPlayerController {
        [SerializeField] private ScriptableStats _stats;

        private bool _cachedQueryStartInColliders;

        private CapsuleCollider2D _col;
        private FrameInput _frameInput;

        private Vector2 _frameVelocity;
        private Rigidbody2D _rb;
        public LightStateBase _lightBeamState;
        public LightStateBase _lightCircleState;
        public LightStateController _lightStateController;
        public LightStateBase _noLightState;


        private float _time;


        private void Awake() {
            _rb = GetComponent<Rigidbody2D>();
            _col = GetComponent<CapsuleCollider2D>();

            _cachedQueryStartInColliders = Physics2D.queriesStartInColliders;

            canFly = false;
            canClimb = false;

            _lightBeamState = new LightBeamState(this, _rb, this);
            _lightCircleState = new LightCircleState(this, _rb, this);
            _noLightState = new NoLightState(this, _rb, this);
            _lightStateController = new LightStateController(this);
            Init();
        }

        private void Update() {
            _time += Time.deltaTime;
            GatherInput();

            _lightStateController.Update();

            SaveData.Instance._playerPosition = _rb.position;
        }

        private void FixedUpdate() {
            CheckCollisions();

            HandleJump();
            HandleDirection();

            if (_flyAct && canFly) {
                FlyAct();
            }
            else if (canClimb) {
                ClimbAct();
                //Debug.Log("1");
            }
            else {
                //Debug.Log("2");
                HandleGravity();
            }


            ApplyMovement();
        }


#if UNITY_EDITOR
        private void OnValidate() {
            if (_stats == null)
                Debug.LogWarning("Please assign a ScriptableStats asset to the Player Controller's Stats slot", this);
        }
#endif

        //输入
        private void GatherInput() {
            _frameInput = new FrameInput {
                JumpDown = Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.C),
                JumpHeld = Input.GetButton("Jump") || Input.GetKey(KeyCode.C),
                Move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")),
                Fly = Input.GetKeyDown(KeyCode.LeftShift),
            };
            if (_stats.SnapInput) {
                _frameInput.Move.x = Mathf.Abs(_frameInput.Move.x) < _stats.HorizontalDeadZoneThreshold
                    ? 0
                    : Mathf.Sign(_frameInput.Move.x);
                _frameInput.Move.y = Mathf.Abs(_frameInput.Move.y) < _stats.VerticalDeadZoneThreshold
                    ? 0
                    : Mathf.Sign(_frameInput.Move.y);
            }

            if (_frameInput.JumpDown) {
                _jumpToConsume = true;
                _timeJumpWasPressed = _time;
            }

            if (_frameInput.Fly) {
                if (_flyAct) {
                    _flyAct = false;
                }
                else if (!_flyAct) {
                    if (canFly) {
                        _flyAct = true;
                    }
                }
            }

            // if (Input.GetKeyDown(KeyCode.N)) {
            //     Save();
            // }
            //
            // if (Input.GetKeyDown(KeyCode.M)) {
            //     Load();
            // }
        }

        #region Gravity

        private void HandleGravity() {
            if (_grounded && _frameVelocity.y <= 0f) {
                _frameVelocity.y = _stats.GroundingForce;
            }
            else {
                var inAirGravity = _stats.FallAcceleration;
                if (_endedJumpEarly && _frameVelocity.y > 0) inAirGravity *= _stats.JumpEndEarlyGravityModifier;
                _frameVelocity.y = Mathf.MoveTowards(_frameVelocity.y, -_stats.MaxFallSpeed,
                    inAirGravity * Time.fixedDeltaTime);
            }
        }

        #endregion

        private void ApplyMovement() {
            _rb.velocity = _frameVelocity;
        }

        #region Horizontal

        private void HandleDirection() {
            if (_frameInput.Move.x == 0) {
                var deceleration = _grounded ? _stats.GroundDeceleration : _stats.AirDeceleration;
                _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, 0, deceleration * Time.fixedDeltaTime);
            }
            else {
                _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, _frameInput.Move.x * _stats.MaxSpeed,
                    _stats.Acceleration * Time.fixedDeltaTime);
            }
        }

        #endregion

        #region Init

        private void Init() {
            _lightStateController.Init();
            _lightBeamState.SetLightStateController(_lightStateController);
            _lightCircleState.SetLightStateController(_lightStateController);
            _noLightState.SetLightStateController(_lightStateController);
        }

        #endregion

        // public void ThrowLantern(Transform m_playerBody) {
        //     var _lantern = (GameObject)Instantiate(Resources.Load("Lantern(External)"), m_playerBody.transform.position,
        //         m_playerBody.transform.rotation);
        //     OutPutState(_lantern);
        // }

        public void MyDestroy(GameObject gameObject) {
            Destroy(gameObject);
        }

        public void GetBlueLight() {
            SaveData.Instance._haveBlue = true;
            SaveData.Instance.Save();
        }

        // private void OutPutState(GameObject lantern) {
        //     lantern.GetComponent<LanternController>().GetState(m_stateSave);
        // }


        #region Interface

        public Vector2 FrameInput => _frameInput.Move;
        public event Action<bool, float> GroundedChanged;
        public event Action Jumped;

        #endregion

        #region Collisions

        private float _frameLeftGrounded = float.MinValue;
        public bool _grounded;

        private void CheckCollisions() {
            Physics2D.queriesStartInColliders = false;

            // Ground and Ceiling(地面和天花板)  灯笼
            SaveData.Instance.groundHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0,
                Vector2.down,
                _stats.GrounderDistance, _stats.GroundLayer);
            bool ceilingHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.up,
                _stats.GrounderDistance, _stats.GroundLayer);

            // Hit a Ceiling
            if (ceilingHit) _frameVelocity.y = Mathf.Min(0, _frameVelocity.y);

            // Landed on the Ground
            if (!_grounded && SaveData.Instance.groundHit) {
                _grounded = true;
                _coyoteUsable = true;
                _bufferedJumpUsable = true;
                _endedJumpEarly = false;
                GroundedChanged?.Invoke(true, Mathf.Abs(_frameVelocity.y));
            }
            // Left the Ground
            else if (_grounded && !SaveData.Instance.groundHit) {
                _grounded = false;
                _frameLeftGrounded = _time;
                GroundedChanged?.Invoke(false, 0);
            }


            Physics2D.queriesStartInColliders = _cachedQueryStartInColliders;
        }

        #endregion


        #region Jumping

        private bool _jumpToConsume;
        private bool _bufferedJumpUsable;
        private bool _endedJumpEarly;
        private bool _coyoteUsable;
        private float _timeJumpWasPressed;

        private bool HasBufferedJump => _bufferedJumpUsable && _time < _timeJumpWasPressed + _stats.JumpBuffer; //缓冲跳跃

        private bool CanUseCoyote =>
            _coyoteUsable && !_grounded && _time < _frameLeftGrounded + _stats.CoyoteTime; //土狼时间

        private void HandleJump() {
            if (!_endedJumpEarly && !_grounded && !_frameInput.JumpHeld && _rb.velocity.y > 0) _endedJumpEarly = true;

            if (!_jumpToConsume && !HasBufferedJump) return;

            if (_grounded || CanUseCoyote) ExecuteJump();

            _jumpToConsume = false;
        }

        private void ExecuteJump() {
            _endedJumpEarly = false;
            _timeJumpWasPressed = 0;
            _bufferedJumpUsable = false;
            _coyoteUsable = false;
            _frameVelocity.y = _stats.JumpPower;
            Jumped?.Invoke();
        }

        #endregion

        #region Fly

        private bool _flyAct;
        private bool canFly;


        private void FlyAct() {
            _frameVelocity.y += _stats.buoyancy * Time.deltaTime;
        }

        public void GetCanFly(bool canFly) {
            this.canFly = canFly;
        }

        #endregion

        #region Climb

        private bool canClimb;

        private void ClimbAct() {
            if (_frameInput.Move.y != 0) {
                _rb.position = _rb.position + Vector2.up * _frameInput.Move.y * _stats.climbSpeed * Time.deltaTime;
            }

            //else {
            _frameVelocity.y = 0;
            //}
        }

        public void GetClimb(bool canClimb) {
            this.canClimb = canClimb;
        }

        #endregion

        #region Save

        // [System.Serializable]
        // class SaveData {
        //     public Vector3 playerPosition;
        //     public bool haveLight;
        //     public bool haveBlue;
        //     public StateSave stateSave;
        // }

        // private const string PLAYER_DATA_KEY = "PlayerData";
        //private const string PLAYER_DATA_FILE_NAME = "PlayerData.sav";

        // void SaveByPlayerPrefs() {
        //     SaveSystem.SaveByPlayerPrefs(PLAYER_DATA_KEY, SavingData());
        // }
        //
        //
        // void LoadFromPlayerPrefs() {
        //     var json = SaveSystem.LoadFromPlayerPrefs(PLAYER_DATA_KEY);
        //     var saveData = JsonUtility.FromJson<SaveData>(json);
        //     LoadData(saveData);
        // }

        //Json存档
        // void SaveByJson() {
        //     SaveSystem.SaveByJson(PLAYER_DATA_FILE_NAME, SavingData());
        // }
        //
        // //Json读档
        // void LoadFromJson() {
        //     var saveData = SaveSystem.LoadFromJson<SaveData>(PLAYER_DATA_FILE_NAME);
        //     LoadData(saveData);
        //     LoadPlayerState(saveData);
        // }
        //
        // void LoadData(SaveData saveData) {
        //     if (saveData != null) {
        //         transform.position = saveData.playerPosition;
        //         _haveLight = saveData.haveLight;
        //         _haveBlue = saveData.haveBlue;
        //         m_stateSave = saveData.stateSave;
        //     }
        // }
        //
        // SaveData SavingData() {
        //     var saveData = new SaveData();
        //
        //     saveData.playerPosition = transform.position;
        //     saveData.haveLight = _haveLight;
        //     saveData.haveBlue = _haveBlue;
        //     saveData.stateSave = m_stateSave;
        //     return saveData;
        // }
        //

        void LoadPlayerState(SaveData saveData) {
            if (saveData._haveLight) {
                if (saveData.m_stateSave.lightMode) {
                    SaveData.Instance.m_stateSave._color = saveData.m_stateSave._color;
                    _lightStateController.TransitionState(StateType.LightBeamState);
                }
                else {
                    SaveData.Instance.m_stateSave._color = saveData.m_stateSave._color;
                    _lightStateController.TransitionState(StateType.LightCircleState);
                }
            }
            else {
                SaveData.Instance.m_stateSave._color = saveData.m_stateSave._color;
                _lightStateController.TransitionState(StateType.NoLantern);
            }
        }

        //
        //
        public void Save() {
            SaveData.Instance.Save();
        }


        public void Load() {
            SaveData.Instance.Load();
            LoadPlayerState(SaveData.Instance);
            // GameObject.Find("Player").transform.position= SaveData.Instance._playerPosition;
            // transform.position = SaveData.Instance._playerPosition;
        }

        // private void DataInit() {
        //     _haveLight = SaveData.instance._haveLight;
        //     _haveBlue = SaveData.instance._haveBlue;
        //     m_stateSave = SaveData.instance.m_stateSave;
        //     transform.position = SaveData.instance._playerPosition;
        // }
        //
        // private void DataSave() {
        // }
        //
        // // [UnityEditor.MenuItem("Developer/Delete Player Data Save File")]
        // // public static void DeletePlayerDataPrefs() {
        // //     SaveSystem.DeleteSave(PLAYER_DATA_KEY);
        // // }
        //
        // [UnityEditor.MenuItem("Developer/Delete Player Data Save File")]
        // public static void DeletePlayerDataSaveFile() {
        //     SaveSystem.DeleteSave(PLAYER_DATA_FILE_NAME);
        // }
        //

        #endregion
    }


    public struct FrameInput {
        public bool JumpDown;
        public bool JumpHeld;
        public bool Fly;
        public Vector2 Move;
    }

    public interface IPlayerController {
        public Vector2 FrameInput { get; }
        public event Action<bool, float> GroundedChanged;

        public event Action Jumped;
    }
}