
using AI.Dependencies;
using AI.Dependencies.Abstractions;
using System;
using System.Collections.Generic;

namespace AI.FiniteStateMachine
{
    /// Copyright © 2015-2019 Lou Vasilev - All rights reserved.
    ///
    /// <summary>
    /// Defines the state of the fight. It contains information
    /// about the game world that the AI would need to know in order
    /// to make decisions.
    /// </summary>
    /// <description>
    /// The information in question will include the following:
    /// 
    ///     --the status of each of the Intelligent Agent's (IA) body
    ///       parts, i.e. healthy or not, any damage to it, etc.
    ///     --same information as above, but about the main character
    ///     --the Fighting Arts style utilized by the IA
    ///     --same as above, but for the main character
    ///     --degree of skill in Fighting Art style for both the IA and
    ///       the main character.
    /// </description>
    /// 
    /// <author>
    /// Lou Vasilev
    /// </author>
    /// <date>
    /// 11-28-2015
    /// </date>
    public class FightState
    {
        private FightManagerBase _fightManager;

        #region Public constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="fightManager">A child class of FightManagerBase for managing a
        /// martial arts fight.</param>
        public FightState(FightManagerBase fightManager)
        {
            _fightManager = fightManager ?? throw new ArgumentNullException(nameof(fightManager) + " reference is not found.");
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets the main character.
        /// </summary>
        /// <returns>The main character.</returns>
        public Fighter GetMainCharacter()
        {
            // Get all the fighters from the main class in the core
            // gameplay system.
            Fighter[] fighters = _fightManager.Fighters;

            // Loop through the fighters obtained above and, for each
            // one, check the type of the Character field. If it is
            // MainCharacter, then we know that's the Fighter object
            // we need.
            foreach (Fighter fighter in fighters)
            {
                Character character = fighter.Character;
                if (character is MainCharacter)
                {
                    return fighter;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the opponents.
        /// </summary>
        /// <returns>The opponents.</returns>
        public List<Fighter> GetOpponents()
        {
            List<Fighter> opponents = new List<Fighter>();

            // Get all the fighters from the main class in the core
            // gameplay system.
            Fighter[] fighters = _fightManager.Fighters;

            // Loop through the fighters obtained above and, for each
            // one, check the type of the Character field. If it is
            // ActionCapableCharacter, then we know that's one of the
            // Fighter objects we need.
            foreach (Fighter fighter in fighters)
            {
                Character character = fighter.Character;
                if (character is ActionCapableCharacter)
                {
                    opponents.Add(fighter);
                }
            }

            return opponents;
        }

        #endregion

    }
}