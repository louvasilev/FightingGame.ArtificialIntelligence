using AI.UtilityBasedSystem.Abstractions;

namespace AI.UtilityBasedSystem
{
    /// Copyright © 2016-2019 Lou Vasilev - All rights reserved.
    ///
    /// <summary>
    /// A concrete implementation of the abstract factory-type class
    /// FactorPlant. Constructs instances of type FightingStyleRankFactor.
    /// </summary>
    /// 
    /// <author>
    /// Lou Vasilev
    /// </author>
    /// <date>
    /// 05-14-2016
    /// </date>
    public class FightingStyleRankFactorPlant : FactorPlant
    {

	    #region Public constructors

	    /// <summary>
	    /// Initializes a new instance of the <see cref="ConcreteFactorPlant"/> class.
	    /// </summary>
	    /// <param name="plantName">Plant name.</param>
	    public FightingStyleRankFactorPlant(string plantName)
	    {
		    _plantName = plantName;
	    }

	    #endregion

	    #region Public methods

	    /// <summary>
	    /// Constructs the factor.
	    /// </summary>
	    /// <returns>The factor.</returns>
	    public override IFactor ConstructFactor ()
	    {
		    return new FightingStyleRankFactor ();
	    }

	    #endregion

    }
}