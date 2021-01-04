using System;
using System.Collections;
using System.Collections.Generic;
using csbcgf;
using Photon.Pun;
using UnityEngine;

public class PlayMaker : MonoBehaviourPunCallbacks
{
    private IGame game;

    // Start is called before the first frame update
    void Start()
    {
        InitGame(); //TODO: Remove, only used for testing
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);

        if(PhotonNetwork.IsMasterClient)
        {
            InitGame();
        }
    }

    private void InitGame()
    {
        game = RandomGame();

        float distance = 0.02f;
        for (int p=0; p<2; ++p)
        {
            IPlayer player = game.Players[p];
            Vector3 position = new Vector3(4f - 8f * p, distance, -3f + 6 * p);
            foreach (MonsterCard3D monsterCard in player.Deck.AllCards)
            {
                GameObject gameObject = PhotonNetwork.Instantiate("MonsterCard", position, Quaternion.identity, 0);
                gameObject.transform.Rotate(-90f, 180f - 180f * p, 0f);
                monsterCard.gameObject = gameObject;

                monsterCard.SetMana(monsterCard.ManaValue);
                monsterCard.SetAttack(monsterCard.AttackValue);
                monsterCard.SetLife(monsterCard.LifeValue);

                position.y += distance;
            }
        }

        game.StartGame(initialHandSize: 3, initialPlayerLife: 5);
    }

    private IGame RandomGame()
    {
        var random = new System.Random();
        IPlayer[] players = new Player[2];
        for (int i=0; i<2; ++i)
        {
            IDeck deck = new Deck();
            for(int j=0; j<30; ++j)
            {
                int mana = random.Next(10) + 1;
                int life = random.Next(mana) + 1;
                int attack = mana - life;
                deck.Push(new MonsterCard3D(mana, attack, life));
            }
            players[i] = new Player(deck);
        }
        return new Game(players);
    }
}
