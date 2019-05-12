
using AI.UtilityBasedSystem.Abstractions;

namespace AI.UtilityBasedSystem
{
    /// Copyright © 2016-2019 Lou Vasilev - All rights reserved.
    ///
    /// <summary>
    /// Constructs objects of type TechniqueProficiencyFactor. This is the
    /// factory portion of a Factory design pattern for instantiating factors
    /// to evaluate actions against.
    /// </summary>
    /// 
    /// <author>
    /// Lou Vasilev
    /// </author>
    /// <date>
    /// 05-17-2016
    /// </date>
    public class TechniqueProficiencyFactorPlant : FactorPlant
    {

	    #region Public constructors

	    /// <summary>
	    /// Initializes a new instance of the <see cref="TechniqueProficiencyFactorPlant"/> class.
	    /// </summary>
	    /// <param name="plantName">Plant name.</param>
	    public TechniqueProficiencyFactorPlant(string plantName)
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
		    return new TechniqueProficiencyFactor ();
	    }

	    #endregion

    }
}