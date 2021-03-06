﻿using System;

namespace HydrantWiki.Objects
{
    public class Setting : AbstractObject
    {
        public Setting()
        {
            Id = Guid.NewGuid();
        }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}

