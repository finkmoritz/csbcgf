# C# Battle Card Game Framework (CSBCGF)

The C# Battle Card Game Framework facilitates the process of developing a custom
battle card game (such as Magic The Gathering, Pokémon and Hearthstone) in C#.
It already provides a basic event-driven game loop and classes to derive from.

## IGame interface & Game class

The ``Game`` class is the central object within the framework implementing the
``IGame`` interface. It contains the whole state of a battle card match and
provides methods to alter its state:
- EndTurn: Ends the current turn and starts a new turn, activating the next player.
- Queue & Process: see section IAction.

## IPlayer interface & Player class

The ``Player`` class represents a participant in a game. It holds the player's
cards in four different collections:
- Deck: Source of cards that the player draws.
- Hand: Source of cards that the player might bring onto the board.
- Board: Location of monster cards that actively participate in the battle.
- Graveyard: Depot for discarded cards.

Implementing the ``IPlayer`` interface, the ``Player`` class provides methods
to interact with those cards:
- DrawCard: Draw a card from the deck.
- PlayMonster: Add a monster card from the player's hand to the player's board.
- PlaySpell: Play a spell card from the player's hand.
- Attack: Initiate a fight between a befriended and an opposing monster card.

## IDeck interface & Deck implementations

The ``IDeck`` interface provides some useful methods to interact with a
collection of cards. This framework includes the following implementations:
- Board: Array of a fixed number of card slots that can either contain a card
or be empty.
- Hand: List of an arbitrary amount of cards (up to a defined maximum).
- StackedDeck: Stack of cards (e.g. to be used as deck or graveyard).

## ICard interface & Card class

The ``ICard`` interface exposes the ManaStat property (i.e. the card's costs
in Mana) and a method ``IsPlayable`` that checks whether this card can be
played (i.e. cast a spell card or add a monster card to the board) by the
active player.

### IMonsterCard interface & MonsterCard class

The ``MonsterCard`` class is an abstract base class for monster cards that can
be played onto the board and attack other characters (i.e. monster cards or players).

### ISpellCard interface & SpellCard implementations

The ``SpellCard`` class is an abstract base class for spell cards that have an
immediate effect once played from the player's hand. There are two subtypes:
- TargetlessSpellCard: A spell card that can be played without specifying a
target character.
- TargetfulSpellCard: A spell card that can only be played by specifying a
target character first.

### ICompoundCard interface & CompoundCard class

``CompoundCard``s are ``Card``s which in turn are made up of one or several
``Card``s (so called components). Such components can be dynamically added
and removed and may contribute to the ``Stat``s and methods of the resulting
``CompoundCard``.

Usage of a ``CompoundCard`` is preferred over a simple ``Card`` as it not only
facilitates dynamic composition of custom cards but also allows for battle
card game specific mechanics, such as:
- Enforcements: Stats and effects (see IReaction) can be added and removed
as card components.
- Permanent effects: Permanent effects can be added and removed as card
components.

## IStat interface & Stat implementations

A ``Stat`` is an atomic value that is part of a game objects state. It provides
the following properties:
- Value: The current integer value.
- MaxValue: The maximum integer value this ``Stat``'s Value can be set to.

This framework features the following ``IStat`` implementations:
- ManaStat: Represents either the costs for playing a card or the mana available
to a player.
- LifeStat: Represents the life points of the character.
- AttackStat: Represents the potential damage dealt in a fight with this
character.

## ICharacter interface

A character is a participant with an ``AttackStat`` and a ``LifeStat`` that
dies once its life reaches a value of zero. The ``IPlayer`` and ``IMonsterCard``
interfaces inherit the ``ICharacter`` interface.

## IAction interface & how to properly change a Game's state

Changes to the game state should only be performed via actions (i.e. classes
implementing ``IAction``)! More specifically only in the ``Execute`` method.
Actions must not be executed immediately or arbitrarily, but rather be queued
via ``Game.Queue`` and executed via ``Game.Process`` methods.

The resulting pipeline looks like this:
1. An ``Action`` is initialized (e.g. with some further information needed to
execute the action later on).
2. The ``Action`` is queued for execution via ``Game.Queue`` method.
3. Repeat 1. & 2. for all ``Action``s that should be executed 'simultaneously'.
4. Execute all queued ``Action``s via ``Game.Process`` method (first in first out).
For each ``Action`` to be executed, the framework first checks if it can be
executed (``IAction.IsExecutable``) immediately before the actual execution
(``IAction.Execute``). Non-executable ``Action``s are discarded.

## IReactive & IReaction interfaces

``Card``s implement the ``IReactive`` interface which means that implementations
of the ``IReaction`` interface can be added/removed to/from cards. After the
execution of each ``Action`` (via Game.Process method), this ``Action`` is fed into
the ``ReactTo`` method of every ``Action`` in every ``Card`` and all ``Action``s
returned from this method are in turn again fed into the action queue and
subsequently processed.
