﻿using System.ComponentModel.DataAnnotations;

namespace ShrtLy.DAL.Entities
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; set; }
    }
}
