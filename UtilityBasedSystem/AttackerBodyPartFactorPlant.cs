
using AI.UtilityBasedSystem.Abstractions;

namespace AI.UtilityBasedSystem
{
    /// Copyright © 2016-2019 Lou Vasilev - All rights reserved.
    ///
    /// <summary>
    /// Attacker body part factor plant.
    /// </summary>
    public class AttackerBodyPartFactorPlant : FactorPlant
    {

	    #region Public constructors

	    /// <summary>
	    /// Initializes a new instance of the <see cref="AttackerBodyPartFactorPlant"/> class.
	    /// </summary>
	    /// <param name="plantName">Plant name.</param>
	    public AttackerBodyPartFactorPlant(string plantName)
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
		    return new AttackerBodyPartFactor ();
	    }

	    #endregion

    }
}