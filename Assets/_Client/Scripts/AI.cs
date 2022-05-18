using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fight_RADAR
{
    public class AI : Spawner
    {
        public override void StartGame(bool value)
        {
            base.StartGame(value);

            if (value)
            {
                List<Unit> mySpawnedUnits = new List<Unit>();
                for (int i = 0; i < GetSpawnCount; i++)
                {
                    Unit u = Instantiate(Game.inst.GetPool.GetUnit());
                    mySpawnedUnits.Add(u);
                    u.LoadUnit(GetTeamID, (Spawner)this);
                }

                LoadUnits(mySpawnedUnits.ToArray());
            }
            else
            {
                ClearUnits();
            }
        }
    }
}
