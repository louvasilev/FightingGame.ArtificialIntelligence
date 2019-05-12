
using AI.Dependencies;
using AI.Dependencies.Abstractions;
using AI.Support;
using AI.Support.Abstractions;
using AI.UtilityBasedSystem.Abstractions;
using System.Collections.Generic;
using UnityEngine;

namespace AI.UtilityBasedSystem
{
    /// Copyright © 2016-2019 Lou Vasilev - All rights reserved.
    ///
    /// <summary>
    /// The main class in the Utility-Based portion of the
    /// Artificial Inteligence (AI) system. A high-level object
    /// making decisions about what actions the AI controlled character
    /// should take, based on a number of factors and appraisals (evaluation
    /// of possible actions).
    /// </summary>
    /// 
    /// <author>
    /// Lou Vasilev
    /// </author>
    /// <date>
    /// 02-07-2016
    /// </date>
    public static class TacticalMind
    {
        public const float BODY_PART_DAMAGE_COEFFICIENT = 1.5f;
        // The maximum value for a character's physical condition.
        // TODO: each character should have a different value for
        // maximum physical condition. Furthermore, that value can
        // increase as the character trains and so forth. In addition,
        // this should probably come from the core gameplay or
        // information storage systems.
        public const float MAX_PHYSICAL_CONDITION_VALUE = 10.0f;

        private static IIntelligentCharacter _intelligentCharacter;
        private static FightManagerBase _fightManager;
        private static TacticalMindInput _input;
        // A list of all possible actions for the opponent to perform.
        private static List<AIAction> _actions;
        // This will keep track of all the actions performed by the
        // AI controlled character during the fight.
        private static List<TurnAction> _actionsHistory;

        #region Public properties

        /// <summary>
        /// Gets or sets the opponent character.
        /// </summary>
        /// <value>The opponent character.</value>
        public static IIntelligentCharacter Opponent
        {
            get
            {
                return _intelligentCharacter;
            }
            set
            {
                _intelligentCharacter = value;
            }
        }

        /// <summary>
        /// Gets or sets the input for the Utility-Based system.
        /// </summary>
        /// <value>The input, containing data from external systems,
        /// such as the fighting styles of both opponents, the degree
        /// of skill in their respective styles, and so on.</value>
        /// 
        /// TODO: this may not be the best way to inject the TacticalMindInput
        /// dependency into the class. One alternative is to have an
        /// initializion method.
        public static TacticalMindInput Input
        {
            get
            {
                return _input;
            }
            set
            {
                _input = value;
            }
        }

        /// <summary>
        /// Gets the actions history.
        /// </summary>
        /// <value>The actions history.</value>
        public static List<TurnAction> ActionsHistory
        {
            get
            {
                return _actionsHistory;
            }
        }

        #endregion

        #region Private constructors

        /// <summary>
        /// Initializes the <see cref="TacticalMind"/> class.
        /// </summary>
        static TacticalMind()
        {
            _fightManager = new FightManager();
            _actionsHistory = new List<TurnAction>();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Picks actions for the character controlled by the AI.
        /// </summary>
        public static void PickActions()
        {
            ActionPlant actionPlant = new ConcreteActionPlant(null);

            if (actionPlant == null)
            {
                throw new System.ArgumentException("ActionPlant reference could not be created.");
            }

            if (_intelligentCharacter == null)
            {
                throw new System.ArgumentException("IntelligentCharacter reference not found.");
            }

            IntelligentCharacter character = (IntelligentCharacter)_intelligentCharacter;
            Fighter fighter = (Fighter)character.Fighter;

            if (fighter == null)
            {
                throw new System.ArgumentException("Fighter reference not found.");
            }

            if (fighter.Character.TechniqueProficiencies == null)
            {
                throw new System.ArgumentException("TechniqueProficiencies reference not found.");
            }

            _actions = ActionAssembler.AssembleActions(actionPlant, fighter.Character.TechniqueProficiencies);

            for (int i = 1; i <= _input.ActionsPerTurn; i++)
            {
                // Find the action with the highest utility measure,
                // create a corresponding action and add it to the list
                // of actions for the character to perform, and finally,
                // append the action to List '_actionsHistory'.
                AIAction bestAction = evaluateActionUtility(_actions);

                createFightingAction(bestAction);
                createTurnAction(_input.Turn, bestAction);
            }
        } // End PickActions

        #endregion

        #region Private methods

        /// <summary>
        /// Creates a fighting action and adds it to the list of actions
        /// for the character to perform.
        /// </summary>
        /// <param name="action">The Action to create a FightingAction from.</param>
        private static void createFightingAction(AIAction action)
        {
            TechniqueProficiency proficiency = ((FightingTechniqueAction)action).Proficiency;

            FightingAction fightingAction = ScriptableObject.CreateInstance<FightingAction>();
            // TODO: the Utility-Based system should probably not have
            // direct access to StrategicFightingArtsMechanism.
            //fightingAction.ActionFighter = StrategicFightingArtsMechanism.ActiveFighter;
            fightingAction.ActionFighter = _fightManager.ActiveFighter;
            fightingAction.AttackerBodyPart = proficiency.TechniqueBodyPart;
            // TODO: need to have a mechanism for selecting the body part to be
            // targeted by the selected action.
            //fightingAction.TargetBodyPart = BodyPartSearchSupport.FindBodyPartByPartType(proficiency.Technique.TargetBodyParts[0].PartType,
            //    StrategicFightingArtsMechanism.InactiveFighter.Character.BodyParts);
            fightingAction.TargetBodyPart = BodyPartSearchSupport.FindBodyPartByPartType(proficiency.Technique.TargetBodyParts[0].PartType,
                _fightManager.InactiveFighter.Character.BodyParts);
            fightingAction.Technique = proficiency.Technique;

            // Add the fighting action to the queue of actions
            // for the active fighter.
            //StrategicFightingArtsMechanism.AddFightingAction(fightingAction);
            _fightManager.AddFightingAction(fightingAction);
        }

        /// <summary>
        /// Creates the turn action.
        /// </summary>
        /// <param name="turn">Turn.</param>
        /// <param name="action">Action.</param>
        private static void createTurnAction(int turn, AIAction action)
        {
            TurnAction turnAction = ScriptableObject.CreateInstance<TurnAction>();
            turnAction.action = action;
            turnAction.turn = turn;

            _actionsHistory.Add(turnAction);
        }

        /// <summary>
        /// Evaluates the utility measures of the given actions by adding up the basic scores
        /// calculated by all factors (per action). It then picks the action with the highest
        /// score.
        /// </summary>
        /// <param name="actions">Actions.</param>
        /// 
        /// TODO: the blocks of code for initializing factors and getting the
        /// appraisals of actions could probably be abstracted into a separate
        /// method.
        /// 
        /// TODO: this could be done similarly to the Open Closed Principle
        /// example in the PluralSight course aboud the SOLID principles of
        /// Object Oriented Software Design by Steve Smith.
        private static AIAction evaluateActionUtility(List<AIAction> actions)
        {
            float previousScore = 0.0f;
            AIAction bestScoreAction = null;

            foreach (AIAction action in actions)
            {
                float totalScore = 0.0f;
                FactorPlant factorPlant = null;

                factorPlant = new FightingStyleRankFactorPlant("Factor Plant");
                IFactor fightingStyleRankFactor = new FactorAssembler().AssembleFactor(factorPlant);
                // TODO: this assumes that the fighting style rank needed here
                // is always the first one. In the event the character is proficient
                // in more than one styles this could prove problematic.
                // This is also a problem because it creates a dependency of this
                // class on FightingStyleRankFactor.
                ((FightingStyleRankFactor)fightingStyleRankFactor).Rank = _input.OpponentRanks[0];
                Appraisal appraisal = fightingStyleRankFactor.Evaluate(action);

                //DebugConsole.Log ("Score for Fighting Style Rank is: " + appraisal.BasicScore);

                totalScore += appraisal.BasicScore;

                factorPlant = null;

                factorPlant = new TechniqueProficiencyFactorPlant("Factor Plant");
                IFactor techniqueProficiencyFactor = new FactorAssembler().AssembleFactor(factorPlant);
                appraisal = techniqueProficiencyFactor.Evaluate(action);

                //DebugConsole.Log ("Score for Technique Proficiency is: " + appraisal.BasicScore);

                totalScore += appraisal.BasicScore;

                factorPlant = null;

                // Get the action's measure of utility in terms of the character's
                // physical condition.
                factorPlant = new PhysicalConditionFactorPlant("Factor Plant");
                IFactor physicalConditionFactor = new FactorAssembler().AssembleFactor(factorPlant);
                appraisal = physicalConditionFactor.Evaluate(action);

                //DebugConsole.Log ("Score for Physical Condition is: " + appraisal.BasicScore);

                totalScore += appraisal.BasicScore;

                factorPlant = null;

                // Get the action's measure of utility in terms of the character's
                // attacker body part status.
                factorPlant = new AttackerBodyPartFactorPlant("Factor Plant");
                IFactor attackerBodyPartFactor = new FactorAssembler().AssembleFactor(factorPlant);
                appraisal = attackerBodyPartFactor.Evaluate(action);

                //DebugConsole.Log ("Score for Attacker Body Part is: " + appraisal.BasicScore);

                totalScore += appraisal.BasicScore;

                // Get the action's measure of utility in terms of the preceding
                // actions performed by the AI Character.
                factorPlant = new ActionHistoryFactorPlant("Factor Plant");
                IFactor actionHistoryFactor = new FactorAssembler().AssembleFactor(factorPlant);
                // TODO: this is a problem because it creates a dependency of
                // this class on ActionHistoryFactor.
                ((ActionHistoryFactor)actionHistoryFactor).Strategy = _input.Strategy;
                appraisal = actionHistoryFactor.Evaluate(action);

                //DebugConsole.Log ("Score for Action History is: " + appraisal.BasicScore);

                totalScore += appraisal.BasicScore;

                float randomElement = Random.Range(0.0f, 1.0f);

                //DebugConsole.Log ("Random element is: " + randomElement);

                totalScore += randomElement;

                //DebugConsole.Log ("Total score for action " + ((FightingTechniqueAction)action).Proficiency.name + " is: " + totalScore);

                if (totalScore > previousScore)
                {
                    previousScore = totalScore;
                    bestScoreAction = action;
                }

            } // End foreach

            return bestScoreAction;
        } // End evaluateActionUtility

        /// <summary>
        /// Gets the appraisal score.
        /// </summary>
        /// <returns>The appraisal score.</returns>
        /// <param name="factorPlant">Factor plant.</param>
        private static float getAppraisalScore(FactorPlant factorPlant)
        {
            IFactor factor = new FactorAssembler().AssembleFactor(factorPlant);
            Appraisal appraisal = factor.Evaluate(null);

            return appraisal.BasicScore;
        }

        #endregion

    }
}