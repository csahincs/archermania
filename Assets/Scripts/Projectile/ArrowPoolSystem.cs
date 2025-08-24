using UnityEngine;
using UnityEngine.Pool;

namespace Projectile
{
    public class ArrowPoolSystem : MonoBehaviour
    {
        [SerializeField] private Arrow _arrowPrefab;
        [SerializeField] private int _maxPoolSize = 10;
        [SerializeField] private bool _collectionChecks = true;

        private IObjectPool<Arrow> _pool;
        public IObjectPool<Arrow> Pool
        {
            get
            {
                _pool ??= new ObjectPool<Arrow>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject,
                    _collectionChecks, 10, _maxPoolSize);
                return _pool;
            }
        }

        private Arrow CreatePooledItem()
        {
            var arrow = Instantiate(_arrowPrefab, transform);
            return arrow;
        }

        // Called when an item is returned to the pool using Release
        private static void OnReturnedToPool(Arrow system)
        {
            system.gameObject.SetActive(false);
        }

        // Called when an item is taken from the pool using Get
        private static void OnTakeFromPool(Arrow system)
        {
            system.gameObject.SetActive(true);
        }

        // If the pool capacity is reached then any items returned will be destroyed.
        // We can control what the destroy behavior does, here we destroy the GameObject.
        private static void OnDestroyPoolObject(Arrow system)
        {
            Destroy(system.gameObject);
        }
    }
}
