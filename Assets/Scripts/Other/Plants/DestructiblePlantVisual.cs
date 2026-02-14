using UnityEngine;

public class DestructiblePlantVisual : MonoBehaviour
{
    [SerializeField] private DestructiblePlant _destructiblePlant;
    [SerializeField] private GameObject _stumpDeathVFXPrefab;

    void Start()
    {
        _destructiblePlant.OnDestructibleTakeDamage += _destructiblePlant_OnDestructibleTakeDamage; ;
    }

    private void _destructiblePlant_OnDestructibleTakeDamage(object sender, System.EventArgs e)
    {
        ShowDeathVFX();
    }

    private void ShowDeathVFX()
    {
        Instantiate(_stumpDeathVFXPrefab, _destructiblePlant.transform.position, Quaternion.identity);
    }

    private void OnDestroy()
    {
        _destructiblePlant.OnDestructibleTakeDamage -= _destructiblePlant_OnDestructibleTakeDamage;
    }
}
