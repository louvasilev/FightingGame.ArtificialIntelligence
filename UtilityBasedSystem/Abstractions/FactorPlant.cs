
namespace AI.UtilityBasedSystem.Abstractions
{
    /// Copyright © 2016 Lou Vasilev - All rights reserved.
    ///
    /// <summary>
    /// An abstract class for constructing objects of type IFactor.
    /// This class corresponds to a factory in the design pattern
    /// of the same name.
    /// </summary>
    /// 
    /// <author>
    /// Lou Vasilev
    /// </author>
    /// <date>
    /// 05-14-2016
    /// </date>
    public abstract class FactorPlant
    {
	    protected string _plantName;

	    /// <summary>
	    /// Constructs the factor.
	    /// </summary>
	    /// <returns>The factor.</returns>
	    public abstract IFactor ConstructFactor ();
    }
}