﻿// -----------------------------------------------------------------------
// <copyright file="SqlServerCeCharacters.cs" company="MicroLite">
// Copyright 2012 - 2016 Project Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// </copyright>
// -----------------------------------------------------------------------
namespace MicroLite.Characters
{
    /// <summary>
    /// The implementation of <see cref="SqlCharacters"/> for SQL Server Compact Edition.
    /// </summary>
    [System.Serializable]
    internal sealed class SqlServerCeCharacters : SqlCharacters
    {
        /// <summary>
        /// The single instance of <see cref="SqlCharacters"/> for SQL Server Compact Edition.
        /// </summary>
        internal static readonly SqlCharacters Instance = new SqlServerCeCharacters();

        /// <summary>
        /// Prevents a default instance of the <see cref="SqlServerCeCharacters"/> class from being created.
        /// </summary>
        private SqlServerCeCharacters()
        {
        }

        /// <summary>
        /// Gets the left delimiter character.
        /// </summary>
        public override string LeftDelimiter
        {
            get
            {
                return "\"";
            }
        }

        /// <summary>
        /// Gets the right delimiter character.
        /// </summary>
        public override string RightDelimiter
        {
            get
            {
                return "\"";
            }
        }

        /// <summary>
        /// Gets the SQL parameter.
        /// </summary>
        public override string SqlParameter
        {
            get
            {
                return "@";
            }
        }

        /// <summary>
        /// Gets a value indicating whether SQL parameters are named.
        /// </summary>
        public override bool SupportsNamedParameters
        {
            get
            {
                return true;
            }
        }
    }
}