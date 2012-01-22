﻿/*
   Copyright 2011 Michael Edwards
 
   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;

namespace Glass.Sitecore.Mapper.Configuration.Fluent
{
    /// <summary>
    /// Used to populate the property with data from a Sitecore field
    /// </summary>
	public class SitecoreField<T>: AbstractSitecoreAttributeBuilder<T>
	{
        Configuration.Attributes.SitecoreFieldAttribute _attr;

        public SitecoreField(Expression<Func<T, object>> ex)
            : base(ex)
        {
            _attr = new Configuration.Attributes.SitecoreFieldAttribute(); 
        }

        /// <summary>
        /// Indicate that the field can not be written to Sitecore
        /// </summary>
        public SitecoreField<T> ReadOnly()
        {
            _attr.ReadOnly = true;
            return this;
        }
        /// <summary>
        /// The name of the field  to use if it is different to the property name
        /// </summary>
        public SitecoreField<T> FieldName(string name)
        {
            _attr.FieldName = name;
            return this;
        }
        /// <summary>
        /// Options to override the behaviour of certain fields.
        /// </summary>
        public SitecoreField<T> Setting(SitecoreFieldSettings setting)
        {
            _attr.Setting = setting;
            return this;
        }



        internal override Glass.Sitecore.Mapper.Configuration.Attributes.AbstractSitecorePropertyAttribute Attribute
        {
            get { return _attr; }
        }

    }
}
