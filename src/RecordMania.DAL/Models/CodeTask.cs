﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using src.DAL.Models;

namespace src.RecordMania.DAL.Models;

[Table("CodeTasks")]
    public class CodeTask
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public ICollection<Record> Records { get; set; } = new List<Record>();
    }