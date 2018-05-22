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
                gameEntities.SDOs.assets = pickAssets(numberOfAssets);

                // Attack pattern -> targets -> Asset relationships
                foreach (var asset in gameEntities.SDOs.assets) {
                    relationships.AddRange(
                        State.I.Entities.SROs.relationships
                             .Where((r) => r.target_ref.Type == StixType.Asset && asset.id == r.target_ref)
                             .Select((r) => r.WithReferenceToParentAsset(asset.id)));
                }

                // Attack patterns
                gameEntities.SDOs.attack_patterns = pickAttackPatterns(gameEntities.SDOs.assets);

                // Course of action -> mitigates -> Attack pattern relationships
                foreach (var attackPattern in gameEntities.SDOs.attack_patterns) {
                    relationships.AddRange(
                        State.I.Entities.SROs.relationships.Where(
                            (r) => r.source_ref.Type == StixType.CourseOfAction
                                   && attackPattern.id == r.target_ref));
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

        private static Asset[] pickAssets(int numberOfAssets) {
            var assets = new List<Asset>(numberOfAssets);

            while (assets.Count < numberOfAssets) {
                assets.Add(State.I.Entities.SDOs.assets.TakeRandom());
                assets = assets.Distinct().DistinctBy((a) => a.FullDescription.ToLower()).ToList();
            }

            return assets.ToArray();
        }

        private static AttackPattern[] pickAttackPatterns(IEnumerable<Asset> assets) {
            var attackPatterns = new List<AttackPattern>();
            foreach (var asset in assets) {
                Asset assetCopy = asset;

                AssetCategory targetedCategory = assetCopy.custom.category;

                var fittingAttackPatterns = State.I.Entities.SDOs.attack_patterns
                                                 .Where(
                                                     (aP) => aP.custom.activation_zone.categories != null
                                                             && aP.custom.activation_zone.categories.Contains(
                                                                 targetedCategory))
                                                 .Select((aP) => aP.WithReferenceToParentAsset(assetCopy.id));

                attackPatterns.AddRange(fittingAttackPatterns);
                attackPatterns = attackPatterns.Distinct().ToList();
            }

            return attackPatterns.ToArray();
        }

        private static CourseOfAction[] pickCourseOfActions(IEnumerable<Relationship> relationships) {
            Relationship[] courseOfActionRelationships =
                relationships.Where((r) => r.relationship_type == StixRelationshipType.Mitigates).ToArray();

            CourseOfAction[] allCourseOfActions = courseOfActionRelationships
                                                  .Select(
                                                      (r) => State.I.Entities.SDOs.course_of_actions.First(
                                                          (c) => c.id == r.source_ref))
                                                  .ToArray();

            /**
             * TODO Should be improved
             * This makes course of actions quite messy, and they should be distincy by category, but this must be
             * reflected in mitigation and attack behaviours.
             */
            return allCourseOfActions /*.DistinctBy((c) => c.custom.category)*/.ToArray();
        }

    }

}
