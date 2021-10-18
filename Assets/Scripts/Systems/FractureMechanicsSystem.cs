using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScaleBySize
{
    [SerializeField] private AsteroidsSize _size;
    [SerializeField] private float _scale;

    public AsteroidsSize Size => _size;
    public float Scale => _scale;
}

public class FractureMechanicsSystem : BaseSystem
{
    [SerializeField] private List<ScaleBySize> _scaleSize = new List<ScaleBySize>(); 

    private AsteroidsManagerSystem _asteroidsManagerSystem;
    private SpawnAsteroidsSystem _spawnAsteroidsSystem;

    protected override void InitializeData()
    {
        _asteroidsManagerSystem = (AsteroidsManagerSystem)_systemInitializer.GetSystem(SystemType.AsteroidsManagerSys);
        _spawnAsteroidsSystem = (SpawnAsteroidsSystem)_systemInitializer.GetSystem(SystemType.SpawnAsteroidSys);
    }

    public void CheckSize(Asteroid asteroid)
    {
        if (asteroid.Size == AsteroidsSize.Small) return;

        DetermineSize(asteroid);
    }

    private void DetermineSize(Asteroid asteroid)
    {
        float newScale = 0f;

        for (int i = 0; i < _scaleSize.Count; i++)
        {
            if (_scaleSize[i].Size == asteroid.Size)
            {
                newScale = _scaleSize[i].Scale;
            }
        }


        //Template of mechanics
        /*
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = new Vector3(newScale, newScale, newScale);
        sphere.transform.position = asteroid.Position;

        Rigidbody rb = sphere.AddComponent<Rigidbody>();
        rb.useGravity = false;

        float angle = Random.Range(-45f, 45f);
        Quaternion q = Quaternion.Euler(new Vector3(0f, 0f, angle));

        Vector3 newVelocity = (q * asteroid.GetVelocity().normalized) * 1f;
        rb.velocity = newVelocity;
        */
    }
}
