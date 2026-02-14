using System;

using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public event EventHandler OnPlayerDeath;
    public event EventHandler OnFlashBlink;
    public static Player Instance { get; private set; }
    [Header("Player Settings")]
    [SerializeField] private float _movingSpeed = 10f;
    [SerializeField] public int maxHealthPoint = 10;
    [SerializeField] private float _damageRecoveryTime = 0.5f; 
    [SerializeField] private int _dashSpeed = 3;
    [SerializeField] private float _dashTime = 0.2f;
    [SerializeField] private TrailRenderer _dashTrailRenderer;
    [SerializeField] private float _dashCooldownTime = 0.25f;
    [Header("Regeneration Settings")]
    [SerializeField] private float _regenerationDelayTime = 3f; 
    [SerializeField] private int _regenerationAmount = 1; 
    private float _minMovingSpeed = 0.1f;
    private float _initialMovingSpeed;
    private float _timeSinceDamage = 0f; 
    private bool _isRunning;
    private bool _canTakeDamage;
    private bool _isAlive;
    private bool _isDashing;
    public int currentHealthPoint;
    Vector2 inputVector;
    private Rigidbody2D _rb;
    private KnockBack _knockBack;
    private void Awake()
    {
        Instance = this;
        _rb = GetComponent<Rigidbody2D>();
        _knockBack = GetComponent<KnockBack>();
        _initialMovingSpeed = _movingSpeed;
    }
    private void Start()
    {
        currentHealthPoint = maxHealthPoint;
        _canTakeDamage = true;
        _isAlive = true;
        GameInput.Instance.OnPlayerAttack += GameInput_OnPlayerAttack;
        GameInput.Instance.OnPlayerDash += Instance_OnPlayerDash;
    }
    private void Update()
    {
        inputVector = GameInput.Instance.GetMovementVector();
        _timeSinceDamage += Time.deltaTime; 
        RegnerationHP(); 
    }
    private void FixedUpdate()
    {
        if (_knockBack.IsGettingKnockedBack)
            return; 
        HandleMovement();
    }
    public void TakeDamage(Transform damageSource, int damage) 
    {
        if (_canTakeDamage && _isAlive)
        {
            _canTakeDamage = false;
            currentHealthPoint = Mathf.Max(0, currentHealthPoint -= damage);
            Debug.Log(currentHealthPoint);
            _knockBack.GetKnockedBack(damageSource);
            OnFlashBlink?.Invoke(this, EventArgs.Empty);
            _timeSinceDamage = 0f; 
            StartCoroutine(DamageRecoveryRoutine());
        }
        DetectDeath();
    }
    private void RegnerationHP()
    {
        if (_timeSinceDamage >= _regenerationDelayTime && currentHealthPoint < maxHealthPoint && _isAlive)
        {
            currentHealthPoint = Mathf.Min(currentHealthPoint + _regenerationAmount, maxHealthPoint);
            _timeSinceDamage = 0f; 
        }
    }
    public bool IsAlive() => _isAlive;
    private void DetectDeath()
    {
        if (currentHealthPoint == 0 && _isAlive)
        {
            _isAlive = false;
            _knockBack.StopKnockBackMovement();
            GameInput.Instance.DisableMovement();
            GameFlowManager.Instance.GameOver();
            OnPlayerDeath?.Invoke(this, EventArgs.Empty);
        }
    }
    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(_damageRecoveryTime);
        _canTakeDamage = true;
    }
    public bool isRun()
    {
        return _isRunning;
    }
    public Vector3 GetPlayerScreenPostion()
    {
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        return playerScreenPosition;
    }
    private void GameInput_OnPlayerAttack(object sender, System.EventArgs e)
    {
        ActiveWeapon.Instance.GetActiveWeapon().Attack();
    }
    private void Instance_OnPlayerDash(object sender, EventArgs e)
    {
        Dash();
    }
    private void Dash()
    {
        if (!_isDashing)
            StartCoroutine(DashRoutine());
    }
    private IEnumerator DashRoutine()
    {
        _isDashing = true;
        _movingSpeed *= _dashSpeed;
        _dashTrailRenderer.emitting = true;
        yield return new WaitForSeconds(_dashTime);
        _dashTrailRenderer.emitting = false;
        _movingSpeed = _initialMovingSpeed;
        yield return new WaitForSeconds(_dashCooldownTime);
        _isDashing = false;
    }
    private void HandleMovement()
    {
        inputVector = inputVector.normalized;
        _rb.MovePosition(_rb.position + inputVector * (_movingSpeed * Time.deltaTime));
        if (Mathf.Abs(inputVector.x) > _minMovingSpeed || Mathf.Abs(inputVector.y) > _minMovingSpeed)
        {
            _isRunning = true;
        }
        else
        {
            _isRunning = false;
        }
    }
    private void OnDestroy()
    {
        GameInput.Instance.OnPlayerAttack -= GameInput_OnPlayerAttack;
        GameInput.Instance.OnPlayerDash -= Instance_OnPlayerDash;
    }
    public void Save(ref PlayerSaveData data)
    {
        data.Position = transform.position;
    }
    public void Load(PlayerSaveData data)
    {
        transform.position = data.Position;
    }
}
[Serializable]
public struct PlayerSaveData
{
    public Vector3 Position;
}