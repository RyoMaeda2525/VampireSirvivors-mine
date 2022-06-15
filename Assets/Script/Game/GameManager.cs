using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager
{
    static private GameManager _instance = new GameManager();
    static public GameManager Instance => _instance;
    private GameManager() { }

    int _exp = 0;
    [Range(1, 6)]
    int _level = 1;
    int _backLevelExp = 0;
    int _nextLevelExp = 5;
    int _stackLevelup = 0;

    PlayerController _player = null;
    public void SetPlayer(PlayerController p) { _player = p; }
    List<Enemy> _enemies = new List<Enemy>();
    List<Exp> _exps = new List<Exp>();
    List<int> _passive = new List<int>();
    SkillSelect _sklSelect = null;

    public void Setup()
    {
        //ObjectPool‚ÉˆË‘¶‚µ‚Ä‚¢‚é
        _enemies = GameObject.FindObjectsOfType<Enemy>(true).ToList();

        _exps = GameObject.FindObjectsOfType<Exp>(true).ToList();

        _sklSelect = GameObject.FindObjectOfType<SkillSelect>();
    }

    public void GetExperience(int exp)
    {
        if (_nextLevelExp == 0) return;

        _exp += exp;

        //level up
        if (GameData.LevelTable.Count > _level && _exp > GameData.LevelTable[_level])
        {
            _backLevelExp = _nextLevelExp;

            _level++;

            _nextLevelExp = GameData.LevelTable[_level];

            //if (Time.timeScale > 0.99f)
            //{
            //    _sklSelect.SelectStart();
            //    Time.timeScale = 0;
            //}
            //else
            //{
            //    _stackLevelup++;
            //}
        }
    }

    public void LevelUpSelect(SkillSelectTable table)
    {
        switch (table.Type)
        {
            case SelectType.Skill:
                //_player.AddSkill(table.TargetId);
                break;

            case SelectType.Passive:
                _passive.Add(table.TargetId);
                break;

            case SelectType.Execute:
                //TODO:
                break;
        }

        if (_stackLevelup > 0)
        {
            _sklSelect.SelectStartDelay();
            _stackLevelup--;
        }
        else
        {
            Time.timeScale = 1;
        }
    }


    static public PlayerController Player => _instance._player;
    static public List<Enemy> EnemyList => _instance._enemies;
    static public int Level => _instance._level;

    static public int Exp => _instance._exp;

    static public int BackLevelExp => _instance._backLevelExp;

    static public int NextLevelExp => _instance._nextLevelExp;
}

