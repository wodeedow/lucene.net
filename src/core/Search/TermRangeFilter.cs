/* 
 * Licensed to the Apache Software Foundation (ASF) under one or more
 * contributor license agreements.  See the NOTICE file distributed with
 * this work for additional information regarding copyright ownership.
 * The ASF licenses this file to You under the Apache License, Version 2.0
 * (the "License"); you may not use this file except in compliance with
 * the License.  You may obtain a copy of the License at
 * 
 * http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Lucene.Net.Util;
using System;
using System.Globalization;

namespace Lucene.Net.Search
{
	
	/// <summary> A Filter that restricts search results to a range of values in a given
	/// field.
	/// 
	/// <p/>This filter matches the documents looking for terms that fall into the
	/// supplied range according to <see cref="String.CompareTo(String)" />. It is not intended
	/// for numerical ranges, use <see cref="NumericRangeFilter{T}" /> instead.
	/// 
	/// <p/>If you construct a large number of range filters with different ranges but on the 
	/// same field, <see cref="FieldCacheRangeFilter" /> may have significantly better performance. 
	/// </summary>
	/// <since> 2.9
	/// </since>
	[Serializable]
	public class TermRangeFilter : MultiTermQueryWrapperFilter<TermRangeQuery>
	{
		
		/// <param name="fieldName">The field this range applies to
		/// </param>
		/// <param name="lowerTerm">The lower bound on this range
		/// </param>
		/// <param name="upperTerm">The upper bound on this range
		/// </param>
		/// <param name="includeLower">Does this range include the lower bound?
		/// </param>
		/// <param name="includeUpper">Does this range include the upper bound?
		/// </param>
		/// <throws>  IllegalArgumentException if both terms are null or if </throws>
		/// <summary>  lowerTerm is null and includeLower is true (similar for upperTerm
		/// and includeUpper)
		/// </summary>
        public TermRangeFilter(string fieldName, BytesRef lowerTerm, BytesRef upperTerm, bool includeLower, bool includeUpper)
            : base(new TermRangeQuery(fieldName, lowerTerm, upperTerm, includeLower, includeUpper))
		{
		}

        public static TermRangeFilter NewStringRange(string field, string lowerTerm, string upperTerm, bool includeLower, bool includeUpper)
        {
            var lower = lowerTerm == null ? null : new BytesRef(lowerTerm);
            var upper = upperTerm == null ? null : new BytesRef(upperTerm);
            return new TermRangeFilter(field, lower, upper, includeLower, includeUpper);
        }
		
		/// <summary> Constructs a filter for field <c>fieldName</c> matching
		/// less than or equal to <c>upperTerm</c>.
		/// </summary>
        public static TermRangeFilter Less(string fieldName, BytesRef upperTerm)
		{
			return new TermRangeFilter(fieldName, null, upperTerm, false, true);
		}
		
		/// <summary> Constructs a filter for field <c>fieldName</c> matching
		/// greater than or equal to <c>lowerTerm</c>.
		/// </summary>
        public static TermRangeFilter More(string fieldName, BytesRef lowerTerm)
		{
			return new TermRangeFilter(fieldName, lowerTerm, null, true, false);
		}

	    /// <summary>Returns the lower value of this range filter </summary>
	    public virtual string LowerTerm
	    {
	        get { return query.LowerTerm; }
	    }

	    /// <summary>Returns the upper value of this range filter </summary>
	    public virtual string UpperTerm
	    {
	        get { return query.UpperTerm; }
	    }

	    /// <summary>Returns <c>true</c> if the lower endpoint is inclusive </summary>
	    public virtual bool IncludesLower
	    {
	        get { return query.IncludesLower; }
	    }

	    /// <summary>Returns <c>true</c> if the upper endpoint is inclusive </summary>
	    public virtual bool IncludesUpper
	    {
	        get { return query.IncludesUpper; }
	    }
	}
}