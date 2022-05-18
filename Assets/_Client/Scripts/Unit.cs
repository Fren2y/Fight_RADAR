using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Fight_RADAR
{
    public class Unit : MonoBehaviour
    {
        public enum UnitStatus
        {
            move,
            attack,
            die
        }

        private UnitStatus _myStatus;
        public UnitStatus GetStatus
        {
            get => _myStatus;
            private set => _myStatus = value;
        }

        [SerializeField] private UnitData _uData;
        public UnitData GetUnitData
        {
            get => _uData;
            private set => _uData = value;
        }

        private int _teamID;
        public int GetTeamID
        {
            get => _teamID;
            private set => _teamID = value;
        }

        [SerializeField] private MeshRenderer meshRend;
        [SerializeField] private NavMeshAgent agent;

        private Unit _target;
        private float _attackTimer;
        private float _unitHP;

        public virtual void LoadUnit(int team, Spawner spawner)
        {
            GetTeamID = team;
            meshRend.material.SetColor("_Color", Game.inst.GetTeamColor(team));
            agent.Warp(spawner.GetRandomSpawnPosition());

            _unitHP = GetUnitData.hp;
            agent.stoppingDistance = GetUnitData.attackRadius;
        }

        private void Update()
        {
            if (GetStatus == UnitStatus.die) return;
            if (_target == null || _target.GetStatus == UnitStatus.die)
            {
                FindTarget(Game.inst.GetRandomUnitFromEnemyTeam(GetTeamID));
                return;
            }

            Debug.DrawLine(transform.position + Vector3.up, _target.transform.position + Vector3.up, Game.inst.GetTeamColor(_teamID));

            float distance = Vector3.Distance(transform.position, _target.transform.position);
            switch (GetStatus)
            {
                case UnitStatus.move:
                    Moving(distance);
                    break;
                case UnitStatus.attack:
                    Attacking(distance);
                    break;
                default:
                    break;
            }
        }

        private void Moving(float dist)
        {
            if (dist <= GetUnitData.attackRadius)
            {
                GetStatus = UnitStatus.attack;
                return;
            }
            else
            {
                agent.SetDestination(_target.transform.position);
            }
        }

        private void Attacking(float dist)
        {
            if (dist > GetUnitData.attackRadius)
            {
                agent.SetDestination(_target.transform.position);
                return;
            }

            _attackTimer -= Time.deltaTime;
            if (_attackTimer <= 0)
            {
                Attack();
                _attackTimer = GetUnitData.attackSpeed;
            }
        }

        public virtual void FindTarget(Unit target)
        {
            _target = target;
        }

        public virtual void Attack()
        {
            if (_target.GetDmg(GetUnitData.attackDmg))
            {
                if (Game.inst.CheckGameEnd(this))
                {
                    //Debug.Log("Team : " + GetTeamID + " Win");
                }
            }
        }

        public virtual bool GetDmg(float amount)
        {
            _unitHP -= amount;

            if (_unitHP <= 0)
            {
                _unitHP = 0;
                Die();
            }

            return _unitHP <= 0;
        }

        public virtual void Die()
        {
            GetStatus = UnitStatus.die;
            meshRend.material.SetColor("_Color", Color.gray);
            agent.enabled = false;
        }
    }
}
