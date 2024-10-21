using UnityEngine;

namespace ShootEmUp 
{
    public interface IPool<TObject>
    {      
        public bool RemoveFromActiveObjects(TObject body);

        public bool AddToActiveObjects(TObject body);

        public bool TryDequeue(out TObject body);

        public void Enqueue(TObject body);

        public TObject[] GetArrayFromActiveObjects();

        public Transform GetContainer();

        public int GetActiveObjectsCount();
    }
}