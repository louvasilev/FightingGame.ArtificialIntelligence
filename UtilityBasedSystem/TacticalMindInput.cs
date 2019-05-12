
using AI.Dependencies.Abstractions;
using AI.Dependencies.Enumerations;
using System.Collections.Generic;

namespace AI.UtilityBasedSystem
{
    /// Copyright © 2016-2019 Lou Vasilev - All rights reserved.
    ///
    /// <summary>
    /// Comprises all of the game objects representing Factors, i.e.
    /// considerations that the Utility-Based system uses to make
    /// decisions about what actions the AI controlled character
    /// should perform next.
    /// 
    /// It should be sent to the Utility-Based system from external
    /// systems, such as the main AI system, the core gameplay system,
    /// etc.
    /// </summary>
    /// 
    /// <author>
    /// Lou Vasilev
    /// </author>
    /// <date>
    /// 02-27-2016
    /// </date>
    /// 
    /// TODO: this may need to implement an interface to make it more
    /// reusable.
    public class TacticalMindInput : SystemInput
    {
        // How many actions can the character perform during
        // the current turn.
        int _actionsPerTurn;
        PersonalityType _mainCharacterPersonality;
        PersonalityType _opponentPersonality;
        StrategyType _opponentStrategy;
        List<FightingStyleRank> _mainCharacterRanks;
        List<FightingStyleRank> _opponentRanks;
        List<ICondition> _fighterConditions;

        #region Public properties

        /// <summary>
        /// Gets the actions per turn.
        /// </summary>
        /// <value>The actions per turn.</value>
        public int ActionsPerTurn
        {
            get
            {
                return _actionsPerTurn;
            }
        }

        /// <summary>
        /// Gets the fighter conditions.
        /// </summary>
        /// <value>The fighter conditions.</value>
        public List<ICondition> FighterConditions
        {
            get
            {
                return _fighterConditions;
            }
        }

        /// <summary>
        /// Gets the opponent fighting style ranks.
        /// </summary>
        /// <value>The opponent ranks.</value>
        public List<FightingStyleRank> OpponentRanks
        {
            get
            {
                return _opponentRanks;
            }
        }

        /// <summary>
        /// Gets the strategy type of the AI Character.
        /// </summary>
        /// <value>The strategy type.</value>
        public StrategyType Strategy
        {
            get
            {
                return _opponentStrategy;
            }
        }

        #endregion

        #region Public constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TacticalMindInput"/> class.
        /// </summary>
        /// <param name="mainCharacterPersonality">Main character personality.</param>
        /// <param name="opponentPersonality">Opponent personality.</param>
        /// <param name="opponentStrategy">Opponent strategy.</param>
        /// <param name="mainCharacterRanks">Main character ranks.</param>
        /// <param name="opponentRanks">Opponent ranks.</param>
        /// <param name="fighterConditions">Fighter conditions.</param>
        public TacticalMindInput(int actionsPerTurn,
                                  int turn,
                                  PersonalityType mainCharacterPersonality,
                                  PersonalityType opponentPersonality,
                                  StrategyType opponentStrategy,
                                  List<FightingStyleRank> mainCharacterRanks,
                                  List<FightingStyleRank> opponentRanks,
                                  List<ICondition> fighterConditions)
        {
            _actionsPerTurn = actionsPerTurn;
            _turn = turn;
            _mainCharacterPersonality = mainCharacterPersonality;
            _opponentPersonality = opponentPersonality;
            _opponentStrategy = opponentStrategy;
            _mainCharacterRanks = mainCharacterRanks;
            _opponentRanks = opponentRanks;
            _fighterConditions = fighterConditions;
        }

        #endregion
    }
}