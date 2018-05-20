using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DdSG {

    public static class CourseOfActionExtensions {

        // TODO This is fairly random, but made so because of missing data in CourseOfAction and to be consistent1
        public static int GetValue(this CourseOfAction courseOfAction) {
            var categoryValue = courseOfAction.custom.category == null ? 10 : courseOfAction.custom.category.Length;
            var mitigationValue =
                courseOfAction.custom.mitigation == null ? 15 : courseOfAction.custom.mitigation.Length;
            var referencesValue = courseOfAction.external_references == null ? 20
                : courseOfAction.external_references.Length;

            return Mathf.RoundToInt((categoryValue + mitigationValue + referencesValue)/2f/10f)*5;
        }

        public static bool RelatedTo(this CourseOfAction courseOfAction, StixDataEntityBase entity) {
            IEnumerable<Relationship> mitigationRelationships = courseOfAction.getRelationships(true, false);
            return mitigationRelationships.Any((r) => string.Equals(r.target_ref.Id, entity.id.Id));
        }

        /// <summary>
        /// Gets all relationships from the game state that relate to this <see cref="CourseOfAction"/>.
        /// </summary>
        /// <param name="courseOfAction">The <see cref="CourseOfAction"/> to get relationships for.</param>
        /// <param name="whereSource">Include relationships where the <see cref="CourseOfAction"/> is the source.</param>
        /// <param name="whereTarget">Include relationships where the <see cref="CourseOfAction"/> is the target.</param>
        /// <returns>Relationships related to this <see cref="CourseOfAction"/> in the current game state.</returns>
        /// <exception cref="NullReferenceException"></exception>
        private static IEnumerable<Relationship> getRelationships(
            this CourseOfAction courseOfAction,
            bool whereSource = true,
            bool whereTarget = true) {
            if (State.I.GameEntities == null) {
                throw new NullReferenceException("The state has not been initialized with game entities yet.");
            }

            return State.I.GameEntities.SROs.relationships.Where(
                            (r) => whereSource && string.Equals(r.source_ref.Id, courseOfAction.id.Id)
                                   || whereTarget && string.Equals(r.target_ref.Id, courseOfAction.id.Id))
                        .ToArray();
        }

    }

}
