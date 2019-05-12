using AI.Dependencies;
using AI.Support.Abstractions;
using System.Collections.Generic;
using AI.UtilityBasedSystem.Abstractions;

namespace AI.UtilityBasedSystem
{
    /// Copyright © 2016 Lou Vasilev - All rights reserved.
    ///
    /// <summary>
    /// Provides methods and properties for creating instances of type
    /// Action.
    /// </summary>
    /// 
    /// <author>
    /// Lou Vasilev
    /// </author>
    /// <date>
    /// 02-14-2016
    /// </date>
    public static class ActionAssembler
    {

	    #region Private constructors

	    /// <summary>
	    /// Initializes the <see cref="ActionAssembler"/> class.
	    /// </summary>
	    static ActionAssembler ()
	    {
		
	    }

	    #endregion

	    #region Public methods

	    /// <summary>
	    /// Assembles the action.
	    /// </summary>
	    /// <param name="actionPlant">
	    /// An Action Plant for constructing Action objects.
	    /// </param>
	    /// <param name="techniqueProficiency">
	    /// A TechniqueProficiency to construct an Action from.
	    /// </param>
	    /// <returns>The action.</returns>
	    public static AIAction AssembleAction (ActionPlant actionPlant, TechniqueProficiency techniqueProficiency)
	    {
		    if (actionPlant == null)
		    {
			    throw new System.ArgumentException ("ActionPlant reference could not be created.");
		    }

		    if (techniqueProficiency == null)
		    {
			    throw new System.ArgumentException ("techniqueProficiency reference could not be created.");
		    }

		    AIAction action = actionPlant.ConstructAction (techniqueProficiency);

		    return action;
	    }

	    /// <summary>
	    /// Assembles the actions.
	    /// </summary>
	    /// <returns>The actions.</returns>
	    /// <param name="">.</param>
	    public static List<AIAction> AssembleActions (ActionPlant actionPlant, List<TechniqueProficiency> techniqueProficiencies)
	    {
		    if (actionPlant == null)
		    {
			    throw new System.ArgumentException ("ActionPlant reference could not be created.");
		    }

		    if (techniqueProficiencies == null)
		    {
			    throw new System.ArgumentException ("techniqueProficiencies reference could not be created.");
		    }

		    if (techniqueProficiencies.Count == 0)
		    {
			    throw new System.ArgumentException ("There are no techniqueProficiencies.");
		    }

		    List<AIAction> actions = new List<AIAction> ();

		    foreach (TechniqueProficiency proficiency in  techniqueProficiencies)
		    {
			    actions.Add (AssembleAction (actionPlant, proficiency));
		    }

		    return actions;
	    }

	    #endregion

    }
}