using System;

namespace Gremlin.Net.CosmosDb.Structure
{
    /// <summary>
    /// A combination of partition key and id, used for instant lookup for partitioned vertices.
    /// </summary>
    public class PartitionKeyIdPair
    {
        /// <summary>
        /// The id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The partition key.
        /// </summary>
        public string PartitionKey { get; set; }

        /// <summary>
        /// Creates a new partition key / id pair.
        /// </summary>
        /// <param name="partitionKey">The partition key.</param>
        /// <param name="id">The id.</param>
        public PartitionKeyIdPair(string partitionKey, string id)
        {
            PartitionKey = partitionKey;
            Id = id;
        }

        /// <summary>
        /// Implicitly convert <see cref="Tuple"/> to <see cref="PartitionKeyIdPair"/>
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        public static implicit operator PartitionKeyIdPair(Tuple<string, string> tuple)
        {
            return new PartitionKeyIdPair(tuple.Item1, tuple.Item2);
        }

        /// <summary>
        /// Implicitly convert <see cref="ValueTuple"/> to <see cref="PartitionKeyIdPair"/>
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        public static implicit operator PartitionKeyIdPair((string, string) tuple)
        {
            return new PartitionKeyIdPair(tuple.Item1, tuple.Item2);
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString() => $"[\"{PartitionKey}\", \"{Id}\"]";
    }
}