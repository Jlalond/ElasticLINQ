﻿// Licensed under the Apache 2.0 License. See LICENSE.txt in the project root for more information.

using ElasticLinq.Logging;
using ElasticLinq.Mapping;
using ElasticLinq.Retry;
using ElasticLinq.Utility;
using System.Linq;

namespace ElasticLinq
{
    /// <summary>
    /// Provides an entry point to easily create LINQ queries for ElasticSearch.
    /// </summary>
    public class ElasticContext : IElasticContext
    {
        private readonly ElasticQueryProvider provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ElasticContext"/> class.
        /// </summary>
        /// <param name="connection">The information on how to connect to the ElasticSearch server.</param>
        /// <param name="mapping">The object that helps map queries (optional, defaults to <see cref="TrivialElasticMapping"/>).</param>
        /// <param name="log">The object which logs information (optional, defaults to <see cref="NullLog"/>).</param>
        /// <param name="retryPolicy">The object which controls retry policy for the search (optional, defaults to <see cref="RetryPolicy"/>).</param>
        public ElasticContext(ElasticConnection connection, IElasticMapping mapping = null, ILog log = null, IRetryPolicy retryPolicy = null)
        {
            Argument.EnsureNotNull("connection", connection);

            Connection = connection;
            Mapping = mapping ?? new TrivialElasticMapping();
            Log = log ?? NullLog.Instance;
            RetryPolicy = retryPolicy ?? new RetryPolicy(Log);

            provider = new ElasticQueryProvider(Connection, Mapping, Log, RetryPolicy);
        }

        public ElasticConnection Connection { get; private set; }

        public ILog Log { get; private set; }

        public IElasticMapping Mapping { get; private set; }

        public IRetryPolicy RetryPolicy { get; private set; }

        /// <inheritdoc/>
        public virtual IQueryable<T> Query<T>()
        {
            return new ElasticQuery<T>(provider);
        }
    }
}