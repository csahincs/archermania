using UnityEngine;

namespace Automation
{
    public class InstantiateFloors : MonoBehaviour
    {
        [SerializeField] private GameObject _floorPrefab;
        [SerializeField] private int _count;

        [ContextMenu( "Instantiate Objects" )]
        public void InstantiateObjects()
        {
            for (int i = -_count/2; i < _count/2; i++)
            {
                for (int j = -_count/2; j < _count/2; j++)
                {
                    var go = Instantiate(_floorPrefab, new Vector3(i, 0, j), Quaternion.identity);
                    go.transform.parent = transform;   
                }
            }
        }
    }
}
