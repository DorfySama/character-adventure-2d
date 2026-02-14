using UnityEngine;
public class FlashBlink : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _damagableObject;
    [SerializeField] private Material _blinkMaterial;
    [SerializeField] private float _blinkDuration = 0.1f;
    private float _currentBlinkTimer;
    private Material _defaultMaterial;
    private SpriteRenderer _spriteRenderer;
    private bool _isBlinking;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultMaterial = _spriteRenderer.material;
        _isBlinking = true;
    }
    private void Start()
    {
        if (_damagableObject is Player) 
        {
            (_damagableObject as Player).OnFlashBlink += FlashBlink_OnFlashBlink;
        }
    }
    public void StopBlinking()
    {
        SetDefaultMaterial();
        _isBlinking = false;
    }
    private void FlashBlink_OnFlashBlink(object sender, System.EventArgs e)
    {
        SetBlinkingMaterial();
    }
    private void Update()
    {
        if (_isBlinking)
        {
            _currentBlinkTimer -= Time.deltaTime; 
            if (_currentBlinkTimer < 0f) 
            {
                SetDefaultMaterial();
            }
        }
    }
    private void SetBlinkingMaterial()
    {
        _currentBlinkTimer = _blinkDuration;
        _spriteRenderer.material = _blinkMaterial;
    }
    private void SetDefaultMaterial()
    {
        _spriteRenderer.material = _defaultMaterial;
    }
    private void OnDestroy()
    {
        if (_damagableObject is Player)
        {
            (_damagableObject as Player).OnFlashBlink -= FlashBlink_OnFlashBlink;
        }
    }
}