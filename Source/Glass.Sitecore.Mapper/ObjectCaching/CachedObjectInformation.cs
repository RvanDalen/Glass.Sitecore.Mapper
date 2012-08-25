﻿using System;
using Sitecore.Data.Items;


namespace Glass.Sitecore.Mapper.ObjectCaching
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class CachedObjectInformation
    {
        /// <summary>
        /// Gets or sets the item ID.
        /// </summary>
        /// <value>
        /// The item ID.
        /// </value>
        public Guid ItemID { get; set; }

        /// <summary>
        /// Gets or sets the revision ID.
        /// </summary>
        /// <value>
        /// The revision ID.
        /// </value>
        public string RevisionID { get; set; }

        /// <summary>
        /// Gets or sets the template ID.
        /// </summary>
        /// <value>
        /// The template ID.
        /// </value>
        public Guid TemplateID { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the type of the target.
        /// </summary>
        /// <value>
        /// The type of the target.
        /// </value>
        public string TargetType { get; set; }

        /// <summary>
        /// Gets or sets the database.
        /// </summary>
        /// <value>
        /// The database.
        /// </value>
        public string Database { get; set; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public object Key { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public int Version { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public Type Type { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CachedObjectInformation"/> class.
        /// </summary>
        public CachedObjectInformation()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CachedObjectInformation"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="type">The type.</param>
        public CachedObjectInformation(Item item, Type type)
        {
            ItemID = item.ID.Guid;
            TemplateID = item.TemplateID.Guid;
            Language = item.Language.Name;
            Database = item.Database.Name;
            Version = item.Version.Number;
            Type = type;
            //not sure what this is??
            //TargetType 

            //How do i get the RevisionID??  
            //need to ref the field name
            RevisionID = item.Fields["__Revision"].Value;
        }
    }
}