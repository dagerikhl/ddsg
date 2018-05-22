using System;
using System.Collections.Generic;
using System.Linq;

namespace DdSG {

    public static class StixDataEntityExtensions {

        public static bool RelatedAsSourceTo(this StixDataEntityBase entity, StixDataEntityBase otherEntity) {
            IEnumerable<Relationship> relationships = entity.GetRelationships(true, false);
            return relationships.Any((r) => r.target_ref == otherEntity.id);
        }

        public static bool RelatedAsTargetTo(this StixDataEntityBase entity, StixDataEntityBase otherEntity) {
            IEnumerable<Relationship> relationships = entity.GetRelationships(false);
            return relationships.Any((r) => r.source_ref == otherEntity.id);
        }

        /// <summary>
        /// Gets all relationships from the game state that relate to this <see cref="StixDataEntityBase"/>.
        /// </summary>
        /// <param name="entity">The <see cref="StixDataEntityBase"/> to get relationships for.</param>
        /// <param name="whereSource">Include relationships where the <see cref="StixDataEntityBase"/> is the source.</param>
        /// <param name="whereTarget">Include relationships where the <see cref="StixDataEntityBase"/> is the target.</param>
        /// <returns>Relationships related to this <see cref="StixDataEntityBase"/> in the current game state.</returns>
        /// <exception cref="NullReferenceException">Throws an exception if the state hasn't been initialized.</exception>
        public static Relationship[] GetRelationships(
            this StixDataEntityBase entity,
            bool whereSource = true,
            bool whereTarget = true) {
            if (State.I.GameEntities == null) {
                throw new NullReferenceException("The state has not been initialized with game entities yet.");
            }

            return State.I.GameEntities.SROs.relationships.Where(
                            (r) => whereSource && r.source_ref == entity.id
                                   || whereTarget && r.target_ref == entity.id)
                        .ToArray();
        }

    }

}
