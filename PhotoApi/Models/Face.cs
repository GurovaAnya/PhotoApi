using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PhotoApi.Models
{
    public class Face
    {
        [Key]
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string Photo { get; set; }
        [JsonIgnore]
        public virtual Person Person { get; set; }
    }
}
