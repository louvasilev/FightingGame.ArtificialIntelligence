using AI.Dependencies.Enumerations;
using AI.Support;
using AI.Support.Abstractions;
using AI.UtilityBasedSystem.Abstractions;
using System;

namespace AI.UtilityBasedSystem
{
    /// Copyright © 2016-2019 Lou Vasilev - All rights reserved.
    ///
    /// <summary>
    /// A Factor for evaluating an action based on the preceeding
    /// actions picked by the AI during the fight.
    /// </summary>
    /// 
    /// <author>
    /// Lou Vasilev
    /// </author>
    /// <date>
    /// 06-02-2016
    /// </date>
    public class ActionHistoryFactor : IFactor
    {
	    private float _initialScore;
	    private float _maxModifier;
	    private StrategyType _strategyType;

	    #region Public properties

	    /// <summary>
	    /// Gets or sets the strategy type for the AI character.
	    /// </summary>
	    /// <value>The strategy type.</value>
	    public StrategyType Strategy
	    {
		    get
		    {
			    return _strategyType;
		    }
		    set
		    {
			    _strategyType = value;
		    }
	    }

	    #endregion

	    #region Public constructors

	    /// <summary>
	    /// Initializes a new instance of the <see cref="ActionHistoryFactor"/> class.
	    /// </summary>
	    public ActionHistoryFactor()
	    {
		    _initialScore = 0.5f;
		    _maxModifier = 0.5f;
		    _strategyType = StrategyType.None;
	    }

	    #endregion

	    #region Public methods

	    /// <summary>
	    /// Evaluate the specified action. If the Strategy Type is “PracticeTechniques”,
	    /// the AI character should favor techniques which have not been used in the
	    /// last several turns or actions (within the current turn). The initial score
	    /// for the action would be 0.5 and then for each turn the action has not been
	    /// used in, a modifier equal to the number of turns divided by 10, not to
	    /// exceed 0.5, would be added.
	    /// 
	    /// If the Strategy Type is “WinTheFight”, the formula will be similar to the one
	    /// above, with the only difference that the number of turns will be divided by
	    /// 100 instead of 10.
	    /// </summary>
	    /// <param name="action">AIAction.</param>
	    public Appraisal Evaluate (AIAction action)
	    {
		    if (action == null)
		    {
			    throw new ArgumentNullException ("Action argument cannot be null.");
		    }

		    // TODO: as more strategies are added, the approach below
		    // would become less desired. It might be better to extract
		    // the logic in some kind of a class hierarchy.
		    int divider = 0;
		    if (_strategyType == StrategyType.PracticeTechniques)
		    {
			    // TODO: this should be a constant
			    divider = 10;
		    }
		    else if (_strategyType == StrategyType.WinTheFight)
		    {
			    // TODO: this should be a constant
			    divider = 100;
		    }

		    Appraisal appraisal = new Appraisal ();
		    // Obtain the number of turns since the last time the given
		    // action was used and then calculate the basic score, for
		    // that action.
		    int turnsSinceLastUsed = ActionSupport.TurnsSinceLastUse (action, TacticalMind.ActionsHistory);
		    float modifier = ((float)turnsSinceLastUsed) / ((float)divider);

		    if (modifier > _maxModifier)
		    {
			    modifier = _maxModifier;
		    }

		    appraisal.BasicScore = _initialScore + modifier;

		    return appraisal;
	    }

	    #endregion

    }
}