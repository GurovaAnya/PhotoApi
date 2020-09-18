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
        public string PhotoName { get; set; }
        public int PhotoHash { get; set; }
        public virtual Person Person { get; set; }
        public static int CreateHash(byte[] photo)
        {
            unchecked
            {
                const int p = 16777619;
                int hash = (int)2166136261;

                for (int i = 0; i < photo.Length; i++)
                    hash = (hash ^ photo[i]) * p;

                hash += hash << 13;
                hash ^= hash >> 7;
                hash += hash << 3;
                hash ^= hash >> 17;
                hash += hash << 5;
                return hash;
            }
            
        }
    }
}
