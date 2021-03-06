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
using NUnit.Framework;
using Sitecore.Data;
using Sitecore.Data.Items;
using Glass.Sitecore.Mapper.Data.QueryParameters;

namespace Glass.Sitecore.Mapper.Tests.Data.QueryParameters
{
    [TestFixture]
    public class ItemIdNoBracketsParameterFixture
    {
        Database _db;
        Item _item;

        [SetUp]
        public void Setup()
        {
            _db = global::Sitecore.Configuration.Factory.GetDatabase("master");
            _item = _db.GetItem("/sitecore/content/glass/test1");
        }

        #region GetValue
          
        [Test]
        public void GetValue_ReturnsItemIdAsString()
        {
            //Assign
            ItemIdNoBracketsParameter param = new ItemIdNoBracketsParameter();

            //Act
            var result = param.GetValue(_item);

            //Assert
            string idResult = _item.ID.Guid.ToString("N");
            Assert.AreEqual(idResult, result);
            Guid idGuid;
            Assert.IsTrue(result.GuidTryParse(out idGuid));
        }


        #endregion
    }
}
