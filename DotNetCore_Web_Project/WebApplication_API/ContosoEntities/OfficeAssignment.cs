﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_API.ContosoEntities
{
    public class OfficeAssignment : AuditableEntity
    {
        public OfficeAssignment()
        {
            CreatedDate = DateTime.Now;
        }

        [Key]
        [ForeignKey("Instructor")]
        public int InstructorId { get; set; }

        [MaxLength(150)]
        [Display(Name = "Office Location")]
        public string Location { get; set; }

        public Instructor Instructor { get; set; }
    }
}