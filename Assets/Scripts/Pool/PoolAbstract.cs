using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShootEmUp 
{
    public class PoolAbstract<TObject> : MonoBehaviour, IPool<TObject>
    {
        [SerializeField]
        private Transform _container;

        private readonly HashSet<TObject> _activeObjects = new();
        private readonly Queue<TObject> _objectsPool = new();

        public bool AddToActiveObjects(TObject body)
        {
            return _activeObjects.Add(body);
        }

        public void Enqueue(TObject body)
        {
            _objectsPool.Enqueue(body);
        }

        public TObject[] GetArrayFromActiveObjects()
        {
            return _activeObjects.ToArray();
        }

        public bool RemoveFromActiveObjects(TObject body)
        {
            return _activeObjects.Remove(body);
        }

        public bool TryDequeue(out TObject body)
        {
            return _objectsPool.TryDequeue(out body);
        }

        public Transform GetContainer()
        {
            return _container;
        }

        public int GetActiveObjectsCount()
        {
            return _activeObjects.Count;
        }
    }
}