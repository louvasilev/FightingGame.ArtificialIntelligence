
using AI.Dependencies;
using AI.Support.Abstractions;
using AI.UtilityBasedSystem.Abstractions;

namespace AI.UtilityBasedSystem
{
    /// Copyright © 2016 Lou Vasilev - All rights reserved.
    ///
    /// <summary>
    /// A concrete implementation of the abstract factory-type class
    /// ActionPlant.
    /// </summary>
    /// 
    /// <author>
    /// Lou Vasilev
    /// </author>
    /// <date>
    /// 2016
    /// </date>
    public class ConcreteActionPlant : ActionPlant
    {

        #region Public constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcreteActionPlant"/> class.
        /// </summary>
        public ConcreteActionPlant(string plantName)
        {
            _plantName = plantName;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Constructs the action.
        /// </summary>
        /// <returns>The action.</returns>
        /// <param name="techniqueProdiciency">Technique prodiciency.</param>
        /// 
        /// TODO: currently this class is tightly coupled with class TechniqueProficiency.
        /// They should be loosely coupled instead.
        public override AIAction ConstructAction(TechniqueProficiency techniqueProdiciency)
        {
            if (techniqueProdiciency == null)
            {
                throw new System.ArgumentException("techniqueProdiciency reference not found.");
            }

            return new FightingTechniqueAction(techniqueProdiciency);
        }

        #endregion

    }
}