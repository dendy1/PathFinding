using System;
using UnityEngine;
using Color = UnityEngine.Color;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class Spawner : MonoBehaviour
{
    [Header("Ground Settings")] 
    public Transform groundPlane;

    [Header("Spawn Zone Settings")] [SerializeField]
    private bool preview;
    [SerializeField] private CustomMath.Borders innerBorders;
    [SerializeField] private CustomMath.Borders externalBorders;

    [Header("Spawner Settings")] 
    [SerializeField] private GameObject[] creepSamples;
    [SerializeField] private float spawnTime;
    [SerializeField] private int maxEnemies = 50;
    [SerializeField] private bool isEnabled;

    private float _spawnerTimer;
    private bool _previewFlag;
    private int _currentEnemyCount;
    
    private void Start()
    {
        _spawnerTimer = spawnTime;
        EventManager.SubscribeToEvent("EnemyDied", OnEnemyDied);
    }

    private void Update()
    {
        if (!isEnabled) return;
        
        _spawnerTimer -= Time.deltaTime;
        if (_spawnerTimer <= 0 && _currentEnemyCount < maxEnemies)
        {
            var index = Random.Range(0, creepSamples.Length);
            var randomPosition = RandomVector3();

            //if (!PoolManager.GetObject(creepSamples[index].name, randomPosition, Quaternion.identity))
            //{
            Instantiate(creepSamples[index], randomPosition, Quaternion.identity);  
            //}

            _currentEnemyCount++;
            _spawnerTimer = spawnTime;
        }
    }

    private Vector3 RandomVector3()
    {
        int randomX = 0, randomZ = 0;
        if (Random.Range(0, 2) < 1)
        {
            randomX = CustomMath.RandomValueFromRanges(
                new CustomMath.Range(-externalBorders.left, -innerBorders.left),
                new CustomMath.Range(innerBorders.right, externalBorders.right));
            randomZ = CustomMath.RandomValueFromRanges(new CustomMath.Range(-externalBorders.bottom, externalBorders.top));
        }
        else
        {
            randomX = CustomMath.RandomValueFromRanges(new CustomMath.Range(-externalBorders.left, externalBorders.right));
            randomZ = CustomMath.RandomValueFromRanges(
                new CustomMath.Range(-externalBorders.bottom, -innerBorders.bottom),
                new CustomMath.Range(innerBorders.top, externalBorders.top));
        }
        
        return new Vector3(randomX, 0f, randomZ) + groundPlane.position;
    }

    private void OnDrawGizmosSelected()
    {
        if (!preview)
            return;
        
        innerBorders.Normalize(externalBorders);

        Vector3[] inner = innerBorders.GetPoints(groundPlane.position);
        Vector3[] external = externalBorders.GetPoints(groundPlane.position);

        var lineWidth = 5;
        var lineColor = Color.blue;
        
        GizmosExtensions.DrawBorders(lineColor, lineWidth, inner, external);
    }

    private void OnEnemyDied(object sender, EventArgs args)
    {
        _currentEnemyCount--;
    }
}
