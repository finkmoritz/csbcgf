using System;
using System.Collections;
using System.Collections.Generic;
using csbcgf;
using Photon.Pun;
using Photon.Realtime;
using UnityEditor;
using UnityEngine;

public class PlayMaker : MonoBehaviourPunCallbacks
{
    private IGame game;
    private Photon.Realtime.Player nonMasterPlayer;
    private GameObject controlPanel;

    private static Vector3 CardDim = new Vector3(1f, 1.5f, 0.05f);

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

        nonMasterPlayer = newPlayer;

        if(PhotonNetwork.IsMasterClient)
        {
            InitGame();
        }
    }

    private void InitGame()
    {
        game = RandomGame();

        InitDecks();
        InitSlots();

        game.StartGame(initialHandSize: 3, initialPlayerLife: 5);

        UpdateCards();
        UpdateUI();
    }

    private void InitDecks()
    {
        int cardUid = 0;
        float distance = 0.02f;
        for (int p = 0; p < 2; ++p)
        {
            IPlayer player = game.Players[p];
            Vector3 position = new Vector3(4f - 8f * p, distance, -3f + 6f * p);
            foreach (MonsterCardWithGameObject monsterCard in player.Deck.AllCards)
            {
                GameObject go = PhotonNetwork.Instantiate(
                    "MonsterCard",
                    position,
                    Quaternion.Euler(-90f, 180f - 180f * p, 0f)
                );
                go.GetPhotonView().RPC("SetUid", RpcTarget.All, cardUid++);
                monsterCard.gameObject = go;

                Card3D card3D = monsterCard.gameObject.GetComponent<Card3D>();
                card3D.SetValue("Mana", monsterCard.ManaValue);
                card3D.SetValue("Attack", monsterCard.AttackValue);
                card3D.SetValue("Life", monsterCard.LifeValue);

                if (p == 1)
                {
                    go.GetPhotonView().TransferOwnership(nonMasterPlayer);
                }

                position.y += distance;
            }
        }
    }

    private void InitSlots()
    {
        float paddingX = 0.25f;
        for (int p = 0; p < 2; ++p)
        {
            IPlayer player = game.Players[p];
            int nSlots = player.Board.MaxSize;
            float startX = -(0.5f * nSlots - 0.5f) * (CardDim.x + 2 * paddingX);
            Vector3 position = new Vector3((1-2*p) * startX, 0, -1 + 2*p);
            for (int i=0; i<nSlots; ++i)
            {
                GameObject go = PhotonNetwork.Instantiate(
                    "Slot",
                    position,
                    Quaternion.identity
                );
                go.GetPhotonView().RPC("Init", RpcTarget.All, p, i);
                position.x += (1 - 2 * p) * (CardDim.x + 2 * paddingX);
            }
        }
    }

    private void UpdateCards()
    {
        UpdateHands();
        UpdateBoards();
    }

    private void UpdateHands()
    {
        const float handContraction = 0.8f;
        for (int p = 0; p < 2; ++p)
        {
            Photon.Realtime.Player networkPlayer = PhotonNetwork.PlayerList[p];
            IPlayer player = game.Players[p];
            int handSize = player.Hand.Size;
            float handAncorX = (1 - 2 * p) * handContraction * (-(0.5f * handSize) + 0.5f) * CardDim.x;
            Vector3 handAncor = new Vector3(handAncorX, 0.75f, -4.25f + 8.5f * p);
            Vector3 distance = new Vector3((1 - 2 * p) * handContraction * CardDim.x, 0f, CardDim.z);
            Quaternion handRotation = Quaternion.Euler(45f, 180f * p, 0f);
            for (int i = 0; i < handSize; ++i)
            {
                MonsterCardWithGameObject monsterCard = (MonsterCardWithGameObject)player.Hand[i];
                GameObject go = monsterCard.gameObject;
                go.GetComponent<Card3D>().photonView.RPC("SetTarget", networkPlayer, handAncor + i * distance, handRotation);
                go.GetComponent<DragDropTransform>().photonView.RPC("SetDraggable", networkPlayer, monsterCard.IsPlayable(game));
                go.GetComponent<DragDropTransform>().photonView.RPC("SetHoverable", networkPlayer, true);
            }
        }
    }

    private void UpdateBoards()
    {
        float paddingX = 0.25f;
        for (int p = 0; p < 2; ++p)
        {
            Photon.Realtime.Player networkPlayer = PhotonNetwork.PlayerList[p];
            IPlayer player = game.Players[p];
            int nSlots = player.Board.MaxSize;
            float startX = -(0.5f * nSlots - 0.5f) * (CardDim.x + 2 * paddingX);
            Vector3 position = new Vector3((1 - 2 * p) * startX, CardDim.z, -1 + 2 * p);
            Quaternion boardRotation = Quaternion.Euler(90f, 180f * p, 0f);
            for (int i = 0; i < nSlots; ++i)
            {
                MonsterCardWithGameObject monsterCard = (MonsterCardWithGameObject)player.Board[i];
                if (monsterCard != null)
                {
                    GameObject go = monsterCard.gameObject;
                    go.GetComponent<Card3D>().photonView.RPC("SetTarget", networkPlayer, position, boardRotation);
                    go.GetComponent<DragDropTransform>().photonView.RPC("SetDraggable", networkPlayer, false);
                    go.GetComponent<DragDropTransform>().photonView.RPC("SetHoverable", networkPlayer, true);
                }
                position.x += (1 - 2 * p) * (CardDim.x + 2 * paddingX);
            }
        }
    }

    private void UpdateUI()
    {
        bool activePlayerIsMaster = game.ActivePlayer == game.Players[0];
        photonView.RPC("SetUIActive", RpcTarget.MasterClient, activePlayerIsMaster);
        photonView.RPC("SetUIActive", RpcTarget.Others, !activePlayerIsMaster);
    }

    [PunRPC]
    private void SetUIActive(bool active)
    {
        if (controlPanel == null)
        {
            controlPanel = GameObject.FindWithTag("Control Panel");
        }
        controlPanel.SetActive(active);
    }

    public void OnEndTurnClicked()
    {
        photonView.RPC("EndTurn", RpcTarget.MasterClient);
    }

    [PunRPC]
    private void EndTurn()
    {
        game.NextTurn();
        UpdateCards();
        UpdateUI();
    }

    public void PlayMonsterCard(int cardUid, Slot slot)
    {
        photonView.RPC("PlayMonsterCardInternal", RpcTarget.MasterClient, cardUid, slot.playerIndex, slot.slotIndex);
    }

    [PunRPC]
    private void PlayMonsterCardInternal(int cardUid, int playerIndex, int slotIndex)
    {
        if (game.ActivePlayer != game.Players[playerIndex])
        {
            return;
        }
        try
        {
            IMonsterCard monsterCard = FindCardByUid(game.ActivePlayer.Hand.AllCards.ConvertAll(c => (MonsterCardWithGameObject)c), cardUid);
            game.ActivePlayer.PlayMonster(game, monsterCard, slotIndex);
            UpdateCards();
        } catch (Exception e)
        {
            Debug.LogError(e.Message + "\n" + e.StackTrace);
        }
    }

    private MonsterCardWithGameObject FindCardByUid(List<MonsterCardWithGameObject> cards, int uid)
    {
        foreach (MonsterCardWithGameObject card in cards)
        {
            if (card.gameObject.GetComponent<Card3D>().uid == uid)
            {
                return card;
            }
        }
        return null;
    }

    private IGame RandomGame()
    {
        var random = new System.Random();
        IPlayer[] players = new csbcgf.Player[2];
        for (int i = 0; i < 2; ++i)
        {
            IDeck deck = new Deck();
            for (int j = 0; j < 30; ++j)
            {
                int mana = random.Next(10) + 1;
                int life = random.Next(mana) + 1;
                int attack = Math.Max(1, mana - life);
                deck.Push(new MonsterCardWithGameObject(mana, attack, life));
            }
            players[i] = new csbcgf.Player(deck);
        }
        return new Game(players);
    }
}
