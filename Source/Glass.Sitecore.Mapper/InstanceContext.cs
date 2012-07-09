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
using Glass.Sitecore.Mapper.Configuration;
using Sitecore.Data.Items;
using System.Reflection;
using Glass.Sitecore.Mapper.Data;
using Glass.Sitecore.Mapper.Configuration.Attributes;
using System.Collections;
using Glass.Sitecore.Mapper.Proxies;
using Glass.Sitecore.Mapper.ObjectCreation;
using Diagnostics = global::Sitecore.Diagnostics;

namespace Glass.Sitecore.Mapper
{
    public class InstanceContext : ICloneable
    {
        private IObjectManager _manager;

        public Dictionary<Guid, IList <SitecoreClassConfig>> ClassesById { get; private set; }
        public Dictionary<Type, SitecoreClassConfig> Classes { get; private set; }
        public IEnumerable<AbstractSitecoreDataHandler> Datas { get; private set; }


        /// <summary>
        /// this is an overloaded constructor if we want to use dependency inject for the ObjectManager
        /// </summary>
        /// <param name="classes"></param>
        /// <param name="datas"></param>
        /// <param name="objectManager"></param>
        public InstanceContext(Dictionary<Type, SitecoreClassConfig> classes, IEnumerable<AbstractSitecoreDataHandler> datas, IObjectManager objectManager)
        {
            Diagnostics.Assert.IsNotNull(objectManager, "objectManager can not be null");

            _manager = objectManager;
            CommonSetup(classes, datas);
        }

        /// <summary>
        /// normal constructor that gets the ObjectManager form a the factory
        /// </summary>
        /// <param name="classes"></param>
        /// <param name="datas"></param>

        public InstanceContext(Dictionary<Type, SitecoreClassConfig> classes, IEnumerable<AbstractSitecoreDataHandler> datas)
        {
            _manager = ObjectManagerFactory.Create();
            CommonSetup(classes, datas);
        }


        /// <summary>
        /// Moved the common code here for the two constructors
        /// </summary>
        /// <param name="classes"></param>
        /// <param name="datas"></param>
        private void CommonSetup(Dictionary<Type, SitecoreClassConfig> classes, IEnumerable<AbstractSitecoreDataHandler> datas)
        {
            //This needs reworking
            //this will be simplified to remove the need for three sets of data

            Classes = classes;
            ClassesById = new Dictionary<Guid, IList<SitecoreClassConfig>>();
            foreach (var record in classes.Where(x => x.Value.TemplateId != Guid.Empty))
            {
                if (!ClassesById.ContainsKey(record.Value.TemplateId))
                {
                    ClassesById.Add(record.Value.TemplateId, new List<SitecoreClassConfig>());
                }

                ClassesById[record.Value.TemplateId].Add(record.Value);
            }

            Datas = datas;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="property"></param>
        public AbstractSitecoreDataHandler GetDataHandler(SitecoreProperty property)
        {
            AbstractSitecoreDataHandler handler = Datas.FirstOrDefault(x => x.WillHandle(property, Datas, Classes));

            if (handler == null)
                throw new NotSupportedException("No data handler for: \n\r Class: {0} \n\r Member: {1} \n\r Attribute: {2}"
                    .Formatted(
                        property.Property.ReflectedType.FullName,
                        property.Property.Name,
                        property.Attribute.GetType().FullName
                    ));

            var newHandler = handler.Clone() as AbstractSitecoreDataHandler;
            newHandler.ConfigureDataHandler(property);

            return newHandler;
        }



        public Guid GetClassId(Type type, object target)
        {
            var scClass = GetSitecoreClass(type);
            
            if (scClass.IdProperty == null)
                throw new SitecoreIdException("The type {0} does not contain a property with the Glass.Sitecore.Mapper.Configuration.Attributes.SitecoreIdAttribute".Formatted(type.FullName));

            Guid guid = (Guid)scClass.IdProperty.Property.GetValue(target, null);
            return guid;
        }


        public SitecoreClassConfig GetSitecoreClass(Type type)
        {
            if (!Classes.ContainsKey(type) || Classes[type] == null)
                throw new MapperException("Type {0} has not been loaded".Formatted(type.FullName));

            return Classes[type];
        }

        public SitecoreClassConfig GetSitecoreClass(Guid templateId, Type type)
        {
            string id = templateId.ToString();
            //would it be quicker to have a second dictionary that recorded classes by their template ID?
            if (ClassesById.ContainsKey(templateId) && ClassesById[templateId] != null)
            {
                var types = ClassesById[templateId];
                if (types.Count == 1) return types.First();
                else
                {
                    return types.First(x => type.IsAssignableFrom(x.Type));
                }
                
            }
            else return null;
        }

        /// <summary>
        /// 
        /// </summary>
        public IObjectManager ObjectManager { get { return _manager; }  }//set { _manager = value; }


        #region ICloneable Members

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}
