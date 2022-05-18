using UnityEngine;

namespace Fight_RADAR
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private int _spawnCount;
        public int GetSpawnCount
        {
            get => _spawnCount;
            private set => _spawnCount = value;
        }

        [SerializeField] private int _teamID;
        public int GetTeamID
        {
            get => _teamID;
            private set => _teamID = value;
        }

        [SerializeField] private BoxCollider spawnZone;
        public Vector3 GetRandomSpawnPosition()
        {
            Vector3 extents = spawnZone.size / 2f;
            Vector3 point = new Vector3(
                Random.Range(-extents.x, extents.x),
                Random.Range(-extents.y, extents.y),
                Random.Range(-extents.z, extents.z)
            );

            return spawnZone.transform.TransformPoint(point);
        }

        private Unit[] _spawnedUnits;
        public Unit[] GetSpawnedUnits
        {
            get => _spawnedUnits;
            private set => _spawnedUnits = value;
        }
        public Unit GetRandomUnit
        {
            get => _spawnedUnits[Random.Range(0, _spawnedUnits.Length)];
        }

        private void Start()
        {
            Game.inst.e_startGame.AddListener(StartGame);
        }

        private void OnDestroy()
        {
            Game.inst.e_startGame.RemoveListener(StartGame);
        }

        public virtual void StartGame(bool value)
        {
            Game.inst.AddTeam(this);
        }

        public void LoadUnits(Unit[] units)
        {
            GetSpawnedUnits = units;
        }

        public void ClearUnits()
        {
            for (int i = 0; i < GetSpawnedUnits.Length; i++)
            {
                Destroy(GetSpawnedUnits[i].gameObject);
            }
        }
    }
}
