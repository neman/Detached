﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Detached.Mvc.Metadata
{
    public class Pattern
    {
        #region Fields

        Regex _regex;
        IDictionary<string, string> _defaults;

        #endregion

        #region Ctor.

        public Pattern(string regexPattern)
            : this(regexPattern, new Dictionary<string, string>())
        { 
        }

        public Pattern(string regexPattern, IDictionary<string, string> defaults)
        {
            _regex = new Regex(regexPattern, RegexOptions.IgnoreCase);
            _defaults = defaults;
        }

        #endregion

        public bool TryGetMetadata(string key, out Dictionary<string, string> result)
        {
            Match match = _regex.Match(key);
            if (match.Success)
            {
                result = new Dictionary<string, string>(_defaults);
                foreach (string groupName in _regex.GetGroupNames())
                {
                    if (groupName != "0")
                    {
                        result[groupName.ToLower()] = match.Groups[groupName].Value;
                    }
                }

                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }
    }
}