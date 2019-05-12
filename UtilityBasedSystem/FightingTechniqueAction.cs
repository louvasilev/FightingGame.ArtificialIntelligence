
using AI.Dependencies;
using AI.Support.Abstractions;

namespace AI.UtilityBasedSystem
{
    /// Copyright © 2016-2019 Lou Vasilev - All rights reserved.
    ///
    /// <summary>
    /// A concrete implementation of the Action abstract class.
    /// It represents an action associated with a Fighting Arts Technique.
    /// </summary>
    /// 
    /// <author>
    /// Lou Vasilev
    /// </author>
    /// <date>
    /// 02-14-2016
    /// </date>
    public class FightingTechniqueAction : AIAction
    {

        TechniqueProficiency _techniqueProdiciency;

        #region Public properties

        /// <summary>
        /// Gets the technique proficiency associated with the action.
        /// </summary>
        /// <value>The technique proficiency.</value>
        public TechniqueProficiency Proficiency
        {
            get
            {
                return _techniqueProdiciency;
            }
        }

        #endregion

        #region Public constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FightingTechniqueAction"/> class.
        /// </summary>
        /// <param name="techniqueProdiciency">Technique prodiciency.</param>
        /// 
        /// TODO: currently this class is tightly coupled with class TechniqueProficiency.
        /// They should be loosely coupled instead.
        public FightingTechniqueAction(TechniqueProficiency techniqueProdiciency)
        {
            if (techniqueProdiciency == null)
            {
                throw new System.ArgumentException("techniqueProdiciency reference not found.");
            }

            _techniqueProdiciency = techniqueProdiciency;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Determines whether this instance is identical to the specified action.
        /// </summary>
        /// <returns>true</returns>
        /// <c>false</c>
        /// <param name="action">The Action to compare this one to.</param>
        public override bool IsIdenticalTo(AIAction action)
        {
            if (action == null)
            {
                return false;
            }

            // The given Action is to equal to this one if their TechniqueProficiency
            // attributes are also equal.
            if (((FightingTechniqueAction)action).Proficiency.IsIdenticalTo(_techniqueProdiciency))
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}