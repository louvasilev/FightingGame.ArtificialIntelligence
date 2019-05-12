
using AI.UtilityBasedSystem.Abstractions;

namespace AI.UtilityBasedSystem
{
    /// Copyright © 2016-2019 Lou Vasilev - All rights reserved.
    ///
    /// <summary>
    /// Assembles objects of type IFactor.
    /// </summary>
    /// 
    /// <author>
    /// Lou Vasilev
    /// </author>
    /// <date>
    /// 05-14-2016
    /// </date>
    public class FactorAssembler
    {

	    #region Public constructors

	    /// <summary>
	    /// Initializes a new instance of the <see cref="FactorAssembler"/> class.
	    /// </summary>
	    public FactorAssembler()
	    {
	    }

	    #endregion

	    #region Public methods

	    /// <summary>
	    /// Assembles the factor.
	    /// </summary>
	    /// <returns>The factor.</returns>
	    /// <param name="factorPlant">Factor plant.</param>
	    public IFactor AssembleFactor(FactorPlant factorPlant)
	    {
		    IFactor factor = factorPlant.ConstructFactor ();

		    return factor;
	    }

	    #endregion

    }
}