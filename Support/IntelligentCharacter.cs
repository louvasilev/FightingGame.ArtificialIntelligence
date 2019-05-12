
using UnityEngine;
using AI.Dependencies.Abstractions;
using AI.Support.Abstractions;

namespace AI.Support
{
    /// Copyright © 2015-2019 Lou Vasilev - All rights reserved.
    ///
    /// <summary>
    /// A class that defines a character whose behavior is
    /// controlled by Artificial Intelligence (AI).
    /// </summary>
    /// <description>
    /// Which particular character an insance of the class
    /// is tied to is defined by the Fighter field.
    /// </description>
    /// 
    /// <author>
    /// Lou Vasilev
    /// </author>
    /// <date>
    /// 12-08-2015
    /// </date>
    public class IntelligentCharacter : IIntelligentCharacter
    {
        // A reference to the Fighter game object an instance
        // of this class is responsible for.
        private IFighter _fighter;

        private FightersMind _fightersMind;

        #region Public properties

        /// <summary>
        /// Gets the fighter.
        /// </summary>
        /// <value>The fighter.</value>
        public IFighter Fighter
        {
            get
            {
                return _fighter;
            }
        }

        /// <summary>
        /// Gets the Finite State Machine (FSM) component of the
        /// AI system that controls the behavior of this intelligent
        /// character.
        /// </summary>
        /// <value>The mind of the fighter.</value>
        public FightersMind MindOfTheFighter
        {
            get
            {
                return _fightersMind;
            }
        }

        #endregion

        #region Public constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="IntelligentCharacter"/> class.
        /// </summary>
        /// <param name="fighter">The Fighter game object this intelligent character represents
        /// in the AI system.</param>
        /// <param name = "fightersMind">The FSM controlling the behavior of this character.</param>
        /// <param name = "fightManager">An implementation of FightManagerBase.</param>
        public IntelligentCharacter(IFighter fighter,
            MonoBehaviour fightersMind)
        {
            if (!(fightersMind is FightersMind))
            {
                throw new System.ArgumentException("Argument fightersMind is not of type FightersMind.");
            }

            _fighter = fighter ?? throw new System.ArgumentException(nameof(fighter) + " reference not found.");
            _fightersMind = (FightersMind)fightersMind ?? throw new System.ArgumentException(nameof(fightersMind) + " reference not found.");
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Adds hard-coded techniques for AI controlled character.
        /// </summary>
        /// TODO: this is no longer being used.
        public void PickTechniques()
        {
        }

        #endregion

    }
}