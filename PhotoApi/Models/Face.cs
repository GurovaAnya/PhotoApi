﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoApi.Models
{
    public class Face
    {
        [Key]
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string  { get; set; }
    }
}
