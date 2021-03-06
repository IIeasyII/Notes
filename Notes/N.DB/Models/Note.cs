﻿using N.DB.Models.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace N.DB.Models
{
    public class Note : IEntity
    {
        public virtual long Id { get; set; }

        [Required(ErrorMessage = "Enter name notes")]
        public virtual string Name { get; set; }

        public virtual string Content { get; set; }

        public virtual bool Flag { get; set; }

        public virtual string TagList { get; set; }

        public virtual DateTime? Date { get; set; }

        public virtual string File { get; set; }

        public virtual User User { get; set; }
    }
}
