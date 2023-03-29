using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour, IEventListenable
{
    [SerializeField] GameObject _basicBulletHole;
    [SerializeField] int _numberOfObjectsToInstantiate = 20;

   Queue<GameObject> _bulletHoles = new Queue<GameObject>();

    void Start() => InstantiatePoolObjects();
  
    void InstantiatePoolObjects()
    {
        if (_basicBulletHole != null)
        {
            for (int i = 0; i < _numberOfObjectsToInstantiate; i++)
            {
                var temp = Instantiate(_basicBulletHole, transform.position, Quaternion.identity, transform.parent = transform);
                temp.SetActive(false);
                _bulletHoles.Enqueue(temp);
            }
        }
        else Logger.LogError("basic bullet hole prefab has not been setup", this);
    }
   
    public void PlaceBulletHole(Vector3 hitPoint)
    {
        if (_bulletHoles.TryPeek(out GameObject bulletHole))
        {
            bulletHole.transform.position = hitPoint;
            bulletHole.SetActive(true);
            MoveQueueObjectToLastPosition();
        }
      
    }

    void MoveQueueObjectToLastPosition()
    {
        var temp = _bulletHoles.Peek();
        _bulletHoles.Dequeue();
        _bulletHoles.Enqueue(temp);
    }

    public void OnEnable() => GameEvents.onBulletHittingSurface.AddListener(PlaceBulletHole);
    public void OnDisable() => GameEvents.onBulletHittingSurface.RemoveListener(PlaceBulletHole);
   


}
