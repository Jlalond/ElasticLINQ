﻿// Copyright (c) Tier 3 Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. 

using ElasticLinq.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElasticLinq.Request.Filters
{
    /// <summary>
    /// Filter that specifies one or more possible values that a
    /// field must match in order to select a document.
    /// </summary>
    internal class TermFilter : IFilter
    {
        private readonly string field;
        private readonly HashSet<object> values;

        public static TermFilter FromIEnumerable(string field, IEnumerable<object> values)
        {
            return new TermFilter(field, values);
        }

        public TermFilter(string field, params object[] values)
            : this(field, values.AsEnumerable())
        {
        }

        private TermFilter(string field, IEnumerable<object> values)
        {
            Argument.EnsureNotNull("value", values);
            this.field = field;
            this.values = new HashSet<object>(values);            
        }

        public string Field
        {
            get { return field; }
        }

        public IReadOnlyList<Object> Values
        {
            get { return values.ToList().AsReadOnly(); }
        }

        public string Name
        {
            get { return Values.Count == 1 ? "term" : "terms"; }
        }

        public override string ToString()
        {
            return String.Format("{0} [{1}]", Name, String.Join(",", values.ToArray()));
        }
    }
}