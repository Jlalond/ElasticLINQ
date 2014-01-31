﻿// Licensed under the Apache 2.0 License. See LICENSE.txt in the project root for more information.

using ElasticLinq.Request.Criteria;

namespace ElasticLinq.Request.Facets
{
    internal class TermsStatsFacet : IOrderableFacet
    {
        private readonly string name;
        private readonly ICriteria criteria;
        private readonly string key;
        private readonly string value;
        private readonly int? size;

        public TermsStatsFacet(string name, string key, string value, int? size)
            : this(name, null, key, value)
        {
            this.size = size;
        }

        public TermsStatsFacet(string name, ICriteria criteria, string key, string value)
        {
            this.name = name;
            this.criteria = criteria;
            this.key = key;
            this.value = value;
        }

        public string Type { get { return "terms_stats"; } }
        public string Name { get { return name; } }
        public ICriteria Filter { get { return criteria; } }
        public string Key { get { return key; } }
        public string Value { get { return value; } }
        public int? Size { get { return size; } }
    }
}