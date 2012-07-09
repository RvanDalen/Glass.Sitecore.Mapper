﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glass.Sitecore.Mapper.ObjectCreation.Implementations
{
    /// <summary>
    /// 
    /// </summary>
    public class ClassManager : ObjectManager, IObjectManager
    {
        /// <summary>
        /// </summary>
        /// <param name="service"></param>
        /// <param name="isLazy"></param>
        /// <param name="inferType"></param>
        /// <param name="type"></param>
        /// <param name="item"></param>
        /// <param name="constructorParameters"></param>
        /// <returns></returns>
        public override object CreateClass(ISitecoreService service, bool isLazy, bool inferType, Type type, global::Sitecore.Data.Items.Item item, params object[] constructorParameters)
        {
            return CreateObject(service, isLazy, inferType, type, item, constructorParameters);
        }

        public override bool AddRelatedCache(string key, string group, object o, Type type)
        {
            return true;
        }
    }
}
