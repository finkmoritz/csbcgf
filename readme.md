# C# Battle Card Game Framework (CSBCGF)

## Overview

The C# Battle Card Game Framework facilitates the process of developing a custom
battle card game (such as Magic The Gathering, Pokémon and Hearthstone) in C#.
It already provides a basic event-driven game loop and classes to derive from.

(see also the [Official CSBCGF Homepage](https://finkmoritz.github.io/pages/csbcgf/index.html))

## Contents

- [Getting Started](#getting-started)
- [Classes & Interfaces](#classes-and-interfaces)
- [Serialization](#serialization)
- [FAQ](#faq)
- [Impressum](#impressum)

---

# Getting Started

Check out how easy it is to setup your own battle card game using **CSBCGF** by
playing the [demo console application](https://github.com/finkmoritz/csbcgf/tree/main/csbcgfdemo).

---

# Classes and Interfaces

## IGame interface & Game class

The ``Game`` class is the central object within the framework implementing the
``IGame`` interface. It contains the whole state of a battle card match and
provides methods to alter its state:
- NextTurn: Ends the current turn and starts a new turn, activating the next player.
- Queue & Process: see section IAction.

There is also a ``IGameState`` interface which represents an ``IGame``, but without
the ability to alter the game's state.

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

## ICardCollection interface & CardCollection class

The ``ICardCollection`` interface provides some useful methods to interact with a
collection of cards.

## ICard interface & Card class

The ``ICard`` interface exposes the ManaValue property (i.e. the card's costs
in Mana) and a method ``IsCastable`` that checks whether this card can be
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

### ICardComponent interface

``Card``s consist of ``CardComponent``s that combinedly make up the ``Card``'s
stats and behaviour. Those components can dynamically be added and removed
from the ``Card`` (e.g. to simulate certain enhancements or enchantments).

While ``SpellCardComponents`` only contain mana costs, ``Action``s to be
executed when the card is played and ``Reaction``s (see IReaction section),
``MonsterCardComponents`` also contain attack and life stats.

## IStat interface & Stat implementations

A ``Stat`` is an atomic value that is part of a game objects state. It provides
the following properties:
- Value: The current integer value.
- BaseValue: The base integer value is used to hold the ``Stat``'s initial (e.g.
life) or maximum (e.g. player mana) value.

This framework features the following ``IStat`` implementations:
- ManaCostStat: Represents the costs for playing a card.
- ManaPoolStat: Represents the mana available to a player.
- LifeStat: Represents the life points of the character.
- AttackStat: Represents the potential damage dealt in a fight with this
character.

## ICharacter interface

A character is a participant with an ``AttackStat`` and a ``LifeStat`` that
dies once its life reaches a value of zero. The ``IPlayer`` and ``IMonsterCard``
interfaces inherit the ``ICharacter`` interface, i.e. within this framework
a character can either be a player or a monster card.

## IAction interface & how to properly change a Game's state

Changes to the game state should only be performed via actions (i.e. classes
implementing ``IAction``)! More specifically only in the ``game.Execute`` method.
This method takes one or more ``IAction``s, adds them to the queue and
executes them one by one. ``IReaction``s triggered by those ``IAction``s
are being executed afterwards (which in turn can trigger further ``IReaction``s).

Since the game state might change before ``IAction.Execute`` is executed,
the ``IAction`` provides a check ``IAction.IsExecutable`` which is executed
immediately before ``IAction.Execute``. If this method evaluates to ``false``
the ``IAction`` will not be executed and simply discarded (i.e. also no
``IReaction``s will be triggered).

### Event

An event is an abstract implementation of an ``IAction`` that does not execute
a game state change but rather serves as marker/trigger. One example would be
the ``StartOfTurnEvent`` which is triggered at the start of each turn.

Additionally, they may be used to mark the start and end of a certain series of
``IAction``s and thereby provide useful information. E.g. before a player draws
a card, a ``StartDrawCardEvent`` is triggered. After the player has drawn the
card, a ``EndDrawCardEvent`` is triggered that also provides the drawn card.

Other useful ``Event``s to listen to and that are provided out of the box are:
- ``[Start/End]AttackEvent``: Provides attacking monster card and target character.
- ``[Start/End]PlayMonsterCardEvent``: Provides monster card and board index.
- ``[Start/End]PlayTargetlessSpellCardEvent``: Provides the spell card.
- ``[Start/End]PlayTargetfulSpellCardEvent``: Provides the spell card and target
character.

## IReactive & IReaction interfaces

``Card``s implement the ``IReactive`` interface which means that implementations
of the ``IReaction`` interface can be added/removed to/from cards. After the
execution of each ``Action`` (via Game.Process method), this ``Action`` is fed into
the ``ReactBefore`` or ``ReactAfter`` method of every ``Action`` in every ``Card`` 
and all ``Action``s returned from this method are in turn again fed into the 
action queue and subsequently processed.

---

# Serialization

The whole game state is meant to be serializable so that it can be sent from server 
to client and vice versa. Hence, the developer needs to ensure that their classes
can be serialized and deserialized correctly. Here are some hints to achieve this:

## Use ``Newtonsoft.Json`` library

If you use below mentioned annotations, make sure to import ``Newtonsoft.Json``.

Example:
```
using Newtonsoft.Json;
```

## Always provide a default constructor

If you implemented one or more constructors, also add one ``protected`` default
constructor without any implementation in its body.

In case this conflicts with your own parameterless ``public`` constructor, add
an optional dummy parameter to your constructor.

Example:
```
public class FarSight : TargetlessSpellCard
{
    protected FarSight() {} // this is used for deserialization only
    
    public FarSight(bool _ = true) : base(new FarSightComponent()) // this is used in your code
    {
    }
...
```

## Keep the whole state in ``protected`` member variables

Make sure that the whole state that needs to be serialized is either contained
in ``protected``/``private`` member variables marked with ``JsonProperty`` or 
``public`` member variables/properties.

If you expose ``protected``/``private`` variables via ``public`` Getters / Setters,
annotate them with ``JsonIgnore`` so that they are not (de)serialized.

Example:
```
public abstract class Compound : ICompound
{
    [JsonProperty]
    protected List<ICardComponent> components = null!;

    protected Compound() {}

    public Compound(List<ICardComponent> components)
    {
        this.components = components;
    }

    [JsonIgnore]
    public List<ICardComponent> Components {
            get => components;
    }
}
```

---

# FAQ

**When should I use card components to assemble cards?**

As often as possible. You can of course change the card's mana costs (``ManaValue``)
or reactions (``AddReaction``/``RemoveReaction``) directly, but this might lead
to unexpected side effects. It is good practice to always add/remove ``IReaction``s/
``IStat``s as components.


**How to properly execute multiple actions?**

Apparently there are two ways to execute multiple ``IAction``s:
1. Call ``game.Execute(IAction)`` multiple times or 
``game.ExecuteSequentially(List<IAction>)``. At each call the ``IAction`` itself
plus all triggered ``IReaction``s are executed if not forbidden by ``IAction.IsExecutable``.
Note that also the triggered ``IReaction``s could in turn trigger further ``IReaction``s.
2. Call ``game.ExecuteSimultaneously(List<IAction>)`` with all ``IAction``s. 
Now all ``IAction``s in the list will be executed first (if not forbidden by 
``IAction.IsExecutable``). Only after that has happened, all ``IReaction``s triggered by 
all those ``IAction``s will be executed. This goes on until no ``IReaction``s have been 
triggered anymore.

So there is definitely a difference between both approaches. Consider e.g. a fight between
two monster cards, where you want both monsters to lower each others life stats
'simultaneously'. If you pick the first approach, the first monster to attack would maybe
kill the second monster and - as reaction - send it to the graveyard. Now the attack from
the second monster could not take place anymore and the first monster leaves the fight
unharmed. In this case you should definitely go with the second approach, which would
modify the life stats of both monsters first, before sending them to the graveyard.


**How can I implement a custom spell card?**

You can either inherit from ``TargetfulSpellCard`` or ``TargetlessSpellCard`` based on whether
your card should feature ``ISpellCardComponent`` that require a target to be selected
(``ITargetful``) or not (``ITargetless``). If at least one component requires a target
you have to inherit from ``TargetfulSpellCard``.


**How can I implement a custom spell card component?**

You can either inherit from ``TargetfulSpellCardComponent`` or ``TargetlessSpellCardComponent``
based on whether your component requires a target to be selected (``ITargetful``) or not
(``ITargetless``).

- ``TargetlessSpellCardComponent``: Override the ``GetActions`` to return the ``IAction``s to
be performed once the corresponding spell card is played.
- ``TargetfulSpellCardComponent``: Override the ``GetActions`` to return the ``IAction``s to
be performed once the corresponding spell card is played. Also override the ``GetPotentialTargets``
method to provide a list of valid targets based on a given game state.

---

# Impressum

The C# Battle Card Game Framework was designed and implemented by
[Moritz Fink](https://finkmoritz.github.io/).
