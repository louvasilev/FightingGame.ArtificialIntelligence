
using AI.Dependencies;
using AI.Support.Abstractions;

namespace AI.UtilityBasedSystem.Abstractions
{
    /// Copyright © 2016 Lou Vasilev - All rights reserved.
    ///
    /// <summary>
    /// An abstract class for constructing objects of (the abstact) type
    /// Action. This class corresponds to a factory in the design pattern
    /// of the same name.
    /// </summary>
    /// 
    /// <author>
    /// Lou Vasilev
    /// </author>
    /// <date>
    /// 02-14-2016
    /// </date>
    public abstract class ActionPlant
    {
	    protected string _plantName;

	    /// <summary>
	    /// Constructs the action.
	    /// </summary>
	    /// <returns>The action.</returns>
	    /// <param name="techniqueProdiciency">Technique prodiciency.</param>
	    /// 
	    /// TODO: currently this class is tightly coupled with class TechniqueProficiency.
	    /// They should be loosely coupled instead.
	    public abstract AIAction ConstructAction (TechniqueProficiency techniqueProdiciency);
    }
}