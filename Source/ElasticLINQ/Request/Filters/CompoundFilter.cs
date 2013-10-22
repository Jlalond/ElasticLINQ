﻿// Copyright (c) Tier 3 Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. 

using System;
using System.Collections.Generic;
using System.Linq;
using ElasticLinq.Utility;

namespace ElasticLinq.Request.Filters
{
    /// <summary>
    /// Base class for any filter wanting to have child filters such
    /// as AndFilter or OrFilter.
    /// </summary>
    internal abstract class CompoundFilter : IFilter
    {
        private readonly List<IFilter> filters;

        protected CompoundFilter(IEnumerable<IFilter> filters)
        {
            Argument.EnsureNotNull("filters", filters);

            this.filters = new List<IFilter>(filters);
        }

        public abstract string Name { get; }

        public IReadOnlyList<IFilter> Filters
        {
            get { return filters.AsReadOnly(); }
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", Name, String.Join(", ", Filters.Select(f => f.ToString()).ToArray()));
        }
    }
}