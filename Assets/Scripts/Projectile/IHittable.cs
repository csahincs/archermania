using UnityEngine;

namespace Projectile
{
    public interface IHittable
    {
        public void OnHit(GameObject hitGo);
    }
}
