# C# Battle Card Game Framework (CSBCGF)

## Overview

The C# Battle Card Game Framework facilitates the process of developing a custom
battle card game (such as Magic The Gathering, Pokémon and Hearthstone) in C#.
It already provides a basic event-driven game loop and classes to derive from.

## IGame interface & Game class

The ``Game`` class is the central object within the framework implementing the
``IGame`` interface. It contains the whole state of a battle card match and
provides methods to alter this state:


