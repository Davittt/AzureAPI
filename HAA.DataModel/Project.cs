﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HAA.DataModel
{
    [Table("Project")]
    public class Project
    {
        [Key]
        public int Index { get; set; }

        public string ProjectNumber { get; set; }

        public string ProjName { get; set; }

        public string Source { get; set; }

        public string FileData { get; set; }

        public string Version { get; set; }

        public string Designer { get; set; }

        public string DesignerEmail { get; set; }
    }
}
