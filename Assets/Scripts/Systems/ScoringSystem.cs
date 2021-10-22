using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScoreByCharacter
{
    [SerializeField] private ScoringSystem.EnemyType _enemyType;
    [SerializeField] private int _score;

    public ScoringSystem.EnemyType EnemyType => _enemyType;
    public int Score => _score;
}

public class ScoringSystem : BaseSystem
{
    public enum CharacterType { None = -1, MainPlayer, AsteroidPlayer, UFOPlayer }
    public enum EnemyType { None = -1, BigAsteroid, MiddleAsteroid, SmallAsteroid, UFOPlayer }

    public delegate void SetScoreDelegate(int score);
    public event SetScoreDelegate OnSetScoreEvent;

    [SerializeField] private int _totalScore = 0;
    [SerializeField] private List<ScoreByCharacter> _scoreList;

    private GameController _gameController;

    protected override void InitializeData()
    {
        _gameController = _systemInitializer.GameController;
    }

    public override void AdditionalInitialize()
    {
        _gameController.OnStartGameEvent += NullifySystem;
    }

    public void SetSacrifice(BasicCharacter character)
    {
        if (character.Killer == null || character.Killer.CharacterType != CharacterType.MainPlayer) return;

        SetScoreByEnemyType(character);
    }

    private void SetScoreByEnemyType(BasicCharacter character)
    {
        EnemyType type = EnemyType.None;
        switch (character.CharacterType)
        {
            case CharacterType.UFOPlayer:
                {
                    type = EnemyType.UFOPlayer;
                    break;
                }
            case CharacterType.AsteroidPlayer:
                {
                    type = DetermineTheTypeOfAsteroid((Asteroid)character);
                    break;
                }
        }

        int score = GetScoreByChar(type);
        
        SetScore(score);
        OnSetScoreEvent?.Invoke(_totalScore);
    }

    private EnemyType DetermineTheTypeOfAsteroid(Asteroid asteroid)
    {
        EnemyType type = EnemyType.None;

        switch (asteroid.TypeSize)
        {
            case AsteroidsSizeType.BigAsteroid:
                {
                    type = EnemyType.BigAsteroid;
                    break;
                }
            case AsteroidsSizeType.MiddleAsteroid:
                {
                    type = EnemyType.MiddleAsteroid;
                    break;
                }
            case AsteroidsSizeType.SmallAsteroid:
                {
                    type = EnemyType.SmallAsteroid;
                    break;
                }
        }

        return type;
    }

    private void SetScore(int score)
    {
        _totalScore += score;
    }

    private int GetScoreByChar(EnemyType type)
    {
        int score = 0;
        for (int i = 0; i < _scoreList.Count; i++)
        { 
            if(_scoreList[i].EnemyType == type)
            {
                score = _scoreList[i].Score;
            }
        }

        return score;
    }

    private void NullifySystem()
    {
        _totalScore = 0;
    }

    private void OnDisable()
    {
        _gameController.OnStartGameEvent -= NullifySystem;
    }
}
