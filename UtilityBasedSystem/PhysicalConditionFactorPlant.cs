
using AI.UtilityBasedSystem.Abstractions;

namespace AI.UtilityBasedSystem
{
    /// Copyright © 2016-2019 Lou Vasilev - All rights reserved.
    ///
    /// <summary>
    /// A concrete implementation of the abstract factory-type class
    /// FactorPlant. Constructs instances of type PhysicalConditionFactor.
    /// </summary>
    public class PhysicalConditionFactorPlant : FactorPlant
    {

        #region Public constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PhysicalConditionFactorPlant"/> class.
        /// </summary>
        /// <param name="plantName">Plant name.</param>
        public PhysicalConditionFactorPlant(string plantName)
        {
            _plantName = plantName;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Constructs the factor.
        /// </summary>
        /// <returns>The factor.</returns>
        public override IFactor ConstructFactor()
        {
            return new PhysicalConditionFactor();
        }

        #endregion

    }
}