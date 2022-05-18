using UnityEngine;

namespace Fight_RADAR
{
    public class UnitsPool : MonoBehaviour
    {
        [SerializeField] private Unit[] _allUnits;
        public Unit GetUnit(int id)
        {
            if (id < 0 || id > _allUnits.Length)
            {
                Debug.LogError("Wrong Range of Colors: Team: " + id);
                return null;
            }

            return _allUnits[id];
        }

        public Unit GetUnit()
        {
            return _allUnits[Random.Range(0, _allUnits.Length)];
        }
    }
}
