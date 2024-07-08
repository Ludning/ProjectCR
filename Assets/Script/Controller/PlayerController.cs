using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    #region Field & Property
    //Data
    [SerializeField] private PlayerCurveData CurveData;
    [SerializeField] private PlayerControlData ControlData;
    
    //Move
    private Vector3 _velocity;
    private Vector3 _lastFixedPosition;
    private Vector3 _nextFixedPosition;
    private bool _isMove = false;
    private bool _isRun = false;
    private float _moveElapsedTime = 0f;
    private float _moveSpeedFactor;
    
    //Rotation
    private Vector2 _aimDirection;
    private Vector2 _moveDirection;
    private Quaternion _lastFixedRotation;
    private Quaternion _nextFixedRotation;
    private bool _isAim = false;
    
    //Component Reference
    [SerializeField] private GroundChecker GroundChecker;
    [SerializeField] private Camera MainCamera;
    [SerializeField] private CharacterController Controller;
    [SerializeField] private Animator Animator;
    [SerializeField] private Player Player;
    //[SerializeField] private Weapon.Weapon Weapon;
    
    //Animation Move Hash
    private readonly int Vertical = Animator.StringToHash("Vertical");
    private readonly int Horizontal = Animator.StringToHash("Horizontal");
    private readonly int IsMove = Animator.StringToHash("IsMove");
    #endregion

    #region MonoBehavior Function
    void Start()
    {
        _lastFixedPosition = transform.position;
        _lastFixedRotation = transform.rotation;
        _nextFixedPosition = transform.position;
        _nextFixedRotation = transform.rotation;
    }
    private void Update()
    {
        float interpolationAlpha = (Time.time - Time.fixedTime) / Time.fixedDeltaTime;
        Controller.Move(Vector3.Lerp(_lastFixedPosition, _nextFixedPosition, interpolationAlpha) - transform.position);
        transform.rotation = Quaternion.Slerp(_lastFixedRotation, _nextFixedRotation, interpolationAlpha);
    }
    private void FixedUpdate()
    {
        //이전 방향, 회전 초기화
        _lastFixedPosition = transform.position;
        _lastFixedRotation = transform.rotation;
        
        //Curve를 통해 값을 업데이트
        float fixedDeltaTime = Time.fixedDeltaTime;
        OnFixedUpdateSpeedFactor(fixedDeltaTime);

        _velocity = GetVelocity();
        
        //현재 프레임 방향, 회전 초기화
        _nextFixedPosition = transform.position + _velocity * (_moveSpeedFactor * ControlData.MoveSpeed * fixedDeltaTime);
        _nextFixedRotation = GetPlayerQuaternion();

        SetAnimation();
    }
    
    private void OnEnable()
    {
        InputHandler.Instance.RegisterInputHandler(InputActionType.Move, OnMove);
        InputHandler.Instance.RegisterInputHandler(InputActionType.Run, OnRun);
        InputHandler.Instance.RegisterInputHandler(InputActionType.Attack, OnAttack);
        InputHandler.Instance.RegisterInputHandler(InputActionType.Jump, OnJump);
        InputHandler.Instance.RegisterInputHandler(InputActionType.Util, OnUtil);
        InputHandler.Instance.RegisterInputHandler(InputActionType.Interaction, OnInteraction);
        InputHandler.Instance.RegisterInputHandler(InputActionType.Skill, OnSkill);
        InputHandler.Instance.RegisterInputHandler(InputActionType.Special, OnSpecial);
        InputHandler.Instance.RegisterInputHandler(InputActionType.ChangeWeapon, OnChangeWeapon);
    }
    private void OnDisable()
    {
        InputHandler.Instance.UnregisterInputHandler(InputActionType.Move, OnMove);
        InputHandler.Instance.UnregisterInputHandler(InputActionType.Run, OnRun);
        InputHandler.Instance.UnregisterInputHandler(InputActionType.Attack, OnAttack);
        InputHandler.Instance.UnregisterInputHandler(InputActionType.Jump, OnJump);
        InputHandler.Instance.UnregisterInputHandler(InputActionType.Util, OnUtil);
        InputHandler.Instance.UnregisterInputHandler(InputActionType.Interaction, OnInteraction);
        InputHandler.Instance.UnregisterInputHandler(InputActionType.Skill, OnSkill);
        InputHandler.Instance.UnregisterInputHandler(InputActionType.Special, OnSpecial);
        InputHandler.Instance.UnregisterInputHandler(InputActionType.ChangeWeapon, OnChangeWeapon);
    }
    #endregion

    #region Function

    #region Character Rotation
    //캐릭터의 회전 반환
    private Quaternion GetPlayerQuaternion()
    {
        if (_isAim == true)
        {
            Vector2 direction = GetAimDirection();
            return Quaternion.LookRotation(new Vector3(direction.x, 0, direction.y));
        }

        return (_moveDirection != Vector2.zero)
            ? Quaternion.LookRotation(new Vector3(_moveDirection.x, 0, _moveDirection.y))
            : transform.rotation;
    }
    private Vector2 GetAimDirection()
    {
        if(MainCamera == null)
            MainCamera = Camera.main;
        // 마우스 위치를 기준으로 레이 생성
        Ray ray = MainCamera.ScreenPointToRay(GetMousePosition());
            
        // 캐릭터의 높이 가져오기
        float characterHeight = this.transform.position.y;
            
        // 평면 방정식을 통한 교차점 계산
        if (CalculateIntersectionPoint(ray, characterHeight, out Vector3 hitPoint))
        {
            Vector3 aimDirection = hitPoint - this.transform.position;
            return new Vector2(aimDirection.x, aimDirection.z);
        }

        return Vector2.zero;
    }
    // 평면 방정식을 통한 교차점 계산 함수
    private bool CalculateIntersectionPoint(Ray ray, float height, out Vector3 hitPoint)
    {
        // 캐릭터 높이와 레이의 교차점을 수학적으로 계산
        if (ray.direction.y != 0)
        {
            // 캐릭터 높이(y)와 레이의 기울기(d)로 교차점 z 위치 계산
            float distance = (height - ray.origin.y) / ray.direction.y;
            hitPoint = ray.origin + ray.direction * distance;
            return true;
        }
        hitPoint = Vector3.zero;
        return false;
    }
    private Vector2 GetMousePosition()
    {
        return Mouse.current.position.ReadValue();
    }
    #endregion
    
    #region Calculate Velocity
    //Velocity 계산
    private Vector3 GetVelocity()
    {
        float velocityY = CalculateVelocityY();
        Vector2 velocityXZ = CalculateVelocityXZ();
        
        return new Vector3(velocityXZ.x, velocityY, velocityXZ.y);
    }
    private Vector2 CalculateVelocityXZ()
    {
        float velocityX = _moveDirection.x;
        float velocityZ = _moveDirection.y;
        if (_isRun)
        {
            velocityX *= ControlData.RunMultiply;
            velocityZ *= ControlData.RunMultiply;
        }
        return new Vector2(velocityX, velocityZ);
    }
    private float CalculateVelocityY()
    {
        return (!GroundChecker.IsGrounded() || _velocity.y > 0.1f)
            ? _velocity.y - ControlData.GravityValue * Time.fixedDeltaTime
            : 0f;
    }
    #endregion
    
    #region Move Curve Function
    private void OnFixedUpdateSpeedFactor(float fixedDeltaTime)
    {
        float ratio = GetMoveCurveRatio(fixedDeltaTime);
        _moveSpeedFactor = GetMoveCurveValue(ratio);
    }
    private float GetMoveCurveRatio(float fixedDeltaTime)
    {
        _moveElapsedTime = _isMove ? _moveElapsedTime + fixedDeltaTime : 0f;
        return Mathf.Clamp01(_moveElapsedTime / ControlData.CurveMaxTime);
    }
    private float GetMoveCurveValue(float ratio)
    {
        return CurveData.MoveCurve.Evaluate(ratio);
    }
    #endregion
    
    private void SetAnimation()
    {
        if(Animator!= null)
        {
            Vector3 localDirection =
                transform.InverseTransformDirection(new Vector3(_moveDirection.x, 0, _moveDirection.y));

            if (_isRun == true)
                localDirection *= 3;//ControlData.RunMultiply;
            
            Animator.SetFloat(Vertical, localDirection.z);
            Animator.SetFloat(Horizontal, localDirection.x);
        }
    }
    
    #endregion


    #region InputAction Function
    public void OnMove(InputAction.CallbackContext context)
    {
        _moveDirection = context.ReadValue<Vector2>();
        if (context.started)
        {
            _isMove = true;
            Animator.SetBool(IsMove, true);
        }
        else if(context.performed)
        {
            
        }
        else if(context.canceled)
        {
            _isMove = false;
            Animator.SetBool(IsMove, false);
        }
    }
    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _isRun = true;
        }
        else if(context.performed)
        {
            
        }
        else if(context.canceled)
        {
            _isRun = false;
        }
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (GroundChecker.IsGrounded())
            {
                _velocity.y = ControlData.JumpForce;
            }
        }
    }
    public void OnUtil(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _isAim = true;
        }
        else if(context.performed)
        {
            
        }
        else if(context.canceled)
        {
            _isAim = false;
        }
    }
    public void OnInteraction(InputAction.CallbackContext context)
    {
        
    }
    public void OnSkill(InputAction.CallbackContext context)
    {
        
    }
    public void OnSpecial(InputAction.CallbackContext context)
    {
        
    }
    
    public void OnChangeWeapon(InputAction.CallbackContext context)
    {
        // 키가 눌렸을 때만 처리
        if (context.phase == InputActionPhase.Performed)
        {
            // 누른 키의 이름을 가져옵니다.
            string keyName = context.control.name;

            if (keyName == "1")
            {
                PlayerManager.Instance.SwapWeapon(WeaponIndexType.Primary);
            }
            else if (keyName == "2")
            {
                PlayerManager.Instance.SwapWeapon(WeaponIndexType.Secondary);
            }
        }
    }
    #endregion
    
}
