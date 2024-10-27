using UnityEngine;

namespace ShootEmUp 
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Pool<GameObject> _pool;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private int _quantity;
        [SerializeField] private Transform _worldTransform;

        private void Awake()
        {
            for (var i = 0; i < _quantity; i++)
            {
                var body = Instantiate(_prefab, _pool.GetContainer());
                _pool.Enqueue(body);
            }
        }

        public TComponent SpawnComponent<TComponent>() where TComponent : MonoBehaviour
        {
            TComponent component = null;

            if (!_pool.TryDequeue(out var gameObject) || gameObject.TryGetComponent(out component))
            {
                gameObject = Instantiate(_prefab, _pool.GetContainer());             
            }

            return component;
        }

        public GameObject SpawnGameObject()
        {
            switch (_pool.TryDequeue(out var gameObject))
            {
                case true:
                    gameObject.transform.SetParent(_worldTransform);
                    break;
                case false:
                    gameObject = Instantiate(_prefab, _worldTransform);
                    break;
            }

            return gameObject;
        }

        public bool RemoveFromActiveObjects(GameObject gameObject) 
        {
            return _pool.RemoveFromActiveObjects(gameObject);
        }

        public void Enqueue(GameObject gameObject) 
        {
            _pool.Enqueue(gameObject);
        }

        public Transform GetContainer()
        {
            return _pool.GetContainer();
        }

        public GameObject[] GetArrayFromActiveObjects() 
        {
            return _pool.GetArrayFromActiveObjects();
        }

        public bool AddToActiveObjects(GameObject gameObject)
        {
            return _pool.AddToActiveObjects(gameObject);
        }

        public int GetActiveObjectsCount()
        {
            return _pool.GetActiveObjectsCount();
        }
    }
}