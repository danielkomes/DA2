﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class PromotionEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public EPromotionType Type { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj is not PromotionEntity) return false;
            PromotionEntity other = obj as PromotionEntity;
            return Id == other.Id;
        }
    }
}
