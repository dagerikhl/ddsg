using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DdSG {

    public static class EntitiesPicker {

        public static void PickGameEntities() {
            var gameEntities = new Entities { SDOs = new StixDataObjects(), SROs = new StixRelationshipObjects() };
            var relationships = new List<Relationship>();

            // To ensure that we are able to create entities should the random draw be extremely unlucky or the data bad
            const int maxAttempts = 100;
            var attempts = 0;
            while (attempts < maxAttempts
                   && (gameEntities.SDOs.assets == null
                       || gameEntities.SDOs.attack_patterns == null
                       || gameEntities.SDOs.course_of_actions == null
                       || !relationships.Any())) {
                attempts++;
                gameEntities = new Entities { SDOs = new StixDataObjects(), SROs = new StixRelationshipObjects() };
                relationships = new List<Relationship>();

                // Assets
                var numberOfAssets = Rnd.Gen.Next(2, 5);
                PlayerStats.I.NumberOfAssets = numberOfAssets;

                gameEntities.SDOs.assets = State.I.Entities.SDOs.assets.TakeRandoms(numberOfAssets).ToArray();

                // Attack pattern -> targets -> Asset relationships
                foreach (var asset in gameEntities.SDOs.assets) {
                    relationships.AddRange(
                        State.I.Entities.SROs.relationships.Where(
                            (r) => r.target_ref.Type == StixType.Asset
                                   && string.Equals(asset.id.Id, r.target_ref.Id)));
                }

                // Attack patterns
                gameEntities.SDOs.attack_patterns = pickAttackPatterns(gameEntities.SDOs.assets);

                // Course of action -> mitigates -> Attack pattern relationships
                foreach (var attackPattern in gameEntities.SDOs.attack_patterns) {
                    relationships.AddRange(
                        State.I.Entities.SROs.relationships.Where(
                            (r) => r.source_ref.Type == StixType.CourseOfAction
                                   && string.Equals(attackPattern.id.Id, r.target_ref.Id)));
                }

                // Course of actions
                gameEntities.SDOs.course_of_actions = pickCourseOfActions(relationships);
            }

            if (attempts == maxAttempts) {
                Logger.Error("STIX and Stones has not been able to generate entities and has crashed.");
                Application.Quit();
            }

            // Add relationships
            gameEntities.SROs.relationships = relationships.ToArray();

            if (Debug.isDebugBuild) {
                Logger.Debug("Attempts: " + attempts);

                Logger.Debug("Assets: " + gameEntities.SDOs.assets.Length);
                // foreach (var e in gameEntities.SDOs.assets) {
                //     Logger.Debug(e);
                // }
                Logger.Debug("Attack patterns: " + gameEntities.SDOs.attack_patterns.Length);
                // foreach (var e in gameEntities.SDOs.attack_patterns) {
                //     Logger.Debug(e);
                //     Logger.Debug(" -> Targets: " + e.TargetsAssetCategories.Join(", "));
                // }
                Logger.Debug("Course of actions: " + gameEntities.SDOs.course_of_actions.Length);
                // foreach (var e in gameEntities.SDOs.course_of_actions) {
                //     Logger.Debug(e);
                // }

                Logger.Debug("Relationships: " + relationships.Count);
                // foreach (var e in relationships) {
                //     Logger.Debug(e);
                // }
            }

            // Store game entities
            State.I.GameEntities = gameEntities;
        }

        private static AttackPattern[] pickAttackPatterns(IEnumerable<Asset> assets) {
            AssetCategory[] gameTargetedCategories = assets.Select((a) => a.custom.category).Distinct().ToArray();

            IEnumerable<AttackPattern> attackPatterns = State.I.Entities.SDOs.attack_patterns.Where(
                (aP) => aP.custom.activation_zone.categories != null
                        && aP.custom.activation_zone.categories.Any(
                            (aZ) => gameTargetedCategories
                                .Contains(aZ)));

            return attackPatterns.ToArray();
        }

        private static CourseOfAction[] pickCourseOfActions(IEnumerable<Relationship> relationships) {
            Relationship[] courseOfActionRelationships =
                relationships.Where((r) => r.relationship_type == StixRelationshipType.Mitigates).ToArray();

            return courseOfActionRelationships.Select(
                                                  (r) => State.I.Entities.SDOs.course_of_actions.First(
                                                      (c) => string.Equals(c.id.Id, r.source_ref.Id)))
                                              .ToArray();
        }

    }

}
