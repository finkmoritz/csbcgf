﻿using System;

namespace csbcgf
{
    [Serializable]
    public abstract class SpellCard : Card, ISpellCard
    {
        /// <summary>
        /// Represents a certain type of Card that has an
        /// immediate effect on the Game's state.
        /// </summary>
        /// <param name="mana"></param>
        public SpellCard(int mana) : base(mana)
        {
        }
    }
}