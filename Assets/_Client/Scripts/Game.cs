using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Fight_RADAR
{
    public class Game : MonoBehaviour
    {
        public static Game inst;

        public UnityEvent<bool> e_startGame;
        public UnityEvent<int, float> e_teamWin;

        [SerializeField] private UnitsPool _myPool;
        public UnitsPool GetPool
        {
            get => _myPool;
            private set => _myPool = value;
        }

        [SerializeField] private Color[] teamColors;
        public Color GetTeamColor(int id)
        {
            if (id < 0 || id > teamColors.Length)
            {
                Debug.LogError("Wrong Range of Colors: Team: " + id);
                return Color.black;
            }

            return teamColors[id];
        }

        private float _gameStartTime;
        public float GetStartPlayTime
        {
            get => _gameStartTime;
            private set => _gameStartTime = value;
        }

        private List<Spawner> _allTeams = new List<Spawner>();
        public List<Spawner> GetTeams
        {
            get => _allTeams;
            private set => _allTeams = value;
        }
        
        private void Awake()
        {
            //Init Singleton
            if (inst == null) inst = this;
            else if (inst == this) Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }

        public void StartGame(bool value)
        {
            e_startGame.Invoke(value);

            if (value)
            {
                GetStartPlayTime = Time.time;
            }

            else
            {
                GetTeams = new List<Spawner>();
            }
        }

        public void TeamWin(int id, float time)
        {
            e_teamWin?.Invoke(id, time);
            Debug.Log("Game Time: " + time + " Team Win: " + id);
        }

        public void AddTeam(Spawner team)
        {
            GetTeams.Add(team);
        }


        /// <summary>
        /// Get Random Target
        /// </summary>
        /// <param name="team"> Team ID </param>
        /// <returns></returns>
        public Unit GetRandomUnitFromEnemyTeam(int team)
        {
            for (int i = 0; i < GetTeams.Count; i++)
            {
                if (team != GetTeams[i].GetTeamID)
                {
                    return GetTeams[i].GetRandomUnit;
                }
            }

            Debug.LogError("Cant Find Unit");
            return null;
        }

        /// <summary>
        /// When Unit Kill Another Unit Check All Other Enemy Units Die
        /// </summary>
        /// <param name="unit"> Killer </param>
        /// <returns> End </returns>
        public bool CheckGameEnd(Unit unit)
        {
            bool unitTeamWin = true;

            for (int i = 0; i < GetTeams.Count; i++)
            {
                if (GetTeams[i].GetTeamID == unit.GetTeamID) continue;

                for (int u = 0; u < GetTeams[i].GetSpawnedUnits.Length; u++)
                {
                    if (GetTeams[i].GetSpawnedUnits[u].GetStatus != Unit.UnitStatus.die)
                    {
                        unitTeamWin = false;
                    }
                }
            }

            if (unitTeamWin) TeamWin(unit.GetTeamID, (Time.time - GetStartPlayTime));

            return unitTeamWin;
        }
    }
}
