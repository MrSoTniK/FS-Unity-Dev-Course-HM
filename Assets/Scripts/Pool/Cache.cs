using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp 
{
    public class Cache : MonoBehaviour
    {
        public Action<GameObject> OnObjectRemoved;

        [SerializeField]
        private LevelBounds _levelBounds;

        [SerializeField]
        private PoolAbstract<GameObject> _pool;

        private readonly List<GameObject> m_cache = new();

        private void FixedUpdate()
        {
            ResetCache();
        }

        private void ResetCache()
        {
            m_cache.Clear();
            var array = _pool.GetArrayFromActiveObjects();
            m_cache.AddRange(array);

            for (int i = 0, count = m_cache.Count; i < count; i++)
            {
                GameObject go = m_cache[i];
                if (!_levelBounds.InBounds(go.transform.position))
                {
                    OnObjectRemoved?.Invoke(go);               
                }
            }
        }
    }
}